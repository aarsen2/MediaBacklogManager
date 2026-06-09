import { Component, inject } from '@angular/core';
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
  private userService = inject(UserService)
  private authService = inject(AuthService);
  displayName: string = this.userService.getUser()?.displayName ?? "User"; 
  

  logout() {
    this.authService.logout();
  }
}
