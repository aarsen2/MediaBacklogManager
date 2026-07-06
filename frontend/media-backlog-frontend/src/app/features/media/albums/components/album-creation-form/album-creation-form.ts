import { Component, effect, inject, Injector, Input, runInInjectionContext, Signal } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ReadAlbumDto } from '../../../models/read/ReadAlbumDto';

@Component({
  selector: 'app-album-creation-form',
  imports: [ReactiveFormsModule],
  templateUrl: './album-creation-form.html',
  styleUrl: './album-creation-form.css',
})
export class AlbumCreationForm {
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)
  private injector = inject(Injector);
  @Input() album!: Signal<ReadAlbumDto | null>;

  albumForm = this.formBuilder.group({
    artist: [''],
    trackCount: [0],
    runTime: [0],
  });



  ngOnInit() {
    const parentForm: any = this.controlContainer.control as FormGroup;
    parentForm.addControl('album', this.albumForm)
    if (this.album != null) {
      this.prefillFrom();
    }


    runInInjectionContext(this.injector, () => {
      effect(() => {
        const album = this.album();

        if (album) {
          this.prefillFrom();
        }
      })
    })
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('album');
  }
  prefillFrom() {
    this.albumForm.patchValue({ artist: this.album()?.artist })
    this.albumForm.patchValue({ runTime: this.album()?.runTime })
    this.albumForm.patchValue({ trackCount: this.album()?.trackCount })
  }
}
