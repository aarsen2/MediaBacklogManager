import { Component, inject, Injector } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-show-creation-form',
  imports: [ReactiveFormsModule],
  templateUrl: './show-creation-form.html',
  styleUrl: './show-creation-form.css',
})
export class ShowCreationForm {
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)


  showForm = this.formBuilder.group({
    seasonCount: [0],
    episodeCount: [0],
    contentRating: ['TV_PG', Validators.required],
  });



  ngOnInit() {
    const parentForm: any = this.controlContainer.control as FormGroup;
    parentForm.addControl('show', this.showForm)
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('show');
  }
}
