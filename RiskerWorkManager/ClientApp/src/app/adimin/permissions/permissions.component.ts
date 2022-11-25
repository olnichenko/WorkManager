import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ApiClient, PermissionData, Role } from '../../api-clients/api-client';
import { PermissionRoles } from '../../models/permission-roles';

@Component({
  selector: 'app-permissions',
  templateUrl: './permissions.component.html',
  styleUrls: ['./permissions.component.css']
})
export class PermissionsComponent implements OnInit {
  protected permissionDatas: PermissionData[] = [];
  protected roles: Role[] = [];
  public showLoader: boolean = false;

  constructor(protected apiClient: ApiClient, protected snackBar: MatSnackBar, protected router: Router) {
  }

  ngOnInit(): void {
    this.showLoader = true;
    this.apiClient.permissionDataList().subscribe((data) => {
      this.permissionDatas = data;
      this.apiClient.rolesList().subscribe((rolesData) => {
        this.roles = rolesData;
        this.showLoader = false;
      })
    })
  }

  public isPermissionContainRole(role: Role, permissionData: PermissionData) : boolean{
    let result = false;
    role.permissions?.forEach(element => {
      if(element.name == permissionData.name){
        result = true;
        return;
      }
    });
    return result;
  }

  public changePermission(permissionName: string, roleId: number, isCheked: boolean){
    this.showLoader = true;
    this.apiClient.changePermission(roleId, permissionName, isCheked).subscribe((data) => {
      this.showLoader = false;
      if (data){
        this.snackBar.open("Role updated", "Succes");
      }
      this.reload();
    });
  }

  public reload(){
    this.ngOnInit(); 
  }
}

