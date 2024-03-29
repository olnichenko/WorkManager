import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
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
import { ProjectsComponent } from './components/projects/projects.component';
import { AuthGuardServiceGuard } from './auth-guard-service.guard';
import { AddProjectComponent } from './components/projects/add-project/add-project.component';
import { ProjectComponent } from './components/projects/project/project.component';
import { ProjectNavMenuComponent } from './components/projects/project-nav-menu/project-nav-menu.component';
import { ProjectViewComponent } from './components/projects/project-view/project-view.component';
import { ProjectEditComponent } from './components/projects/project-edit/project-edit.component';
import { AddUserToProjectComponent } from './components/projects/add-user-to-project/add-user-to-project.component';
import { FeaturesComponent } from './components/features/features.component';
import { AddFeatureComponent } from './components/features/add-feature/add-feature.component';
import { RiskErrorHandler } from './services/error-handler';
import { VersionsComponent } from './components/versions/versions.component';
import { EditVersionComponent } from './components/versions/edit-version/edit-version.component';
import { BugsComponent } from './components/bugs/bugs.component';
import { NotesComponent } from './components/notes/notes.component';
import { TimeSheetsComponent } from './components/time-sheets/time-sheets.component';
import { EditBugComponent } from './components/bugs/edit-bug/edit-bug.component';
import { EditNoteComponent } from './components/notes/edit-note/edit-note.component';
import { FeatureViewComponent } from './components/features/feature-view/feature-view.component';
import { EditTimeSpentComponent } from './components/time-sheets/edit-time-spent/edit-time-spent.component';
import { BugViewComponent } from './components/bugs/bug-view/bug-view.component';
import { EditCommentComponent } from './components/comments/edit-comment/edit-comment.component';
import { NoteViewComponent } from './components/notes/note-view/note-view.component';

const projectRoutes: Routes = [
  { path: '', component: ProjectViewComponent,
  canActivate: [AuthGuardServiceChildGuard]  },
  { path: 'edit', component: ProjectEditComponent,
  canActivate: [AuthGuardServiceChildGuard]  },
  { path: 'features', component: FeaturesComponent,
  canActivate: [AuthGuardServiceChildGuard]  },
  { path: 'versions', component: VersionsComponent,
  canActivate: [AuthGuardServiceChildGuard]  },
  { path: 'bugs', component: BugsComponent,
  canActivate: [AuthGuardServiceChildGuard]  },
  { path: 'notes', component: NotesComponent,
  canActivate: [AuthGuardServiceChildGuard]  },
  { path: 'time-sheets', component: TimeSheetsComponent,
  canActivate: [AuthGuardServiceChildGuard]  }
]

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
  { path: 'projects', component: ProjectsComponent, canActivate: [AuthGuardServiceGuard] },
  { path: 'project/:id', component: ProjectComponent, canActivate: [AuthGuardServiceGuard], children: projectRoutes },
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
    EditUserRoleComponent,
    ProjectsComponent,
    AddProjectComponent,
    ProjectComponent,
    ProjectNavMenuComponent,
    ProjectViewComponent,
    ProjectEditComponent,
    AddUserToProjectComponent,
    FeaturesComponent,
    AddFeatureComponent,
    VersionsComponent,
    EditVersionComponent,
    BugsComponent,
    NotesComponent,
    TimeSheetsComponent,
    EditBugComponent,
    EditNoteComponent,
    FeatureViewComponent,
    EditTimeSpentComponent,
    BugViewComponent,
    EditCommentComponent,
    NoteViewComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AgGridModule,
    MaterialModule,
    RouterModule.forRoot(appRoutes, { onSameUrlNavigation: 'reload' }),
    BrowserAnimationsModule
  ],
  providers: [
    {
      provide: BASE_URL,
      useValue: environment.apiUrl
    },
    {provide: ErrorHandler, useClass: RiskErrorHandler},
    { provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 10000, horizontalPosition: "right" } },
    ApiClient
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
