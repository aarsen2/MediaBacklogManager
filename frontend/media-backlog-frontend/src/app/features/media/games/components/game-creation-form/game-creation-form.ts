import { Component, inject, Input } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReadGameDto } from '../../../models/read/ReadGameDto';

@Component({
  selector: 'app-game-creation-form',
  imports: [ReactiveFormsModule],
  templateUrl: './game-creation-form.html',
  styleUrl: './game-creation-form.css',
})
export class GameCreationForm {
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)
  @Input() game!: ReadGameDto | null;

  gameForm = this.formBuilder.group({
    studio: ['', [Validators.maxLength(100)]],
    contentRating: ['RP'],
    platformInput: [''],
    platforms: [[] as string[]],
  });



  ngOnInit() {
    const parentForm: any = this.controlContainer.control as FormGroup;
    parentForm.addControl('game', this.gameForm)
    if (this.game != null) {
      this.prefillFrom();
    }
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('game');
  }
  prefillFrom() {
    this.gameForm.patchValue({ studio: this.game?.studio })
    this.gameForm.patchValue({ platforms: this.getPlatforms() })
    this.gameForm.patchValue({ contentRating: this.game?.contentRating })
  }
  getPlatforms(): string[] {
    return this.game?.platforms?.map(g => g.name) ?? [];
  }





  addPlatform() {
    console.log("adding platform")
    const value = this.gameForm.value.platformInput?.trim();
    console.log(value)

    if (!value) return;

    const platforms = this.gameForm.value.platforms as string[];

    // prevent duplicates (case-insensitive)
    const exists = platforms.some(g => g.toLowerCase() === value.toLowerCase());
    if (exists) {
      this.gameForm.patchValue({
        platformInput: ''
      });
      return;
    }

    platforms.push(value);
    this.gameForm.patchValue({ platforms: platforms });

    this.gameForm.patchValue({ platformInput: '' });
  }

  removePlatform(platform: string) {
    const platforms = this.gameForm.value.platforms as string[];
    this.gameForm.patchValue({
      platforms: platforms.filter(g => g !== platform)
    });
  }

  // optional: allow comma to add
  handleKeydown(event: KeyboardEvent) {
    if (event.key === ',' || event.key === 'Enter') {
      event.preventDefault();
      this.addPlatform();
    }

  }

}
