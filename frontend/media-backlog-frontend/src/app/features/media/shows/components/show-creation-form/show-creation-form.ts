import { Component, effect, inject, Injector, Input, input, runInInjectionContext, Signal } from '@angular/core';
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
  private controlContainer = inject(ControlContainer);
  private injector = inject(Injector);

  @Input() show!: Signal<ReadShowDto | null>;

  showForm = this.formBuilder.group({
    seasonCount: [0],
    episodeCount: [0],
    contentRating: ['TV_PG', Validators.required],
  });

  ngOnInit() {
    const parentForm = this.controlContainer.control as FormGroup;
    parentForm.addControl('show', this.showForm);

    runInInjectionContext(this.injector, () => {
      effect(() => {
        const show = this.show();

        if (show) {
          this.prefillFrom(show);
        }
      });
    });
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('show');
  }

  prefillFrom(show: ReadShowDto) {
    this.showForm.patchValue({
      seasonCount: show.seasonCount ?? 0,
      episodeCount: show.episodeCount ?? 0,
      contentRating: show.contentRating ?? 'TV_PG'
    });
  }
}