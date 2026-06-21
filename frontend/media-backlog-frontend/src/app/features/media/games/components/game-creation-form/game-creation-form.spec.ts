import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameCreationForm } from './game-creation-form';

describe('GameCreationForm', () => {
  let component: GameCreationForm;
  let fixture: ComponentFixture<GameCreationForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GameCreationForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GameCreationForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
