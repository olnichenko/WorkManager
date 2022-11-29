import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, GridReadyEvent } from 'ag-grid-community';
import { ApiClient, UserVm } from 'src/app/api-clients/api-client';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent {
  
  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;
  users: UserVm[] = [];
  pageSize: number = 10;
  currentPage: number = 1;
  usersCount: number = 0;
  searchEmail: string = "";
  gridHeight: number = 600;
  isDisableEdit: boolean = true;

  public columnDefs: ColDef[] = [
    { field: 'email' , resizable: true },
    { field: 'firstName' , resizable: true },
    { field: 'lastName' , resizable: true },
    { field: 'isBlocked' , resizable: true },
    { field: 'isAdmin' , resizable: true },
    { field: 'dateRegistration' , resizable: true}
  ];

  constructor(protected apiClient: ApiClient, public dialog: MatDialog, protected snackBar: MatSnackBar) {}
  
  onSelectionChanged($event: any) {
    const selectedRows = this.agGrid.api.getSelectedRows();
    this.isDisableEdit = selectedRows.length == 0;
  }

  onGridReady(params: GridReadyEvent) {
    this.loadUsers();
    var he = window.innerHeight;
    this.gridHeight = he - 120;
    this.agGrid.api
  }

  loadUsers(){
    this.apiClient.getUsersCount(this.searchEmail).subscribe((data) => {
      this.usersCount = data;
      this.apiClient.usersList(this.currentPage, this.pageSize, this.searchEmail).subscribe((usersData) => {
        this.users = usersData;
      })
    })
  }
}
