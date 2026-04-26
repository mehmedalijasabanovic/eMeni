import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ChangePasswordCommand,
  CreateUserCommand,
  CreateUserResponse,
  GetUserByIdDto,
  UpdateUserCommand
} from './users-api.model';

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

  /**
   * GET /User/{id}
   * Get user by id.
   */
  getById(id: number): Observable<GetUserByIdDto> {
    return this.http.get<GetUserByIdDto>(`${this.baseUrl}/${id}`);
  }

  /**
   * PUT /User/{id}
   * Update user profile.
   */
  update(id: number, payload: UpdateUserCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, payload);
  }

  /**
   * PUT /User/{id}/change-password
   * Change user password.
   */
  changePassword(id: number, payload: ChangePasswordCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}/change-password`, payload);
  }
}
