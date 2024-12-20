import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthResponse } from 'src/app/models/auth/authResponse';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  isLogin: boolean = true;
  loginForm!: FormGroup;
  returnUrl: string | null = null;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: new FormControl<string>('', [
        Validators.required,
        Validators.email,
      ]),
      password: new FormControl('', [Validators.required]),
    });
    this.route.queryParams.subscribe((params) => {
      this.returnUrl = params['returnUrl'];
    });
    if (this.authService.isAuthorized()) this.redirectToAuthorizedUrl();
  }

  redirectToRegister() {
    if (!!this.returnUrl)
      this.router.navigateByUrl(
        `/auth/registration?returnUrl=${this.returnUrl}`
      );
    else this.router.navigateByUrl(`/auth/registration`);
  }

  submitForm() {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe(
        (response: AuthResponse) => {
          localStorage.setItem('token', response.data);
          this.redirectToAuthorizedUrl();
        },
        (error: any) => {
          console.log(error);
        }
      );
    } else {
      console.log(this.loginForm);
      console.log('Form is invalid!');
      this.loginForm.markAllAsTouched(); // Mark all controls as touched to display validation errors
    }
  }

  redirectToAuthorizedUrl() {
    if (!!this.returnUrl) this.router.navigateByUrl(`${this.returnUrl}`);
    else this.router.navigateByUrl('/personal-expense');
    // if (!!this.returnUrl)
    // this.router.navigateByUrl(
    //   `/?returnUrl=${this.returnUrl}`
    // );
    // else
    // this.router.navigateByUrl('/');
    // console.log("redirectToAuthorizedUrl", this.returnUrl)

  }
}
