import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AgreeDiaglogComponent } from './agree-diaglog.component';

describe('AgreeDiaglogComponent', () => {
  let component: AgreeDiaglogComponent;
  let fixture: ComponentFixture<AgreeDiaglogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AgreeDiaglogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AgreeDiaglogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
