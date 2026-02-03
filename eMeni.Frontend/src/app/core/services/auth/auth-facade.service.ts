// src/app/core/services/auth/auth-facade.service.ts
import { Injectable, inject, signal, computed } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, tap, catchError, map } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

import { AuthApiService } from '../../../api-services/auth/auth-api.service';
import {
  LoginCommand,
  LoginCommandDto,
  LogoutCommand,
  RefreshTokenCommand,
  RefreshTokenCommandDto,
} from '../../../api-services/auth/auth-api.model';

import { AuthStorageService } from './auth-storage.service';
import { CurrentUserDto } from './current-user.dto';
import { JwtPayloadDto } from './jwt-payload.dto';

/**
 * Glavni auth servis (façade).
 * - priča sa AuthApiService (HTTP)
 * - priča sa AuthStorageService (localStorage)
 * - dekodira JWT i drži CurrentUser kao signal
 *
 * Koristi se u:
 * - interceptoru (getAccessToken, refresh)
 * - guardovima (isAuthenticated, isAdmin)
 * - komponentama (login, logout, navbar)
 */
@Injectable({ providedIn: 'root' })
export class AuthFacadeService {
  private api = inject(AuthApiService);
  private storage = inject(AuthStorageService);
  private router = inject(Router);

  // === REACTIVE STATE: current user ===

  private _currentUser = signal<CurrentUserDto | null>(null);

  /** readonly signal za UI – čita se kao auth.currentUser() */
  currentUser = this._currentUser.asReadonly();

  /** computed signali nad current userom */
  isAuthenticated = computed(() => !!this._currentUser());
  isAdmin = computed(() => this._currentUser()?.isAdmin ?? false);
  isOwner = computed(() => this._currentUser()?.isOwner ?? false);
  isUser = computed(() => this._currentUser()?.isUser ?? false);

  constructor() {
    // pokušaj inicijalizacije iz postojećeg access tokena
    this.initializeFromToken();
  }

  // =========================================================
  // PUBLIC API
  // =========================================================

  /**
   * Login korisnika (email + password).
   * Snima tokene u storage, dekodira JWT i popunjava current user state.
   */
  login(payload: LoginCommand): Observable<void> {
    return this.api.login(payload).pipe(
      tap((response: LoginCommandDto) => {
        this.storage.saveLogin(response);           // access + refresh + expiries
        this.decodeAndSetUser(response.accessToken); // popuni _currentUser
      }),
      map(() => void 0)
    );
  }

  /**
   * Logout korisnika:
   * - lokalno očisti state i tokene
   * - pokuša invalidirati refresh token na serveru (bez drame na error)
   */
  logout(): Observable<void> {
    const refreshToken = this.storage.getRefreshToken();

    // 1) lokalno očisti (optimistic logout)
    this.clearUserState();

    // 2) nema refresh tokena → nema ni API poziva
    if (!refreshToken) {
      return of(void 0);
    }

    const payload: LogoutCommand = { refreshToken };

    // 3) pokušaj server-side logout, ignoriši greške
    return this.api.logout(payload).pipe(catchError(() => of(void 0)));
  }

  /**
   * Refresh access tokena – koristi refresh token.
   * Poziva interceptor kada dobije 401.
   */
  refresh(payload: RefreshTokenCommand): Observable<RefreshTokenCommandDto> {
    return this.api.refresh(payload).pipe(
      tap((response: RefreshTokenCommandDto) => {
        this.storage.saveRefresh(response);           // snimi nove tokene
        this.decodeAndSetUser(response.accessToken);  // update current usera
      })
    );
  }

  /**
   * Utility za guardove/interceptore – očisti auth state i prebaci na /login.
   */
  redirectToLogin(): void {
    this.clearUserState();
    this.router.navigate(['/login']);
  }

  // =========================================================
  // GETTERI ZA INTERCEPTOR
  // =========================================================

  /**
   * Access token za Authorization header.
   */
  getAccessToken(): string | null {
    return this.storage.getAccessToken();
  }

  /**
   * Refresh token za refresh poziv.
   */
  getRefreshToken(): string | null {
    return this.storage.getRefreshToken();
  }

  // =========================================================
  // PRIVATE HELPERS
  // =========================================================

  /**
   * Na startu aplikacije (konstruktor) – pokušaj obnoviti stanje iz postojećeg tokena.
   */
  private initializeFromToken(): void {
    const token = this.storage.getAccessToken();
    if (token) {
      this.decodeAndSetUser(token);
    }
  }

  /**
   * Dekodiraj JWT i postavi current user state.
   */
  private decodeAndSetUser(token: string): void {
    try {
      // Decode without type constraint first to see all actual keys
      const rawPayload = jwtDecode<any>(token);
      
      const currentTimeInSeconds = Math.floor(Date.now() / 1000);
      if (rawPayload.exp && rawPayload.exp < currentTimeInSeconds) {
        console.warn('JWT token has expired. Clearing user state.');
        this._currentUser.set(null);
        this.storage.clear(); // Clear expired tokens
        return;
      }

      // .NET ClaimTypes.Email maps to this URI in JWT tokens
      const emailClaimUri = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress';
      
      // Try to get email from different possible claim names
      const email = rawPayload.email || 
                   rawPayload[emailClaimUri] ||
                   rawPayload.Email ||
                   rawPayload.emailaddress;

      if (!email) {
        console.error('Email claim not found in JWT token. Available keys:', Object.keys(rawPayload));
        console.error('Full payload:', rawPayload);
        this._currentUser.set(null);
        this.storage.clear();
        return;
      }

      const user: CurrentUserDto = {
        userId: Number(rawPayload.sub),
        email: email,
        isAdmin: rawPayload.is_admin === 'true' || rawPayload.is_admin === true,
        isOwner: rawPayload.is_owner === 'true' || rawPayload.is_owner === true,
        isUser: rawPayload.is_user === 'true' || rawPayload.is_user === true,
        tokenVersion: Number(rawPayload.ver),
      };

      console.log('User decoded successfully:', user);
      this._currentUser.set(user);
    } catch (error) {
      console.error('Failed to decode JWT token:', error);
      this._currentUser.set(null);
      this.storage.clear();
    }
  }

  /**
   * Očisti user state + sve tokene iz storage-a.
   */
  private clearUserState(): void {
    this._currentUser.set(null);
    this.storage.clear();
  }
}
