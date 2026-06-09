import { Component, ElementRef, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { ProfileMenu } from '../profile-menu/profile-menu';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-nav-bar',
  imports: [RouterLink, RouterLinkActive, ProfileMenu],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.css',
})
export class NavBar {
  profileMenuVisable: boolean = false;



  profileButton() {
    this.profileMenuVisable = !this.profileMenuVisable
  }
  closeProfile() {
    this.profileMenuVisable = false;
  }
}
