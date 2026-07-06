import { Component, effect, inject, Injector, Input, runInInjectionContext, Signal } from '@angular/core';
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
  private injector = inject(Injector)
  @Input() song!: Signal<ReadSongDto | null>;

  songForm = this.formBuilder.group({
    artist: ['', [Validators.maxLength(100)]],
    runTime: [0],
  });



  ngOnInit() {
    const parentForm: any = this.controlContainer.control as FormGroup;
    parentForm.addControl('song', this.songForm)
    if (this.song != null) {
      this.prefillFrom();
    }

    runInInjectionContext(this.injector, () => {
      effect(() => {
        const song = this.song();

        if (song) {
          this.prefillFrom();
        }
      })
    })
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('song');

  }

  prefillFrom() {
    this.songForm.patchValue({ artist: this.song()?.artist })
    this.songForm.patchValue({ runTime: this.song()?.runTime })
  }

}
