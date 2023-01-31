import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiClient, TimeSpent, Project, TimeSheetFilterVm } from 'src/app/api-clients/api-client';
import { AccountService } from 'src/app/services/account.service';
import { ProjectService } from 'src/app/services/project.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-time-sheets',
  templateUrl: './time-sheets.component.html',
  styleUrls: ['./time-sheets.component.css']
})
export class TimeSheetsComponent implements OnInit {

  selectedTimeSpent: TimeSpent | null = null;
  project: Project = new Project();
  timeSpentList: TimeSpent[] = [];
  displayedColumns: string[] = ['dateFrom', 'userCreated', 'hoursCount', 'taskType', 'taskNumber', 'taskName', 'comment'];
  isEnableEdit = false;
  timeSheetFilterVm: TimeSheetFilterVm = new TimeSheetFilterVm();

  constructor(protected apiClient: ApiClient,
    public accountService: AccountService,
    public dialog: MatDialog,
    public projectSevice: ProjectService,
    protected snackBar: MatSnackBar) { }

  exportToExcel() {
    let element = document.getElementById('time-sheet-table');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
 
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
 
    /* save to file */  
    XLSX.writeFile(wb, 'time_sheet_table.xlsx');
  }

  ngOnInit(): void {
    this.isEnableEdit = this.projectSevice.isUserCanEditProject();
    this.projectSevice.project.subscribe((data) => {
      this.project = data;
      this.timeSheetFilterVm.projectId = this.project.id;
      this.timeSheetFilterVm.startDateFrom = new Date();
      this.timeSheetFilterVm.startDateFrom.setDate(new Date().getDate() + -7);
      this.timeSheetFilterVm.endDateFrom = new Date();
      this.timeSheetFilterVm.taskType = "All";
      this.load();
    });
  }

  rowDblClick(row: TimeSpent) {
    // let item = this.bugs.find(x => x.id == row.id);
    // const dialogRef = this.dialog.open(BugViewComponent, {
    //   width: '600px',
    //   data: { bug: item }
    // });
  }

  load(): void {
    // this.apiClient.getTimeSpentByProject(this.project.id).subscribe((data) => {
    //   this.timeSpentList = data;
    //   this.selectedTimeSpent = null;
    // })
    if(this.timeSheetFilterVm.taskId == null)
    {
      this.timeSheetFilterVm.taskId = 0;
    }
    this.apiClient.getTimeSpentByFilter(this.timeSheetFilterVm).subscribe(data => {
      this.timeSpentList = data;
      this.selectedTimeSpent = null;
    })
  }

  confirmDelete() {
    var result = confirm("Are you sure you want to delete time spent?");
    if (result) {
      // this.apiClient.deleteBug(this.selectedBug?.id).subscribe(data => {
      //   if (data) {
      //     this.snackBar.open("Bug deleted", "Succes");
      //     this.loadBugs();
      //   } else {
      //     this.snackBar.open("Error", "Error");
      //   }
      // })
    }
  }

  rowSelected(row: TimeSpent) {
    this.selectedTimeSpent = row;
  }
}
