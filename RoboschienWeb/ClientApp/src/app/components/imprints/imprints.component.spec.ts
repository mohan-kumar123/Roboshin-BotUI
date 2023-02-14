import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ImprintsComponent } from './imprints.component';

describe('ImprintsComponent', () => {
  let component: ImprintsComponent;
  let fixture: ComponentFixture<ImprintsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ImprintsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ImprintsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
