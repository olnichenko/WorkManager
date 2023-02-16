import { HttpClient, HttpEventType, HttpRequest } from '@angular/common/http';
import { Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ApiClient, Project } from 'src/app/api-clients/api-client';
import { AccountService } from 'src/app/services/account.service';
import { ProjectService } from 'src/app/services/project.service';
import { AddUserToProjectComponent } from '../add-user-to-project/add-user-to-project.component';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-project-view',
  templateUrl: './project-view.component.html',
  styleUrls: ['./project-view.component.css']
})
export class ProjectViewComponent implements OnInit, OnDestroy {

  isEnableEdit: boolean = false;
  project: Project = new Project();
  private subscriptions: Subscription[] = [];
  files: string[] = [];
  filePath: string = environment.apiUrl + "/Files/Projects/";
  @ViewChild('file')
  inputFiles!: ElementRef;

  constructor(public projectSevice: ProjectService,
    private http: HttpClient,
    protected accountService: AccountService,
    protected apiClient: ApiClient,
    private router: Router,
    public dialog: MatDialog,
    protected snackBar: MatSnackBar) { }

  removeUserFromProject(email: string) {
    if (!this.isEnableEdit) {
      return;
    }
    var result = confirm("Are you sure you want to remove user from project?");
    if (result) {
      this.apiClient.removeUserFromProject(email, this.project.id).subscribe(data => {
        if (data) {
          this.snackBar.open("User removed from project", "Succes");
          this.projectSevice.loadProject(this.project.id);
        } else {
          this.snackBar.open("Error", "Error");
        }
      });
    }

  }

  openAddUserDialog(): void {
    const dialogRef = this.dialog.open(AddUserToProjectComponent, {
      width: '600px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.apiClient.addUserToProject(result, this.project.id).subscribe((data) => {
          if (data) {
            this.snackBar.open("User added to project", "Succes");
            this.projectSevice.loadProject(this.project.id);
          } else {
            this.snackBar.open("Error", "Error");
          }
        })
      }
    });
  }

  upload(files: any) {
    if (files.length === 0)
      return;

    const formData = new FormData();

    for (const file of files) {
      formData.append(file.name, file);
    }

    const uploadReq = new HttpRequest('POST', environment.apiUrl + '/files/UploadToProject?projectId=' + this.project.id, formData, {
      reportProgress: false,
    });

    this.http.request(uploadReq).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress) {
        
      };
      this.snackBar.open("File uploaded", "Succes");
      this.loadFiles();
    });
    this.inputFiles.nativeElement.value = "";
  }

  viewFile(fileName: string){
    window.open(this.filePath + this.project.id + "/" + fileName, "_blank");
  }

  loadFiles(){
    this.apiClient.getProjectFiles(this.project?.id).subscribe(data => {
      this.files = data;
    })
  }

  removeFile(name: string){
    this.apiClient.removeFileFromProject(name, this.project.id).subscribe(data => {
        this.snackBar.open("File deleted", "Succes");
        this.loadFiles();
    })
  }

  ngOnDestroy(): void {
    this.unsubscribe();
  }

  navigateToEdit() {
    this.router.navigate(['/project/' + this.project.id + '/edit']);
  }

  private unsubscribe() {
    this.subscriptions
      .forEach(s => s.unsubscribe());
  }

  loadProject() {
    return this.projectSevice.project.subscribe((data) => {
      this.project = data;
      this.isEnableEdit = this.accountService.getCurrentUser()?.id == data.userCreated?.id;
      if(this.project != null){
        this.loadFiles();
      }
    })
  }

  ngOnInit(): void {
    const sub = this.loadProject();
    this.subscriptions.push(sub);
  }
}
