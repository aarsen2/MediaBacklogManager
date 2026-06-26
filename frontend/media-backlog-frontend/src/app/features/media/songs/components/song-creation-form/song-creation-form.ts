import { Component, inject, Input } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReadSongDto } from '../../../models/read/ReadSongDto';

@Component({
  selector: 'app-song-creation-form',
  imports: [ReactiveFormsModule],
  templateUrl: './song-creation-form.html',
  styleUrl: './song-creation-form.css',
})
export class SongCreationForm {
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)
  @Input() song!: ReadSongDto | null;

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

    if (this.song != null) {
      this.prefillFrom();
    }
  }

  prefillFrom() {
    this.songForm.patchValue({ artist: this.song?.artist })
    this.songForm.patchValue({ runTime: this.song?.runTime })
  }

}
