import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CreateUserCommand, CreateUserResponse } from './users-api.model';

@Injectable({
  providedIn: 'root'
})
export class UsersApiService {
  private readonly baseUrl = `${environment.apiUrl}/User`;
  private http = inject(HttpClient);

  /**
   * POST /User
   * Create user account.
   */
  create(payload: CreateUserCommand): Observable<CreateUserResponse> {
    return this.http.post<CreateUserResponse>(this.baseUrl, payload);
  }
}
