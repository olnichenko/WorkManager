import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css']
})
export class ToastComponent implements OnInit {
  show = true;
  text!: string;
  constructor() { }

  ngOnInit(): void {
  }
  showToast(text: string) {
    this.show = true;
    this.text = text;
  }
}
