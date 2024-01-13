import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/services/account/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginUser: any = {};

  constructor(private accountService: AccountService, private router: Router, private toastr: ToastrService,) { }

  login(): void {
    this.accountService.login(this.loginUser).subscribe({
      next: () => {
        this.router.navigateByUrl('/members')
        this.toastr.success("You logged in successfully!")
      },
      error: err => this.toastr.error(err.error)
    })
  };
}
