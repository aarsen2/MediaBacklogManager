import { Component, inject, Injector, Input, input } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReadShowDto } from '../../../models/read/ReadShowDto';

@Component({
  selector: 'app-show-creation-form',
  imports: [ReactiveFormsModule],
  templateUrl: './show-creation-form.html',
  styleUrl: './show-creation-form.css',
})
export class ShowCreationForm {
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)
  @Input() show!: ReadShowDto | null;


  showForm = this.formBuilder.group({
    seasonCount: [0],
    episodeCount: [0],
    contentRating: ['TV_PG', Validators.required],
  });



  ngOnInit() {
    const parentForm: any = this.controlContainer.control as FormGroup;
    parentForm.addControl('show', this.showForm)
  if (this.show != null) {
      this.prefillFrom();
    }
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('show');
  }



  prefillFrom() {
    this.showForm.patchValue({seasonCount: this.show?.seasonCount})
    this.showForm.patchValue({episodeCount: this.show?.episodeCount})
    this.showForm.patchValue({contentRating: this.show?.contentRating})
  }
}
