import { Component, inject } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-game-creation-form',
  imports: [ReactiveFormsModule],
  templateUrl: './game-creation-form.html',
  styleUrl: './game-creation-form.css',
})
export class GameCreationForm {
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)


  gameForm = this.formBuilder.group({
    studio: ['', [Validators.maxLength(100)]],
    contentRating: ['RP'],
    platformInput: [''],
    platforms: [[] as string[]],
  });



  ngOnInit() {
    const parentForm: any = this.controlContainer.control as FormGroup;
    parentForm.addControl('game', this.gameForm)
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('game');
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
