import { Component, inject } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-song-creation-form',
  imports: [ReactiveFormsModule],
  templateUrl: './song-creation-form.html',
  styleUrl: './song-creation-form.css',
})
export class SongCreationForm {
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)


  songForm = this.formBuilder.group({
    artist: ['', [Validators.maxLength(100)]],
    runTime: [0],
  });



  ngOnInit() {
    const parentForm: any = this.controlContainer.control as FormGroup;
    parentForm.addControl('song', this.songForm)
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('song');
  }


}
