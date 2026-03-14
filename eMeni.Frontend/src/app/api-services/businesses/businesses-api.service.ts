import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import {
  ListBusinessesRequest,
  ListBusinessesResponse
} from './businesses-api.model';
import { buildHttpParams } from '../../core/models/build-http-params';

@Injectable({
  providedIn: 'root'
})
export class BusinessesApiService {
  private readonly baseUrl = `${environment.apiUrl}/Business`;
  private http = inject(HttpClient);

  /**
   * GET /Business
   * List businesses (paged).
   */
  list(request?: ListBusinessesRequest): Observable<ListBusinessesResponse> {
    const params = request ? buildHttpParams(request as any) : undefined;
    return this.http.get<ListBusinessesResponse>(this.baseUrl, { params });
  }
}

