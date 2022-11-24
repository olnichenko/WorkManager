import { Component, ViewChild } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, GridApi, GridReadyEvent } from 'ag-grid-community';
import { Observable } from 'rxjs';
import { ApiClient, Role, RoleVm } from '../../api-clients/api-client';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AddRoleComponent } from '../add-role/add-role.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent {
  public roles!: Role[];
  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;
  sheight: number = 600;
  public columnDefs: ColDef[] = [
    { field: 'name' },
    { field: 'description' }
  ];
  private gridApi!: GridApi;

  constructor(protected apiClient: ApiClient, public dialog: MatDialog, protected snackBar: MatSnackBar) {

  }

  onGridReady(params: GridReadyEvent) {
    this.loadRoles();
    var he = window.innerHeight;
    this.sheight = he - 120;
    this.agGrid.api
  }

  loadRoles() {
    this.apiClient.rolesList().subscribe((data) => {
      this.roles = data;
    })
  }

  openAddDialog(): void {
    const dialogRef = this.dialog.open(AddRoleComponent, {
      width: '800px'
    });

    dialogRef.afterClosed().subscribe(result => {
      let role = result;
      if (role != null) {
        this.openMessage("New role added", "Succes");
        this.loadRoles();
      }
    });
  }

  openMessage(message: string, action: string) {
    this.snackBar.open(message, action);
  }
}
