import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { BusinessCategoriesApiService } from '../../../api-services/busines-categories/business-categories-api.service';
import {
  ListBusinessCategoriesDto, ListBusinessCategoriesRequest,
  ListBusinessCategoriesResponse
} from '../../../api-services/busines-categories/business-categories-api.model';
import {BaseListPagedComponent} from '../../../core/components/base-classes/base-list-paged-component';
import {TranslateService} from '@ngx-translate/core';

@Component({
  selector: 'app-public-layout',
  standalone: false,
  templateUrl: './public-layout.html',
  styleUrl: './public-layout.scss',
})
export class PublicLayout extends BaseListPagedComponent<ListBusinessCategoriesDto,ListBusinessCategoriesRequest>
  implements OnInit {

  private api = inject(BusinessCategoriesApiService);
  private translate=inject(TranslateService);
  private cdr = inject(ChangeDetectorRef);

  currentLang:string;

  languages = [
    { code: 'bs', name: 'Bosanski', flag: '🇧🇦' },
    { code: 'en', name: 'English', flag: '🇬🇧' }
  ];

  constructor() {
    super();
    this.request=new ListBusinessCategoriesRequest();
    this.currentLang=this.translate.currentLang || 'bs';
  }
  
  ngOnInit() {
    console.log('PublicLayout ngOnInit called');
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
}
