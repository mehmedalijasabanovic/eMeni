import { ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { AbstractControl, FormBuilder, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { BaseComponent } from '../../../core/components/base-classes/base-component';
import { fadeAnimation, scaleFade } from '../../../core/animations/route-animations';
import { ToasterService } from '../../../core/services/toaster.service';
import { CityApiService } from '../../../api-services/cities/cities-api.service';
import { ListCitiesDto, ListCitiesRequest } from '../../../api-services/cities/cities-api.model';
import { UsersApiService } from '../../../api-services/users/users-api.service';
import { CreateUserCommand } from '../../../api-services/users/users-api.model';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
  animations: [fadeAnimation, scaleFade]
})
export class RegisterComponent extends BaseComponent implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private translate = inject(TranslateService);
  private toaster = inject(ToasterService);
  private cdr = inject(ChangeDetectorRef);
  private citiesApi = inject(CityApiService);
  private usersApi = inject(UsersApiService);

  hidePassword = true;
  hideConfirmPassword = true;
  cities: ListCitiesDto[] = [];

  registerForm = this.fb.group(
    {
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^0?6\d{7,8}$/)]],
      cityId: [null as number | null, [Validators.required]],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d).{6,}$/)]],
      confirmPassword: ['', [Validators.required]]
    },
    { validators: this.passwordsMatchValidator }
  );

  ngOnInit(): void {
    this.loadCities();
  }

  togglePasswordVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }

  toggleConfirmPasswordVisibility(): void {
    this.hideConfirmPassword = !this.hideConfirmPassword;
  }

  goBack(): void {
    this.router.navigate(['/']);
  }

  private passwordsMatchValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;
    if (!password || !confirmPassword) {
      return null;
    }
    return password === confirmPassword ? null : { passwordMismatch: true };
  }

  private loadCities(): void {
    const request = new ListCitiesRequest();
    this.citiesApi.list(request).subscribe({
      next: (response) => {
        this.cities = response.items;
        this.cdr.markForCheck();
      },
      error: () => {
        this.toaster.error(this.translate.instant('REGISTER.CITIES_LOAD_ERROR'));
      }
    });
  }

  onSubmit(): void {
    this.registerForm.markAllAsTouched();

    if (!this.registerForm.valid) {
      this.toaster.error(this.translate.instant('REGISTER.FORM_INVALID'));
      return;
    }

    if (this.isLoading) {
      return;
    }

    this.startLoading();
    this.cdr.detectChanges();

    const rawPhone = this.registerForm.value.phone ?? '';
    const normalizedPhone = this.phoneNorm(rawPhone);
    const command: CreateUserCommand = {
      email: this.registerForm.value.email ?? '',
      passwordHash: this.registerForm.value.password ?? '',
      firstName: this.registerForm.value.firstName ?? '',
      lastName: this.registerForm.value.lastName ?? '',
      phone: normalizedPhone,
      cityId: this.registerForm.value.cityId ?? 0
    };

    this.usersApi.create(command).subscribe({
      next: () => {
        this.stopLoading();
        this.cdr.detectChanges();
        this.toaster.success(this.translate.instant('REGISTER.SUCCESS'));
        setTimeout(() => {
          this.router.navigate(['/auth/login']);
        }, 500);
      },
      error: (error) => {
        this.stopLoading();
        this.cdr.detectChanges();
        const errorMessage =
          error?.error?.message ||
          error?.message ||
          this.translate.instant('REGISTER.ERROR_GENERIC');
        this.toaster.error(errorMessage);
      }
    });
  }

  private phoneNorm(phone: string): string {
    const digitsOnly = phone.replace(/\D/g, '');
    if (digitsOnly.length === 0) {
      return '';
    }
    const normalized = digitsOnly.startsWith('0') ? digitsOnly.slice(1) : digitsOnly;
    return `+387${normalized}`;
  }
}
