import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { BaseComponent } from '../../../core/components/base-classes/base-component';
import { CurrentUserService } from '../../../core/services/auth/current-user.service';
import { UsersApiService } from '../../../api-services/users/users-api.service';
import { ToasterService } from '../../../core/services/toaster.service';
import { fadeAnimation } from '../../../core/animations/route-animations';

@Component({
  selector: 'app-user-profile',
  standalone: false,
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss',
  animations: [fadeAnimation]
})
export class UserProfileComponent extends BaseComponent implements OnInit {
  private fb = inject(FormBuilder);
  private usersApi = inject(UsersApiService);
  private currentUserService = inject(CurrentUserService);
  private translate = inject(TranslateService);
  private toaster = inject(ToasterService);
  private cdr = inject(ChangeDetectorRef);

  hideCurrentPassword = true;
  hideNewPassword = true;
  hideConfirmPassword = true;

  profileForm = this.fb.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', [Validators.required]]
  });

  passwordForm = this.fb.group(
    {
      currentPassword: ['', [Validators.required]],
      newPassword: ['', [Validators.required, Validators.minLength(6), Validators.pattern(/(?=.*[A-Za-z])(?=.*\d)/)]],
      confirmPassword: ['', [Validators.required]]
    },
    { validators: this.passwordsMatchValidator }
  );

  private get userId(): number {
    return this.currentUserService.snapshot?.userId ?? 0;
  }

  ngOnInit(): void {
    this.loadProfile();
  }

  private loadProfile(): void {
    this.startLoading();
    this.usersApi.getById(this.userId).subscribe({
      next: (user) => {
        this.profileForm.patchValue({
          firstName: user.firstName,
          lastName: user.lastName,
          email: user.email,
          phone: user.phone
        });
        this.stopLoading();
        this.cdr.markForCheck();
      },
      error: () => {
        this.stopLoading(this.translate.instant('PROFILE.LOAD_ERROR'));
        this.cdr.markForCheck();
      }
    });
  }

  onSaveProfile(): void {
    this.profileForm.markAllAsTouched();
    if (!this.profileForm.valid || this.isLoading) return;

    this.startLoading();
    this.usersApi.update(this.userId, {
      firstName: this.profileForm.value.firstName!,
      lastName: this.profileForm.value.lastName!,
      email: this.profileForm.value.email!,
      phone: this.profileForm.value.phone!
    }).subscribe({
      next: () => {
        this.stopLoading();
        this.toaster.success(this.translate.instant('PROFILE.UPDATE_SUCCESS'));
        this.cdr.markForCheck();
      },
      error: (err) => {
        this.stopLoading();
        const msg = err?.error?.message || this.translate.instant('PROFILE.UPDATE_ERROR');
        this.toaster.error(msg);
        this.cdr.markForCheck();
      }
    });
  }

  onChangePassword(): void {
    this.passwordForm.markAllAsTouched();
    if (!this.passwordForm.valid || this.isLoading) return;

    this.startLoading();
    this.usersApi.changePassword(this.userId, {
      currentPassword: this.passwordForm.value.currentPassword!,
      newPassword: this.passwordForm.value.newPassword!
    }).subscribe({
      next: () => {
        this.stopLoading();
        this.toaster.success(this.translate.instant('PROFILE.PASSWORD_SUCCESS'));
        this.passwordForm.reset();
        this.cdr.markForCheck();
      },
      error: (err) => {
        this.stopLoading();
        const msg = err?.error?.message || this.translate.instant('PROFILE.PASSWORD_ERROR');
        this.toaster.error(msg);
        this.cdr.markForCheck();
      }
    });
  }

  private passwordsMatchValidator(control: AbstractControl): ValidationErrors | null {
    const newPassword = control.get('newPassword')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;
    if (!newPassword || !confirmPassword) return null;
    return newPassword === confirmPassword ? null : { passwordMismatch: true };
  }
}
