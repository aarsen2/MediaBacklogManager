import { Component, EventEmitter, inject, Output, signal } from '@angular/core';
import { RouterLink } from "@angular/router";
import { AuthService } from '../../auth/auth-service';
import { UserService } from '../../user/services/user-service';

@Component({
  selector: 'app-profile-menu',
  imports: [RouterLink],
  templateUrl: './profile-menu.html',
  styleUrl: './profile-menu.css',
})
export class ProfileMenu {
  @Output() closeMenu = new EventEmitter<void>();
  userService = inject(UserService)
  private authService = inject(AuthService);
  
  logout() {
    this.authService.logout();
    this.userService.clearUser();
    this.closeMenu.emit();
  }
  
  onNavigate() {
    this.closeMenu.emit();
  }
  
  // username = signal<string>("");
  // constructor() {
  //   this.username.set(this.userService.getUsername());
  //   console.log(this.username())
  // }


}
