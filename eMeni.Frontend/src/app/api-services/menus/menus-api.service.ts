import {inject,Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import { environment } from '../../../environments/environment';
import {ListOnlyMenusRequest,ListOnlyMenusResponse} from './menus-api.model';
import {buildHttpParams} from '../../core/models/build-http-params';
@Injectable(
  {
    providedIn: 'root',
  }
)
export class MenusApiService {
  private readonly baseUrl=`${environment.apiUrl}/only-menus`;
  private https=inject(HttpClient);
  /**GET /only-menus
   * list cities
   */
  listonlymenus(request?:ListOnlyMenusRequest):Observable<ListOnlyMenusResponse>{
    const params=request?buildHttpParams(request as any):undefined
    return this.https.get<ListOnlyMenusResponse>(this.baseUrl,{params,});
  }
}
