import { BrowserModule } from '@angular/platform-browser';
import { InjectionToken, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { RegisterComponent } from './components/register/register.component';
import { RegisterAdminComponent } from './components/register/register-admin.component';
import { MaterialModule } from './material.module';

import { ApiClient, BASE_URL } from './api-clients/api-client';
import { environment } from '../environments/environment';

// export const BASE_URL = new InjectionToken<string>('BASE_URL');

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    RegisterComponent,
    RegisterAdminComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'register-admin', component: RegisterAdminComponent },
    ])
  ],
  providers: [
    {
      provide: BASE_URL,
      useValue: environment.apiUrl
    },
    ApiClient
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
