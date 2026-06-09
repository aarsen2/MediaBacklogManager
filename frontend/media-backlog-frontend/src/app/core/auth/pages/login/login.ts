import { Component, inject } from '@angular/core';
import { AuthService } from '../../auth-service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {

  message: string = "";
  loginMessage: string = "You have logged in"
  logoutMessage: string = "You have logged out"
  isLoggingIn: boolean = false;
  isLoggingOut: boolean = false;
  private authService = inject(AuthService)
  private formBuilder = inject(FormBuilder)


  loginForm = this.formBuilder.group({
    username: ['', [Validators.required]],
    password: ['', [Validators.required]],
  });



  login() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }
    const formValue = this.loginForm.getRawValue();

    this.isLoggingIn = true;
    const username: string = formValue.username?.trim().toLowerCase() ?? "";
    const password: string = formValue.password ?? ""

    console.log(username);
    console.log(password);
    this.authService.login(username, password).subscribe({
      next: (res) => {
        console.log('Logged In');
        console.log(res)
      },
      error: (err) => {
        console.error('Failed To Log In');
        console.error(err.error)
      }
    });;

    this.message = this.loginMessage
    this.isLoggingIn = false;
  }

  logout() {
    this.isLoggingOut = true;
    this.authService.logout();
    this.message = this.logoutMessage
    this.isLoggingOut = false;
  }
}
