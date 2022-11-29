import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, GridReadyEvent } from 'ag-grid-community';
import { EditUserRoleComponent } from '../edit-user-role/edit-user-role.component';
import { ApiClient, RoleVm, UserVm } from 'src/app/api-clients/api-client';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;
  users: UserVm[] = [];
  pageSize: number = 10;
  currentPage: number = 1;
  usersCount: number = 0;
  searchEmail: string = "";
  gridHeight: number = 600;
  isDisableEditRole: boolean = true;
  isDisableEditBlock: boolean = true;
  isDisableEditAdmin: boolean = true;
  allRoles: RoleVm[] = [];

  public columnDefs: ColDef[] = [
    { field: 'email', resizable: true },
    { field: 'firstName', resizable: true },
    { field: 'lastName', resizable: true },
    { field: 'isBlocked', resizable: true },
    { field: 'isAdmin', resizable: true },
    { field: 'dateRegistration', resizable: true },
    { field: 'roleName', resizable: true }
  ];

  constructor(protected apiClient: ApiClient, public dialog: MatDialog, protected snackBar: MatSnackBar) { }
  ngOnInit(): void {
    this.apiClient.rolesList().subscribe((data) => {
      this.allRoles = data;
    })
  }

  openDialog() {
    const selectedRows = this.agGrid.api.getSelectedRows();
    if (selectedRows.length > 0) {
      let user = selectedRows[0] as UserVm;

      const dialogRef = this.dialog.open(EditUserRoleComponent, {
        width: '400px',
        data: { roles: this.allRoles, user: user}
      });
      dialogRef.afterClosed().subscribe(result => {
        this.snackBar.open("User role changed", "Succes");
        this.loadUsers();
      });
    }
  }

  changeUserBlockStatus() {
    const selectedRows = this.agGrid.api.getSelectedRows();
    if (selectedRows.length > 0) {
      let user = selectedRows[0] as UserVm;
      if (user.isBlocked) {
        var result = window.confirm("Do you really want unblock user " + user.email + " ?");
        if (result) {
          this.apiClient.changeUserBlockStatus(user.id, false).subscribe((data) => {
            this.loadUsers();
            this.snackBar.open("User " + user.email + " unblocked", "Success");
          })
        }
      } else {
        var result = window.confirm("Do you really want block user " + user.email + " ?");
        if (result) {
          this.apiClient.changeUserBlockStatus(user.id, true).subscribe((data) => {
            this.loadUsers();
            this.snackBar.open("User " + user.email + " blocked", "Success");
          })
        }
      }
    }
  }

  editAdminRights() {
    const selectedRows = this.agGrid.api.getSelectedRows();
    if (selectedRows.length > 0) {
      let user = selectedRows[0] as UserVm;
      if (user.isAdmin) {
        var result = window.confirm("Do you really want remove admin rights for user " + user.email + " ?");
        if (result) {
          this.apiClient.setUserAdminRights(user.id, false).subscribe((data) => {
            this.loadUsers();
            this.snackBar.open("For user " + user.email + " removed admin rights", "Success");
          })
        }
      } else {
        var result = window.confirm("Do you really want add admin rights for user " + user.email + " ?");
        if (result) {
          this.apiClient.setUserAdminRights(user.id, true).subscribe((data) => {
            this.loadUsers();
            this.snackBar.open("For user " + user.email + " added admin rights", "Success");
          })
        }
      }
    }
  }

  onSelectionChanged($event: any) {
    const selectedRows = this.agGrid.api.getSelectedRows();
    if (selectedRows.length > 0) {
      this.isDisableEditBlock = false;
      this.isDisableEditAdmin = false;
      if (selectedRows[0].isAdmin) {
        this.isDisableEditRole = true;
      } else {

        this.isDisableEditRole = false;
      }
    } else {
      this.isDisableEditAdmin = true;
      this.isDisableEditBlock = true;
    }
  }

  onGridReady(params: GridReadyEvent) {
    this.loadUsers();
    var he = window.innerHeight;
    this.gridHeight = he - 120;
    this.agGrid.api
  }

  loadUsers() {
    this.apiClient.getUsersCount(this.searchEmail).subscribe((data) => {
      this.usersCount = data;
      this.apiClient.usersList(this.currentPage, this.pageSize, this.searchEmail).subscribe((usersData) => {
        this.users = usersData;
        this.isDisableEditRole = true;
        this.isDisableEditBlock = true;
        this.isDisableEditAdmin = true;
      })
    })
  }
}
