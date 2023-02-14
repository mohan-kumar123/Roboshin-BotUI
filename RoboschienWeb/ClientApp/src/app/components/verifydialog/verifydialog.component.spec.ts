import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VerifydialogComponent } from './verifydialog.component';

describe('VerifydialogComponent', () => {
  let component: VerifydialogComponent;
  let fixture: ComponentFixture<VerifydialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VerifydialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VerifydialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
