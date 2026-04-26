import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ListQrProductsRequest, ListQrProductsResponse } from './qr-products-api.model';
import { buildHttpParams } from '../../core/models/build-http-params';

@Injectable({
  providedIn: 'root'
})
export class QrProductsApiService {
  private readonly baseUrl = `${environment.apiUrl}/QrProduct`;
  private http = inject(HttpClient);

  /**
   * GET /QrProduct
   * List QR code products (paged).
   */
  list(request?: ListQrProductsRequest): Observable<ListQrProductsResponse> {
    const params = request ? buildHttpParams(request as any) : undefined;
    return this.http.get<ListQrProductsResponse>(this.baseUrl, { params });
  }
}
