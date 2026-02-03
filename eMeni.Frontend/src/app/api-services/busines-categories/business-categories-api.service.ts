import {inject,Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import { environment } from '../../../environments/environment';
import {ListBusinessCategoriesRequest, ListBusinessCategoriesResponse} from './business-categories-api.model';
import {buildHttpParams} from '../../core/models/build-http-params';
@Injectable(
  {
    providedIn: 'root',
  }
)
export class BusinessCategoriesApiService {
  private readonly baseUrl=`${environment.apiUrl}/BusinessesCategory`;
  private https=inject(HttpClient);
  /**GET /BusinessCategproes
   * list categories
   */
  list(request?:ListBusinessCategoriesRequest):Observable<ListBusinessCategoriesResponse>{
    const params=request?buildHttpParams(request as any):undefined
    return this.https.get<ListBusinessCategoriesResponse>(this.baseUrl,{params,});
  }
}
