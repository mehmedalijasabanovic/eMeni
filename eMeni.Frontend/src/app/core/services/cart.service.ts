import { Injectable, signal, computed } from '@angular/core';
import { ListQrProductDto } from '../../api-services/qr-products/qr-products-api.model';

export interface CartItem {
  product: ListQrProductDto;
  quantity: number;
}

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private _items = signal<CartItem[]>([]);

  items = this._items.asReadonly();
  itemCount = computed(() => this._items().reduce((sum, item) => sum + item.quantity, 0));
  total = computed(() => this._items().reduce((sum, item) => sum + item.product.price * item.quantity, 0));

  addProduct(product: ListQrProductDto): void {
    const current = this._items();
    const existing = current.find(i => i.product.id === product.id);
    if (existing) {
      this._items.set(current.map(i =>
        i.product.id === product.id ? { ...i, quantity: i.quantity + 1 } : i
      ));
    } else {
      this._items.set([...current, { product, quantity: 1 }]);
    }
  }

  removeProduct(productId: number): void {
    this._items.set(this._items().filter(i => i.product.id !== productId));
  }

  clear(): void {
    this._items.set([]);
  }
}
