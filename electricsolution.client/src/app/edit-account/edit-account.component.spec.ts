import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAccountComponent } from './edit-account.component';

describe('ViewMyAccountComponent', () => {
  let component: MyAccountComponent;
  let fixture: ComponentFixture<MyAccountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyAccountComponent]
    });
    fixture = TestBed.createComponent(MyAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
