import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenusApiService } from '../../../api-services/menus/menus-api.service';
import { CityApiService } from '../../../api-services/cities/cities-api.service';
import { BusinessCategoriesApiService } from '../../../api-services/busines-categories/business-categories-api.service';
import {
  ListOnlyMenusDto,
  ListOnlyMenusRequest
} from '../../../api-services/menus/menus-api.model';
import { ListCitiesDto, ListCitiesRequest } from '../../../api-services/cities/cities-api.model';
import { ListBusinessCategoriesRequest } from '../../../api-services/busines-categories/business-categories-api.model';
import { BaseListPagedComponent } from '../../../core/components/base-classes/base-list-paged-component';
import { TranslateService } from '@ngx-translate/core';
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
  private route = inject(ActivatedRoute);
  private toaster = inject(ToasterService);

  cities: ListCitiesDto[] = [];
  selectedCity: string | null = null;
  categoryId: number | null = null;
  categoryName: string | null = null;

  constructor() {
    super();
    this.request = new ListOnlyMenusRequest();
    this.request.paging.pageSize = 9;
  }

  ngOnInit() {
    this.loadCities();

    this.route.params.subscribe((params) => {
      const categorySlug = params['categorySlug'];
      if (categorySlug) {
        this.loadCategoryBySlug(categorySlug);
      } else {
        const categoryIdParam = this.route.snapshot.queryParams['categoryId'];
        if (categoryIdParam) {
          this.categoryId = +categoryIdParam;
          this.loadCategoryName();
          this.initList();
        } else {
          this.categoryId = null;
          this.categoryName = null;
          this.initList();
        }
      }
    });
  }

  private categoryNameToSlug(categoryName: string): string {
    return categoryName
      .toLowerCase()
      .trim()
      .replace(/[^a-z0-9\s-]/g, '')
      .replace(/\s+/g, '-')
      .replace(/-+/g, '-')
      .replace(/^-|-$/g, '');
  }

  loadCategoryBySlug(categorySlug: string): void {
    const categoriesRequest = new ListBusinessCategoriesRequest();
    this.categoriesApi.list(categoriesRequest).subscribe({
      next: (response) => {
        const category = response.items.find((cat) => {
          const slug = this.categoryNameToSlug(cat.categoryName);
          return slug === categorySlug;
        });

        if (category) {
          this.categoryId = category.id;
          this.categoryName = category.categoryName;
          this.cdr.markForCheck();
          this.initList();
        } else {
          this.categoryId = null;
          this.categoryName = null;
          this.cdr.markForCheck();
          this.initList();
        }
      },
      error: (err) => {
        console.error('Failed to load categories:', err);
        this.categoryId = null;
        this.categoryName = null;
        this.cdr.markForCheck();
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
        const category = response.items.find((cat) => cat.id === this.categoryId);
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
    this.startLoading();

    if (this.categoryId) {
      this.request.categoryId = this.categoryId;
    } else {
      delete this.request.categoryId;
    }

    if (this.selectedCity) {
      this.request.city = this.selectedCity;
    } else {
      delete this.request.city;
    }

    this.menusApi.listonlymenus(this.request).subscribe({
      next: (response) => {
        this.handlePageResult(response);

        if (this.categoryId && response.items.length === 0) {
          this.toaster.error(this.translate.instant('MENUS.NO_MENUS_IN_CATEGORY') || 'No menus in this category');
        }

        this.stopLoading();
        this.cdr.markForCheck();
      },
      error: () => {
        this.stopLoading('Failed to load menus.');
        this.cdr.markForCheck();
        this.toaster.error(this.translate.instant('MENUS.LOAD_ERROR') || 'Failed to load menus');
      }
    });
  }

  loadCities(): void {
    const citiesRequest = new ListCitiesRequest();
    this.citiesApi.list(citiesRequest).subscribe({
      next: (response) => {
        this.cities = response.items;
        this.cdr.markForCheck();
      },
      error: () => {
        this.toaster.error(this.translate.instant('MENUS.CITIES_LOAD_ERROR') || 'Failed to load cities');
      }
    });
  }

  onCityChange(cityName: string | null): void {
    this.selectedCity = cityName;
    this.request.paging.page = 1;
    this.loadPagedData();
  }

  loadMoreMenus() {
    this.request.paging.pageSize += 6;
    this.loadPagedData();
  }
}
