import { Component, EventEmitter, inject, Output } from '@angular/core';
import { UserService } from '../../user/services/user-service';
import { RouterLink } from "@angular/router";
import { AuthService } from '../../auth/auth-service';

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
    this.closeMenu.emit();
  }

  onNavigate() {
    this.closeMenu.emit();
  }


}
