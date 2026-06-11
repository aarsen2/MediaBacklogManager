import { inject, Injectable, signal } from "@angular/core";
import { UserApi } from "./user-api";
import { UserDto } from "../models/UserDto";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private userApi = inject(UserApi);

  user = signal<UserDto | null>(null);

  init() {
    const token = localStorage.getItem('token');
    if (!token) return;

    this.loadUser();
  }

  clearUser() {
    localStorage.removeItem('token');
    this.user.set(null);
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