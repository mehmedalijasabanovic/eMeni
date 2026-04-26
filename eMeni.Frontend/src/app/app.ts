import { Component, OnInit, signal, computed } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router, RouterOutlet, NavigationEnd } from '@angular/router';
import { routeAnimations } from './core/animations/route-animations';
import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.scss',
  animations: [routeAnimations]
})
export class App implements OnInit {
  protected readonly title = signal('eMeni');
  currentLang: string = 'bs';
  showFooter = signal(true);

  constructor(private translate: TranslateService, private router: Router) {
    console.log('AppComponent constructor - initializing TranslateService');

    // Inicijalizacija translate servisa
    this.translate.addLangs(['en', 'bs']);
    this.translate.setDefaultLang('bs');

    // Učitaj jezik iz localStorage ili koristi default
    const savedLang = localStorage.getItem('language') || 'bs';
    this.currentLang = savedLang;

    this.translate.use(savedLang).subscribe({
      next: (translations) => {
        console.log('Translations loaded successfully for language:', savedLang);
        console.log('Available keys:', Object.keys(translations));
      },
      error: (error) => {
        console.error('Error loading translations:', error);
        console.error('Check if files exist at: /i18n/' + savedLang + '.json');
      }
    });
  }

  ngOnInit(): void {
    // Test translation
    this.translate.get('APP.TITLE').subscribe((res: string) => {
      console.log('Translation for APP.TITLE:', res);
      if (res === 'APP.TITLE') {
        console.error('⚠️ Translation not working! Key returned instead of value.');
      }
    });

    // Hide footer on auth routes
    this.router.events.pipe(
      filter(e => e instanceof NavigationEnd),
      map((e) => (e as NavigationEnd).urlAfterRedirects)
    ).subscribe(url => {
      this.showFooter.set(!url.startsWith('/auth'));
    });

    // Set initial value
    this.showFooter.set(!this.router.url.startsWith('/auth'));
  }

  prepareRoute(outlet: RouterOutlet): string {
    return outlet?.activatedRouteData?.['animation'] || '';
  }

  switchLanguage(lang: string): void {
    this.currentLang = lang;
    localStorage.setItem('language', lang);
    this.translate.use(lang).subscribe({
      next: () => {
        console.log('Language switched to:', lang);
      },
      error: (error) => {
        console.error('Error switching language:', error);
      }
    });
  }
}
