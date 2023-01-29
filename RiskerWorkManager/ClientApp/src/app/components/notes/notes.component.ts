import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiClient, Note, Project } from 'src/app/api-clients/api-client';
import { AccountService } from 'src/app/services/account.service';
import { ProjectService } from 'src/app/services/project.service';
import { EditNoteComponent } from './edit-note/edit-note.component';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {

  selectedNote: Note | null = null;
  project: Project = new Project();
  notes: Note[] = [];
  displayedColumns: string[] = ['title', 'userCreated', 'dateCreated'];
  isEnableEdit = false;

  constructor(protected apiClient: ApiClient,
    public accountService: AccountService,
    public dialog: MatDialog,
    public projectSevice: ProjectService,
    protected snackBar: MatSnackBar) { }

  confirmDelete() {
    var result = confirm("Are you sure you want to delete note?");
    if (result) {
      this.apiClient.deleteNote(this.selectedNote?.id).subscribe(data => {
        if (data) {
          this.snackBar.open("Note deleted", "Succes");
          this.loadNotes();
        } else {
          this.snackBar.open("Error", "Error");
        }
      })
    }
  }

  rowSelected(row: Note) {
    this.selectedNote = row;
  }

  openNewDialog(): void {
    const dialogRef = this.dialog.open(EditNoteComponent, {
      width: '800px',
      data: { EditNoteComponent: null, projectId: this.project.id }
    });

    dialogRef.afterClosed().subscribe(result => {
      let project = result;
      if (project != null) {
        this.snackBar.open("Note saved", "Succes");
        this.loadNotes();
      }
    });
  }

  openEditDialog(): void {
    if (this.selectedNote == null) {
      return;
    }
    const dialogRef = this.dialog.open(EditNoteComponent, {
      width: '800px',
      data: { note: this.selectedNote, projectId: this.project.id }
    });

    dialogRef.afterClosed().subscribe(result => {
      let project = result;
      if (project != null) {
        this.snackBar.open("Note saved", "Succes");
        this.loadNotes();
      }
    });
  }

  loadNotes(): void {
    this.apiClient.getNotesByProject(this.project.id).subscribe((data) => {
      this.notes = data;
      this.selectedNote = null;
    })
  }

  ngOnInit(): void {
    this.isEnableEdit = this.projectSevice.isUserCanEditProject();
    this.projectSevice.project.subscribe((data) => {
      this.project = data;
      this.loadNotes();
    });
  }

}
