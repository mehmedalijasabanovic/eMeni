import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MenusApiService } from '../../../api-services/menus/menus-api.service';
import { CityApiService } from '../../../api-services/cities/cities-api.service';
import { BusinessCategoriesApiService } from '../../../api-services/busines-categories/business-categories-api.service';
import {
  ListOnlyMenusDto,
  ListOnlyMenusRequest,
  ListOnlyMenusResponse
} from '../../../api-services/menus/menus-api.model';
import { ListCitiesDto, ListCitiesRequest } from '../../../api-services/cities/cities-api.model';
import { ListBusinessCategoriesRequest } from '../../../api-services/busines-categories/business-categories-api.model';
import { BaseListPagedComponent } from '../../../core/components/base-classes/base-list-paged-component';
import { TranslateService } from '@ngx-translate/core';
import { CurrentUserService } from '../../../core/services/auth/current-user.service';
import { AuthFacadeService } from '../../../core/services/auth/auth-facade.service';
import { ToasterService } from '../../../core/services/toaster.service';
import { fadeAnimation } from '../../../core/animations/route-animations';

@Component({
  selector: 'app-menus',
  standalone: false,
  templateUrl: './menus.component.html',
  styleUrl: './menus.component.scss',
  animations: [fadeAnimation]
})
export class MenusComponent extends BaseListPagedComponent<ListOnlyMenusDto, ListOnlyMenusRequest>
  implements OnInit {

  private menusApi = inject(MenusApiService);
  private citiesApi = inject(CityApiService);
  private categoriesApi = inject(BusinessCategoriesApiService);
  private translate = inject(TranslateService);
  private cdr = inject(ChangeDetectorRef);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private authFacade = inject(AuthFacadeService);
  private toaster = inject(ToasterService);

  currentUserService = inject(CurrentUserService);
  currentLang: string;

  // Expose computed signals directly for template reactivity
  isAuthenticated = this.currentUserService.isAuthenticated;
  currentUser = this.currentUserService.currentUser;

  languages = [
    { code: 'bs', name: 'Bosanski', flag: '🇧🇦' },
    { code: 'en', name: 'English', flag: '🇬🇧' }
  ];

  cities: ListCitiesDto[] = [];
  selectedCity: string | null = null; // null means "every city"
  categoryId: number | null = null;
  categoryName: string | null = null;

  constructor() {
    super();
    this.request = new ListOnlyMenusRequest();
    this.currentLang = this.translate.currentLang || 'bs';
    this.request.paging.pageSize = 9;
  }

  ngOnInit() {
    console.log('MenusComponent ngOnInit called');

    this.loadCities();

    // Get category slug from route params (new way)
    this.route.params.subscribe(params => {
      const categorySlug = params['categorySlug'];
      if (categorySlug) {
        console.log('Category slug from route:', categorySlug);
        this.loadCategoryBySlug(categorySlug);
      } else {
        // No category slug in route, check query params for backward compatibility
        const categoryIdParam = this.route.snapshot.queryParams['categoryId'];
        if (categoryIdParam) {
          this.categoryId = +categoryIdParam; // Convert to number
          console.log('CategoryId from query params:', this.categoryId);
          this.loadCategoryName();
          this.initList();
        } else {
          this.categoryId = null; // Clear categoryId if not in route
          this.categoryName = null;
          this.initList(); // Load all menus if no category filter
        }
      }
    });
  }

  /**
   * Converts a category name to a URL-friendly slug
   * Example: "Ugostiteljstvo" -> "ugostiteljstvo"
   */
  private categoryNameToSlug(categoryName: string): string {
    return categoryName
      .toLowerCase()
      .trim()
      .replace(/[^a-z0-9\s-]/g, '') // Remove special characters except spaces and hyphens
      .replace(/\s+/g, '-') // Replace spaces with hyphens
      .replace(/-+/g, '-') // Replace multiple hyphens with single hyphen
      .replace(/^-|-$/g, ''); // Remove leading/trailing hyphens
  }

  /**
   * Loads category by slug from route parameter
   */
  loadCategoryBySlug(categorySlug: string): void {
    const categoriesRequest = new ListBusinessCategoriesRequest();
    this.categoriesApi.list(categoriesRequest).subscribe({
      next: (response) => {
        // Find category by matching slug
        const category = response.items.find(cat => {
          const slug = this.categoryNameToSlug(cat.categoryName);
          return slug === categorySlug;
        });
        
        if (category) {
          this.categoryId = category.id;
          this.categoryName = category.categoryName;
          console.log('Category found:', category);
          this.cdr.markForCheck();
          // Initialize list with the found categoryId
          this.initList();
        } else {
          console.warn('Category not found for slug:', categorySlug);
          this.categoryId = null;
          this.categoryName = null;
          this.cdr.markForCheck();
          // Still load menus without category filter
          this.initList();
        }
      },
      error: (err) => {
        console.error('Failed to load categories:', err);
        this.categoryId = null;
        this.categoryName = null;
        this.cdr.markForCheck();
        // Still load menus without category filter
        this.initList();
      }
    });
  }

  loadCategoryName(): void {
    if (!this.categoryId) {
      return;
    }

    const categoriesRequest = new ListBusinessCategoriesRequest();
    this.categoriesApi.list(categoriesRequest).subscribe({
      next: (response) => {
        const category = response.items.find(cat => cat.id === this.categoryId);
        if (category) {
          this.categoryName = category.categoryName;
          this.cdr.markForCheck();
        }
      },
      error: (err) => {
        console.error('Failed to load category name:', err);
      }
    });
  }

  protected loadPagedData(): void {
    console.log('loadPagedData called, request:', this.request);
    this.startLoading();

    // Set categoryId if available, otherwise remove it
    if (this.categoryId) {
      this.request.categoryId = this.categoryId;
    } else {
      delete this.request.categoryId;
    }

    // Set city filter if a city is selected
    if (this.selectedCity) {
      this.request.city = this.selectedCity;
    } else {
      // Remove city filter if "every city" is selected
      delete this.request.city;
    }

    this.menusApi.listonlymenus(this.request).subscribe({
      next: (response) => {
        this.handlePageResult(response);
        console.log('Menus loaded:', response);

        // Check if no menus found and categoryId is set
        if (this.categoryId && response.items.length === 0) {
          this.toaster.error(this.translate.instant('MENUS.NO_MENUS_IN_CATEGORY') || 'No menus in this category');
        }

        this.stopLoading();
        this.cdr.markForCheck();
      },
      error: (err) => {
        this.stopLoading("Failed to load menus.");
        console.log('Load menus error: ', err);
        this.cdr.markForCheck();
        this.toaster.error(this.translate.instant('MENUS.LOAD_ERROR') || 'Failed to load menus');
      },
    });
  }

  loadCities(): void {
    const citiesRequest = new ListCitiesRequest();
    this.citiesApi.list(citiesRequest).subscribe({
      next: (response) => {
        this.cities = response.items;
        console.log('Cities loaded:', this.cities);
        this.cdr.markForCheck();
      },
      error: (err) => {
        console.error('Failed to load cities:', err);
        this.toaster.error(this.translate.instant('MENUS.CITIES_LOAD_ERROR') || 'Failed to load cities');
      }
    });
  }

  onCityChange(cityName: string | null): void {
    this.selectedCity = cityName;
    // Reset to first page when filtering
    this.request.paging.page = 1;
    this.loadPagedData();
  }

  switchLanguage(langCode: string): void {
    this.currentLang = langCode;
    this.translate.use(langCode);
    localStorage.setItem('language', langCode);
  }

  getCurrentLanguage() {
    return this.languages.find(lang => lang.code === this.currentLang);
  }
  loadMoreMenus(){
    this.request.paging.pageSize += 6;
    this.loadPagedData();
  }
  logout(): void {
    this.authFacade.logout().subscribe({
      next: () => {
        // Redirect to home page after logout
        this.router.navigate(['/']).catch((err: any) => {
          console.error('Navigation error:', err);
        });
      },
      error: (error) => {
        console.error('Logout error:', error);
        // Even if logout fails, redirect to home (optimistic logout)
        this.router.navigate(['/']).catch((err: any) => {
          console.error('Navigation error:', err);
        });
      }
    });
  }
}

