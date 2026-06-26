import { Component, ElementRef, HostListener, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ProfileMenu } from '../profile-menu/profile-menu';
import { DOCUMENT } from '@angular/common';
import { AuthService } from '../../auth/auth-service';

@Component({
  selector: 'app-nav-bar',
  imports: [RouterLink, RouterLinkActive, ProfileMenu],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.css',
})
export class NavBar {
  profileMenuVisable: boolean = false;
  private readonly router = inject(Router)
  public readonly authService = inject(AuthService)



  profileButton() {
    this.profileMenuVisable = !this.profileMenuVisable
  }
  closeProfile() {
    this.profileMenuVisable = false;
  }

  search(searchInput: string) {
    this.router.navigate(['/search/'], {
      queryParams: {q: searchInput.trim()}
    })
  }



  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    const target = event.target as HTMLElement;

    const clickedInside = target.closest('.profileWrapper')

    if(!clickedInside) {
      this.profileMenuVisable = false;
    }
  }
}
