import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReCaptchaValidateComponent } from './recaptcha-validate.component';

describe('ReCaptchaValidateComponent', () => {
  let component: ReCaptchaValidateComponent;
  let fixture: ComponentFixture<ReCaptchaValidateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ReCaptchaValidateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReCaptchaValidateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
