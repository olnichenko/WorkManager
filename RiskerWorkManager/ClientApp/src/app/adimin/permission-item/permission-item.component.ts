import { Component, Input, OnInit } from '@angular/core';
import { RowNodeBlockLoader } from 'ag-grid-community';
import { PermissionData, Role } from '../../api-clients/api-client';

@Component({
  selector: 'app-permission-item',
  templateUrl: './permission-item.component.html',
  styleUrls: ['./permission-item.component.css']
})
export class PermissionItemComponent implements OnInit {
  @Input() permissionData!: PermissionData;
  @Input() roles!: Role[]
  ngOnInit(): void {
  }

  public isPermissionContainRole(role: Role) : boolean{
    let result = false;
    role.permissions?.forEach(element => {
      if(element.name == this.permissionData.name){
        result = true;
        return;
      }
    });
    return result;
  }
}
