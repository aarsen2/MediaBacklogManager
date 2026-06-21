import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowCreationForm } from './show-creation-form';

describe('ShowCreationForm', () => {
  let component: ShowCreationForm;
  let fixture: ComponentFixture<ShowCreationForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShowCreationForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowCreationForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
