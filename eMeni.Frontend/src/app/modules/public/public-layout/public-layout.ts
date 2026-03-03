import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { BusinessCategoriesApiService } from '../../../api-services/busines-categories/business-categories-api.service';
import {
  ListBusinessCategoriesDto,
  ListBusinessCategoriesRequest
} from '../../../api-services/busines-categories/business-categories-api.model';
import { BaseListPagedComponent } from '../../../core/components/base-classes/base-list-paged-component';
import { fadeAnimation } from '../../../core/animations/route-animations';

@Component({
  selector: 'app-public-layout',
  standalone: false,
  templateUrl: './public-layout.html',
  styleUrl: './public-layout.scss',
  animations: [fadeAnimation]
})
export class PublicLayout extends BaseListPagedComponent<ListBusinessCategoriesDto, ListBusinessCategoriesRequest>
  implements OnInit {

  private api = inject(BusinessCategoriesApiService);
  private router = inject(Router);

  constructor() {
    super();
    this.request = new ListBusinessCategoriesRequest();
  }

  ngOnInit() {
    this.initList();
  }

  protected loadPagedData(): void {
    this.startLoading();

    this.api.list(this.request).subscribe({
      next: (response) => {
        this.handlePageResult(response);
        this.stopLoading();
      },
      error: () => {
        this.stopLoading('Failed to load business categories.');
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
      .replace(/[^a-z0-9\s-]/g, '')
      .replace(/\s+/g, '-')
      .replace(/-+/g, '-')
      .replace(/^-|-$/g, '');
  }

  viewMenus(categoryId: number): void {
    const category = this.items.find((cat) => cat.id === categoryId);
    if (category) {
      const slug = this.categoryNameToSlug(category.categoryName);
      this.router.navigate(['/menus', slug]).catch((err: any) => {
        console.error('Navigation error:', err);
      });
    } else {
      this.router.navigate(['/menus'], { queryParams: { categoryId: categoryId } }).catch((err: any) => {
        console.error('Navigation error:', err);
      });
    }
  }
}
