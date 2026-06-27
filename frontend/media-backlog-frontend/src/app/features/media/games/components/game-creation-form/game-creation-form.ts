import { Component, HostListener, inject, Input, signal } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReadGameDto } from '../../../models/read/ReadGameDto';
import { toSignal } from '@angular/core/rxjs-interop';
import { MediaBacklogService } from '../../../../backlog/services/media-backlog-service';

@Component({
  selector: 'app-game-creation-form',
  imports: [ReactiveFormsModule],
  templateUrl: './game-creation-form.html',
  styleUrl: './game-creation-form.css',
})
export class GameCreationForm {


  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    const target = event.target as HTMLElement;
    if (!target.closest('.platforms-wrapper')) {
      this.filteredPlatforms.set([]);
    }
  }

  private backlogService = inject(MediaBacklogService)
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)
  @Input() game!: ReadGameDto | null;
  filteredPlatforms = signal<string[]>([]);
  possiblePlatforms = toSignal(
    this.backlogService.getPlatforms(),
    { initialValue: [] as string[] }
  )

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





  addPlatform(platform?: string | null) {
    console.log("adding platform")
    const value = !platform ? this.gameForm.value.platformInput?.trim() : platform;
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
    this.filteredPlatforms.set([]);

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


  onPlatformInput() {
    console.log(this.possiblePlatforms())
    const value = this.gameForm.value.platformInput?.toLowerCase() ?? "";

    if (!value) {
      this.filteredPlatforms.set([]);
      return;
    }
    const selected = this.gameForm.value.platforms as string[];

    this.filteredPlatforms.set(this.possiblePlatforms().filter(g =>
      g.toLowerCase().includes(value) && !selected.some(s => s.toLowerCase() === g.toLowerCase())
    ).sort((a, b) => a.localeCompare(b)));
  }

  selectFromList(genre: string) {
    this.addPlatform(genre);
    this.filteredPlatforms.set([]);
  }

}
