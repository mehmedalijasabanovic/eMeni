import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { BusinessCategoriesApiService } from '../../../api-services/busines-categories/business-categories-api.service';
import {
  ListBusinessCategoriesDto,
  ListBusinessCategoriesRequest
} from '../../../api-services/busines-categories/business-categories-api.model';
import { BaseListPagedComponent } from '../../../core/components/base-classes/base-list-paged-component';
import { fadeAnimation } from '../../../core/animations/route-animations';
import { QrProductsApiService } from '../../../api-services/qr-products/qr-products-api.service';
import { ListQrProductDto, ListQrProductsRequest } from '../../../api-services/qr-products/qr-products-api.model';
import { CartService } from '../../../core/services/cart.service';
import { CurrentUserService } from '../../../core/services/auth/current-user.service';
import { ToasterService } from '../../../core/services/toaster.service';
import { TranslateService } from '@ngx-translate/core';

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
  private qrApi = inject(QrProductsApiService);
  private cartService = inject(CartService);
  private currentUserService = inject(CurrentUserService);
  private toaster = inject(ToasterService);
  private translate = inject(TranslateService);

  qrProducts: ListQrProductDto[] = [];
  qrLoading = false;

  get isOwner(): boolean {
    return this.currentUserService.isOwner();
  }

  constructor() {
    super();
    this.request = new ListBusinessCategoriesRequest();
  }

  ngOnInit() {
    this.initList();
    this.loadQrProducts();
  }

  private loadQrProducts(): void {
    this.qrLoading = true;
    this.qrApi.list(new ListQrProductsRequest()).subscribe({
      next: (response) => {
        this.qrProducts = response.items;
        this.qrLoading = false;
      },
      error: () => {
        this.qrLoading = false;
      }
    });
  }

  addToCart(product: ListQrProductDto): void {
    this.cartService.addProduct(product);
    this.toaster.success(
      this.translate.instant('QR.ADDED_TO_CART', { name: product.productName })
    );
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

  viewBusinesses(categoryId: number): void {
    const category = this.items.find((cat) => cat.id === categoryId);
    if (category) {
      const slug = this.categoryNameToSlug(category.categoryName);
      this.router
        .navigate(['/businesses', slug], {
          queryParams: { categoryId }
        })
        .catch((err: any) => {
          console.error('Navigation error:', err);
        });
    } else {
      this.router
        .navigate(['/businesses'], {
          queryParams: { categoryId }
        })
        .catch((err: any) => {
          console.error('Navigation error:', err);
        });
    }
  }
}
