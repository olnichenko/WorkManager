import { Component, ViewChild } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, GridApi, GridReadyEvent, RowValueChangedEvent } from 'ag-grid-community';
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
  public defaultColDef: ColDef = {
    editable: true,
  };
  public editType: 'fullRow' = 'fullRow';

  constructor(protected apiClient: ApiClient, public dialog: MatDialog, protected snackBar: MatSnackBar) {

  }

  onRowValueChanged(event: RowValueChangedEvent) {
    var data = event.data;
    console.log(
      'onRowValueChanged: (' +
        data.make +
        ', ' +
        data.model +
        ', ' +
        data.price +
        ', ' +
        data.field5 +
        ')'
    );
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
