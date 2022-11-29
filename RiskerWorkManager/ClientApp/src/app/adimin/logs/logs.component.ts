import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ApiClient } from 'src/app/api-clients/api-client';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.css']
})
export class LogsComponent implements OnInit {
  logFile!: string;

  constructor(protected apiClient: ApiClient, private http: HttpClient){}
  ngOnInit(): void {
    // this.http.get(environment.apiUrl + "/Logs/GetLogFilesList").subscribe((data) => {
    //   this.logFile = data.toString();
    // })
    this.apiClient.getLogFilesList().subscribe((data) => {
      this.logFile = data;
    })
  }
}
