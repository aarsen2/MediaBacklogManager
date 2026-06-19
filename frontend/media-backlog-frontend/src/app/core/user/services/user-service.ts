import { inject, Injectable, signal } from "@angular/core";
import { UserApi } from "./user-api";
import { UserDto } from "../models/UserDto";
import { AuthService } from "../../auth/auth-service";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private userApi = inject(UserApi);
  private authService = inject(AuthService)

  user = signal<UserDto | null>(null);


  constructor() {
    this.init();
  }

  init() {
    const token = this.authService.getToken();
    if (!token) return;
    this.loadUser();

  }

  clearUser() {
    localStorage.removeItem('token');
    this.user.set(null);
  }

  getUsername(): string{

    if (this.user == null)
    {
      this.loadUser();
    }    
    return this.user()?.username ?? "";
  }

  loadUser() {
    this.userApi.getUser().subscribe({
      next: (user) => {
        this.user.set(user)
        console.log(user);
      },
      error: () => this.clearUser()
    });
  }
}