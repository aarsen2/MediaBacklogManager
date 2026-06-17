import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../../auth-service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {

  message = signal<string | null>(null)

  errorMessage: string = "Your Username or Password Was Incorrect"
  isLoggingIn: boolean = false;
  isLoggingOut: boolean = false;

  private authService = inject(AuthService)
  private formBuilder = inject(FormBuilder)
  private route = inject(ActivatedRoute)
  private router = inject(Router)


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
        const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
        this.router.navigate([returnUrl]);
      },
      error: (err) => {
        this.message.set(this.errorMessage);
        console.error('Failed To Log In');
        console.error(err.error)
      }
    });;

    this.isLoggingIn = false;
  }

}
