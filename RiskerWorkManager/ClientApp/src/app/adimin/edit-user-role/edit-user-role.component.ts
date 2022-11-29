import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApiClient, RoleVm, UserVm } from 'src/app/api-clients/api-client';

@Component({
  selector: 'app-edit-user-role',
  templateUrl: './edit-user-role.component.html',
  styleUrls: ['./edit-user-role.component.css']
})
export class EditUserRoleComponent implements OnInit {

  currentUserRole: number = 0;

  constructor(public dialogRef: MatDialogRef<EditUserRoleComponent>, @Inject(MAT_DIALOG_DATA) public data: {roles: RoleVm[], user: UserVm}, private apiClient: ApiClient){
  }
  ngOnInit(): void {
    this.currentUserRole = this.data.user.role?.id;
  }
  save(){
    this.apiClient.changeUserRole(this.data.user.id, this.currentUserRole).subscribe((data) => {
      this.dialogRef.close(data);
    })
  }
}
