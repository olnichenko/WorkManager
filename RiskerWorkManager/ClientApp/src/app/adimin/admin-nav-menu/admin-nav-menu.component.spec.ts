import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminNavMenuComponent } from './admin-nav-menu.component';

describe('AdminNavMenuComponent', () => {
  let component: AdminNavMenuComponent;
  let fixture: ComponentFixture<AdminNavMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminNavMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminNavMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
