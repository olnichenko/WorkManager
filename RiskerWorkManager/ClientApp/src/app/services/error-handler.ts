import { ErrorHandler, Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable()
export class RiskErrorHandler implements ErrorHandler {
    constructor(protected snackBar: MatSnackBar){
        
    }
    handleError(_error: any) {
        // this.snackBar.open(_error, "Error");
    }
  }