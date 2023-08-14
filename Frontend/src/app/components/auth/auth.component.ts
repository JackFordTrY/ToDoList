import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, concatMap, interval, take } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})
export class AuthComponent {
  constructor(private authService: AuthService, private router: Router) {}

  authForm: FormGroup = new FormGroup({
    username: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
      Validators.maxLength(20),
      Validators.pattern('[a-zA-Z0-9 ]*'),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.pattern('[a-zA-Z0-9 ]*'),
    ]),
  });

  isLogin: boolean = true;

  get username() {
    return this.authForm.get('username');
  }

  get password() {
    return this.authForm.get('password');
  }

  submit() {
    if (this.isLogin) {
      this.authService
        .login(this.username?.value, this.password?.value)
        .pipe(concatMap(() => interval(300).pipe(take(1))))
        .subscribe(() => this.router.navigate(['/list']));
    } else {
      this.authService
        .register(this.username?.value, this.password?.value)
        .subscribe(() => {
          this.authForm.reset();
          this.isLogin = !this.isLogin;
        });
    }
  }
}
