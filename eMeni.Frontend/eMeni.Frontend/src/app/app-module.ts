import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideHttpClient, withInterceptors } from '@angular/common/http';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {CustomTranslateLoader} from './core/services/custom-translate-loader';
import {HttpClient} from '@angular/common/http';
import { loadingBarInterceptor } from './core/interceptors/loading-bar-interceptor.service';
import { authInterceptor } from './core/interceptors/auth-interceptor.service';
import { errorLoggingInterceptor } from './core/interceptors/error-logging-interceptor.service';


@NgModule({
  declarations: [
    App
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (http: HttpClient) => new CustomTranslateLoader(http),
        deps: [HttpClient]
      }
    }),
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(
      withInterceptors([
        loadingBarInterceptor,
        authInterceptor,
        errorLoggingInterceptor
      ])
    )
  ],
  bootstrap: [App]
})
export class AppModule { }
