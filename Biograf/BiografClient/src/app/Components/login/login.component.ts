import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { race } from 'rxjs';
import { Login } from 'src/app/Models/Login';
import { GenericService } from 'src/app/Services/generic.service';
import { LoginService } from 'src/app/Services/login.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  // login?:Login
  // login?: Login;
  Form: FormGroup = this.resetForm();

  error = '';
  constructor(private route: ActivatedRoute,
    private router: Router,
    // maybe formBuilder
    // private authService: GenericService<User>) {}
    private authService: LoginService) {}


  ngOnInit()
  {
    if (this.authService.currentUserValue != null && this.authService.currentUserValue.id > 0) {
      this.router.navigate(['/']);
    }
  }
  resetForm(): FormGroup{
    return new FormGroup ({
      email: new FormControl('', Validators.required),
      password: new FormControl('',Validators.required),
    });
  }

  onSubmit(): void {
    this.error = ''
    console.log(this.Form.value);
    // this.login = this.Form.value;
    const email = this.Form.get('email')?.value;
    const password = this.Form.get('password')?.value;

    this.authService.login(email, password)
    // this.authService.login(this.Form.get('email')?.value, this.Form.get('password')?.value)
      .subscribe({
        next: () => {
          // get return url from route parameters or default to '/'
          let returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
          this.router.navigate([returnUrl]);
        },
        error: err => {
          if (err.error?.status == 400 || err.error?.status == 401 || err.error?.status == 500) {
            this.error = 'Incorrect Username or Password';
          }
          else {
            this.error = err.error.title;
          }
        }
      });
  }
  // logout():void{

  // }

}
