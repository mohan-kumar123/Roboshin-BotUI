import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FossLicenseComponent } from './foss-license.component';

describe('FossLicenseComponent', () => {
  let component: FossLicenseComponent;
  let fixture: ComponentFixture<FossLicenseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FossLicenseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FossLicenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
