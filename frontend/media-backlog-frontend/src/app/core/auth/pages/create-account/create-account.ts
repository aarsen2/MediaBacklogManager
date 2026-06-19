import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../../auth-service';
import { AbstractControl, FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { RegisterDto } from '../../models/RegisterDto';
import { AuthResponse } from '../../models/AuthResponse';

@Component({
  selector: 'app-create-account',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './create-account.html',
  styleUrl: './create-account.css',
})
export class CreateAccount {

  errorMessages = signal<string[] | null>(null)

  errorMessage: string = "Your Username or Password Was Incorrect"
  isCreating: boolean = false;

  private authService = inject(AuthService)
  private formBuilder = inject(FormBuilder)
  private route = inject(ActivatedRoute)
  private router = inject(Router)

  registerForm = this.formBuilder.group({
    displayName: [''],
    email: ['', [Validators.required, Validators.email]],
    username: ['', Validators.required],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
  }, {
    validators: this.passwordMatchValidator
  });



  register() {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }
    const formValue = this.registerForm.getRawValue();

    this.isCreating = true;
    const displayName: string = formValue.displayName ?? ""
    const email: string = formValue.email ?? ""
    const username: string = formValue.username?.trim().toLowerCase() ?? "";
    const password: string = formValue.password ?? ""
    const confirmPassword: string = formValue.confirmPassword ?? ""

    const dto: RegisterDto = {
      displayName: displayName,
      email: email,
      username: username,
      password: password
    }
    
    this.authService.register(dto).subscribe({
      next: (res) => {
        console.log('Logged In');
        console.log(res)
        const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
        this.router.navigate([returnUrl]);
      },
      error: (err) => {
        const error = err.error as AuthResponse
        console.error('Failed To Log In');
        console.error(error.errors)
        this.errorMessages.set(error.errors);
      }
    });;

    this.isCreating = false;
  }


  passwordMatchValidator(form: AbstractControl) {
    const password = form.get('password')?.value;
    const confirm = form.get('confirmPassword')?.value;

    return password === confirm ? null : { passwordMismatch: true };
  }


}
