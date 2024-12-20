import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthResponse } from 'src/app/models/auth/authResponse';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  returnUrl: string | null = null;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(8),
      ]),
      confirmPassword: new FormControl('', [Validators.required]),
    });

    this.route.queryParams.subscribe((params) => {
      this.returnUrl = params['returnUrl'];
    });

    if (this.authService.isAuthorized()) this.redirectToAuthorizedUrl();
  }

  redirectToLogin() {
    if (!!this.returnUrl) {
      this.router.navigateByUrl(`/auth/login?returnUrl=${this.returnUrl}`);
    } else this.router.navigateByUrl(`/auth/login`);
  }

  passwordMatchValidator(control: FormControl): ValidationErrors | null {
    if (this.registerForm == undefined || this.registerForm == null)
      return null;

    return control.value === this.registerForm.get('password')?.value
      ? null
      : { mismatch: true };
  }

  submitForm() {
    if (this.registerForm.valid) {
      console.log('Form Value:', this.registerForm.value);
      this.authService.signup(this.registerForm.value).subscribe(
        (response: AuthResponse) => {
          console.log(response);
          localStorage.setItem('token', response.data);
          this.redirectToAuthorizedUrl();
        },
        (error: any) => {
          console.log(error);
        }
      );
    } else {
      console.log(this.registerForm);
      console.log('Form is invalid!');
      this.registerForm.markAllAsTouched(); // Mark all controls as touched to display validation errors
    }
  }

  redirectToAuthorizedUrl() {
    if (!!this.returnUrl) this.router.navigateByUrl(`${this.returnUrl}`);
    else this.router.navigateByUrl('/personal-expense');
  }
}
