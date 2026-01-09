import {Component, inject, OnInit, ChangeDetectorRef} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AuthFacadeService } from '../../../core/services/auth/auth-facade.service';
import { CurrentUserService } from '../../../core/services/auth/current-user.service';
import { ToasterService } from '../../../core/services/toaster.service';
import { LoginCommand } from '../../../api-services/auth/auth-api.model';
import {BaseComponent} from '../../../core/components/base-classes/base-component';
import { fadeAnimation, scaleFade } from '../../../core/animations/route-animations';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  animations: [fadeAnimation, scaleFade]
})
export class LoginComponent extends BaseComponent{
  private fb = inject(FormBuilder);
  private authFacade = inject(AuthFacadeService);
  private router = inject(Router);
  private currentUserService = inject(CurrentUserService);
  private toaster = inject(ToasterService);
  private cdr = inject(ChangeDetectorRef);
  translate = inject(TranslateService);

  hidePassword = true;
  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]],
    rememberMe: [false]
  });


  togglePasswordVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }

  goBack(): void {
    this.router.navigate(['/']);
  }

  onSubmit(): void {
    // Mark all fields as touched to show validation errors
    this.loginForm.markAllAsTouched();

    if (!this.loginForm.valid) {
      // Show validation error toast
      if (this.loginForm.get('email')?.hasError('required')) {
        this.toaster.error(this.translate.instant('LOGIN.EMAIL_REQUIRED'));
      } else if (this.loginForm.get('email')?.hasError('email')) {
        this.toaster.error(this.translate.instant('LOGIN.EMAIL_INVALID'));
      } else if (this.loginForm.get('password')?.hasError('required')) {
        this.toaster.error(this.translate.instant('LOGIN.PASSWORD_REQUIRED'));
      } else {
        this.toaster.error(this.translate.instant('LOGIN.FORM_INVALID'));
      }
      return;
    }

    if (this.isLoading) {
      return;
    }

    this.startLoading();
    this.cdr.detectChanges();

    const loginCommand: LoginCommand = {
      email: this.loginForm.value.email??'',
      password: this.loginForm.value.password??'',
      fingerprint: null
    };

    this.authFacade.login(loginCommand).subscribe({
      next: () => {
        // Signals update synchronously, but we need to ensure change detection runs
        // Verify user is authenticated before redirecting
        if (this.currentUserService.isAuthenticated()) {
          const user = this.currentUserService.currentUser();
          console.log('Login successful, user:', user);
          const defaultRoute = this.currentUserService.getDefaultRoute();
          this.stopLoading();
          this.cdr.detectChanges();
          this.toaster.success(this.translate.instant('LOGIN.SUCCESS'));
          // Small delay to show success message before navigation
          setTimeout(() => {
            this.router.navigate([defaultRoute]);
          }, 500);
        } else {
          console.error('User not authenticated after login');
          this.stopLoading();
          this.cdr.detectChanges();
          this.toaster.error(this.translate.instant('LOGIN.ERROR_AUTH_FAILED'));
        }
      },
      error: (error) => {
        console.error('Login error:', error);
        this.stopLoading();
        this.cdr.detectChanges();
        const errorMessage = error?.error?.message || error?.message || this.translate.instant('LOGIN.ERROR_GENERIC');
        this.toaster.error(errorMessage);
      }
    });
  }
}
