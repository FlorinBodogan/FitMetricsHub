import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RegisterUser } from 'src/app/interfaces/account/registerUser';
import { AccountService } from 'src/app/services/account/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup = new FormGroup({});
  submitted = false;

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  };

  initializeForm(): void {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(15), Validators.pattern(/^[a-z][a-z0-9_-]{2,14}$/)]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20), Validators.pattern(/^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])/)]],
    });
  };

  checkClassInvalid(param: string, submitted: boolean) {
    return this.registerForm.get(param)?.errors && this.registerForm.get(param)?.touched || this.registerForm.get(param)?.errors && submitted ? true : false;
  };

  register(): void {
    if (this.registerForm.invalid) {
      this.submitted = true;
      this.getErrorsMessage();
      return;
    }

    const newUser = {
      ...this.registerForm.value,
    } as RegisterUser;

    this.accountService.register(newUser).subscribe({
      next: () => {
        this.router.navigateByUrl('/login');
        this.toastr.success("Account creation successfully!");

      },
      error: (err) => {
        this.toastr.error(err.error)
      }
    });
  };

  getErrorsMessage(): void {
    this.getErrorsMessageUsername();
    this.getErrorsMessagePassword();
  };

  getErrorsMessageUsername(): string {
    if (this.registerForm.get('username')?.hasError('required')) {
      return 'Please enter an username.';
    } else if (this.registerForm.get('username')?.hasError('minlength')) {
      return "Username it's too short.";
    } else if (this.registerForm.get('username')?.hasError('maxlength')) {
      return "Username it's too long.";
    } else if (this.registerForm.get('username')?.hasError('pattern')) {
      return "Username must contain only lowercase letters.";
    } else return '';
  };

  getErrorsMessagePassword(): string {
    if (this.registerForm.get('password')?.hasError('required')) {
      return 'Please enter a password.';
    } else if (this.registerForm.get('password')?.hasError('minlength')) {
      return "Password it's too short.";
    } else if (this.registerForm.get('password')?.hasError('maxlength')) {
      return "Password it's too long.";
    } else if (this.registerForm.get('password')?.hasError('pattern')) {
      return "Password must contain at least one uppercase letter, one digit, and one special character.";
    } else return '';
  };
}
