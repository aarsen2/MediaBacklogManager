import { Component, inject } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-album-creation-form',
  imports: [ReactiveFormsModule],
  templateUrl: './album-creation-form.html',
  styleUrl: './album-creation-form.css',
})
export class AlbumCreationForm {
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)


  albumForm = this.formBuilder.group({
    artist: [''],
    trackCount: [0],
    runTime: [0],
  });



  ngOnInit() {
    const parentForm: any = this.controlContainer.control as FormGroup;
    parentForm.addControl('album', this.albumForm)
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('album');
  }
}
