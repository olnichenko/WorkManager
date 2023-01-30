import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiClient, Bug, Project } from 'src/app/api-clients/api-client';
import { AccountService } from 'src/app/services/account.service';
import { ProjectService } from 'src/app/services/project.service';
import { EditBugComponent } from './edit-bug/edit-bug.component';
import { BugViewComponent } from './bug-view/bug-view.component';

@Component({
  selector: 'app-bugs',
  templateUrl: './bugs.component.html',
  styleUrls: ['./bugs.component.css']
})
export class BugsComponent implements OnInit {

  selectedBug: Bug | null = null;
  project: Project = new Project();
  bugs: Bug[] = [];
  displayedColumns: string[] = ['title', 'userCreated', 'dateCreated', 'solvedInVersion'];
  isEnableEdit = false;

  constructor(protected apiClient: ApiClient,
    public accountService: AccountService,
    public dialog: MatDialog,
    public projectSevice: ProjectService,
    protected snackBar: MatSnackBar) { }

  confirmDelete() {
    var result = confirm("Are you sure you want to delete bug?");
    if (result) {
      this.apiClient.deleteBug(this.selectedBug?.id).subscribe(data => {
        if (data) {
          this.snackBar.open("Bug deleted", "Succes");
          this.loadBugs();
        } else {
          this.snackBar.open("Error", "Error");
        }
      })
    }
  }

  rowSelected(row: Bug) {
    this.selectedBug = row;
  }

  rowDblClick(row: Bug) {
    let item = this.bugs.find(x => x.id == row.id);
    const dialogRef = this.dialog.open(BugViewComponent, {
      width: '600px',
      data: { bug: item }
    });
  }

  loadBugs(): void {
    this.apiClient.getBugsByProject(this.project.id).subscribe((data) => {
      this.bugs = data;
      this.selectedBug = null;
    })
  }

  openNewDialog(): void {
    const dialogRef = this.dialog.open(EditBugComponent, {
      width: '800px',
      data: { bug: null, projectId: this.project.id }
    });

    dialogRef.afterClosed().subscribe(result => {
      let project = result;
      if (project != null) {
        this.snackBar.open("Bug saved", "Succes");
        this.loadBugs();
      }
    });
  }

  openEditDialog(): void {
    if (this.selectedBug == null) {
      return;
    }
    const dialogRef = this.dialog.open(EditBugComponent, {
      width: '800px',
      data: { bug: this.selectedBug, projectId: this.project.id }
    });

    dialogRef.afterClosed().subscribe(result => {
      let project = result;
      if (project != null) {
        this.snackBar.open("Bug saved", "Succes");
        this.loadBugs();
      }
    });
  }

  ngOnInit(): void {
    this.isEnableEdit = this.projectSevice.isUserCanEditProject();
    this.projectSevice.project.subscribe((data) => {
      this.project = data;
      this.loadBugs();
    });
  }
}
