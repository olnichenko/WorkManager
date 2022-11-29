import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { RegisterAdminComponent } from './components/register/register-admin.component';
import { MaterialModule } from './material.module';
import { AgGridModule } from 'ag-grid-angular';

import { ApiClient, BASE_URL } from './api-clients/api-client';
import { environment } from '../environments/environment';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';
import { LoginComponent } from './components/login/login.component';
import { RolesComponent } from './adimin/roles/roles.component';
import { AdministrationComponent } from './adimin/administration/administration.component';
import { AdminNavMenuComponent } from './adimin/admin-nav-menu/admin-nav-menu.component';
import { AddRoleComponent } from './adimin/add-role/add-role.component';
import { PermissionsComponent } from './adimin/permissions/permissions.component';
import { EditPermissionComponent } from './adimin/edit-permission/edit-permission.component';
import { AuthGuardServiceChildGuard } from './auth-guard-service-child.guard';
import { LogsComponent } from './adimin/logs/logs.component';
import { UsersComponent } from './adimin/users/users.component';
import { EditUserRoleComponent } from './adimin/edit-user-role/edit-user-role.component';

const adminRoutes: Routes = [
  { path: 'users', component: UsersComponent,
  canActivate: [AuthGuardServiceChildGuard]  },
  { path: 'roles', component: RolesComponent,
  canActivate: [AuthGuardServiceChildGuard]  },
  { path: 'permissions', component: PermissionsComponent,
  canActivate: [AuthGuardServiceChildGuard]  },
  { path: 'logs', component: LogsComponent,
  canActivate: [AuthGuardServiceChildGuard] },
];

const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'register-admin', component: RegisterAdminComponent },
  { path: 'administration', component: AdministrationComponent, children: adminRoutes},
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegisterComponent,
    RegisterAdminComponent,
    LoginComponent,
    RolesComponent,
    AdministrationComponent,
    AdminNavMenuComponent,
    AddRoleComponent,
    PermissionsComponent,
    EditPermissionComponent,
    LogsComponent,
    UsersComponent,
    EditUserRoleComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AgGridModule,
    MaterialModule,
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule
  ],
  providers: [
    {
      provide: BASE_URL,
      useValue: environment.apiUrl
    },
    { provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 10000, horizontalPosition: "right" } },
    ApiClient
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
