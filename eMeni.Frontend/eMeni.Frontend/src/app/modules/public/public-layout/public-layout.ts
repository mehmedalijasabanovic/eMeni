import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { BusinessCategoriesApiService } from '../../../api-services/busines-categories/business-categories-api.service';
import {
  ListBusinessCategoriesDto, ListBusinessCategoriesRequest,
  ListBusinessCategoriesResponse
} from '../../../api-services/busines-categories/business-categories-api.model';
import {BaseListPagedComponent} from '../../../core/components/base-classes/base-list-paged-component';
import {TranslateService} from '@ngx-translate/core';
import {CurrentUserService} from '../../../core/services/auth/current-user.service';
import {AuthFacadeService} from '../../../core/services/auth/auth-facade.service';
import {ToasterService} from '../../../core/services/toaster.service';
import { fadeAnimation } from '../../../core/animations/route-animations';

@Component({
  selector: 'app-public-layout',
  standalone: false,
  templateUrl: './public-layout.html',
  styleUrl: './public-layout.scss',
  animations: [fadeAnimation]
})
export class PublicLayout extends BaseListPagedComponent<ListBusinessCategoriesDto,ListBusinessCategoriesRequest>
  implements OnInit {

  private api = inject(BusinessCategoriesApiService);
  private translate = inject(TranslateService);
  private cdr = inject(ChangeDetectorRef);
  private router = inject(Router);
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

  constructor() {
    super();
    this.request = new ListBusinessCategoriesRequest();
    this.currentLang = this.translate.currentLang || 'bs';
  }

  ngOnInit() {
    console.log('PublicLayout ngOnInit called');
    console.log('Is authenticated:', this.isAuthenticated());
    console.log('Current user:', this.currentUser());
    this.initList();
  }
  protected loadPagedData():void{
    console.log('loadPagedData called, request:', this.request);
    this.startLoading();

    this.api.list(this.request).subscribe({
      next:(response )=>{
        this.handlePageResult(response);
        console.log('Categories loaded:', response);
        this.stopLoading();
        this.cdr.markForCheck(); // Mark component for change detection

       
      },
      error:(err )=>{
        this.stopLoading("Failed to load business categories.");
        console.log('Load business categories error: ',err);
        this.cdr.markForCheck(); // Mark component for change detection
        this.toaster.error(this.translate.instant('CATEGORIES.LOAD_ERROR'));
      },
    });

  }

  switchLanguage(langCode: string): void {
    this.currentLang = langCode;
    this.translate.use(langCode);
    localStorage.setItem('language', langCode);
  }

  getCurrentLanguage() {
    return this.languages.find(lang => lang.code === this.currentLang);
  }

  closeMobileMenu(): void {
    // Menu will close automatically on navigation
  }

  navigateToLogin(): void {
    this.router.navigate(['/auth/login']).catch((err: any) => {
      console.error('Navigation error:', err);
    });
  }

  navigateToRegister(): void {
    this.router.navigate(['/auth/register']).catch((err: any) => {
      console.error('Navigation error:', err);
    });
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
