import { Component, Input, inject } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { CurrentUserService } from '../../../../core/services/auth/current-user.service';
import { AuthFacadeService } from '../../../../core/services/auth/auth-facade.service';

@Component({
  selector: 'app-fit-navbar',
  standalone: false,
  templateUrl: './fit-navbar.component.html',
  styleUrl: './fit-navbar.component.scss'
})
export class FitNavbarComponent {
  @Input() showFullNavbar = true;

  private translate = inject(TranslateService);
  private router = inject(Router);
  private authFacade = inject(AuthFacadeService);

  currentUserService = inject(CurrentUserService);
  currentLang: string;

  languages = [
    { code: 'bs', name: 'Bosanski', flag: '\uD83C\uDDE7\uD83C\uDDE6' },
    { code: 'en', name: 'English', flag: '\uD83C\uDDEC\uD83C\uDDE7' }
  ];

  constructor() {
    this.currentLang = localStorage.getItem('language') || this.translate.currentLang || 'bs';
  }

  switchLanguage(langCode: string): void {
    this.currentLang = langCode;
    this.translate.use(langCode);
    localStorage.setItem('language', langCode);
  }

  getCurrentLanguage() {
    return this.languages.find((lang) => lang.code === this.currentLang);
  }

  closeMobileMenu(): void {
    // Menu closes automatically on navigation.
  }

  navigateTo(path: string): void {
    this.router.navigate([path]).catch((err: any) => {
      console.error('Navigation error:', err);
    });
  }

  logout(): void {
    this.authFacade.logout().subscribe({
      next: () => {
        this.router.navigate(['/']).catch((err: any) => {
          console.error('Navigation error:', err);
        });
      },
      error: (error) => {
        console.error('Logout error:', error);
        this.router.navigate(['/']).catch((err: any) => {
          console.error('Navigation error:', err);
        });
      }
    });
  }
}
