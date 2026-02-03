import {inject,Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import { environment } from '../../../environments/environment';
import {ListCitiesRequest,ListCitiesResponse} from './cities-api.model';
import {buildHttpParams} from '../../core/models/build-http-params';
@Injectable(
  {
    providedIn: 'root',
  }
)
export class CityApiService {
  private readonly baseUrl=`${environment.apiUrl}/City`;
  private https=inject(HttpClient);
  /**GET /City
   * list cities
   */
  list(request?:ListCitiesRequest):Observable<ListCitiesResponse>{
    const params=request?buildHttpParams(request as any):undefined
    return this.https.get<ListCitiesResponse>(this.baseUrl,{params,});
  }
}
