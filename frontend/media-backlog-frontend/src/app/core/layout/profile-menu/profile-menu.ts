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
  
getName() {
  let username = this.userService.user()?.displayName

  if (username == null) {
    username = this.userService.user()?.username;
  }
  if (username == null) {
    username = "User"
  }
  
  return username;
}

}
