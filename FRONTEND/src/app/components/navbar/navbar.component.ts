import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account/account.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  sticky: boolean = false;

  constructor(public accountService: AccountService, private router: Router) { }

  logout(): void {
    this.accountService.logout();
    this.router.navigateByUrl('/')
  };

  @HostListener('window:scroll', ['$event'])
  onScroll(event: Event) {
    // this.sticky = window.pageYOffset >= 50;
  }

  //MOBILE NAV
  public isMenuOpen: boolean = false;
  public menuClass: string = "";

  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;
    this.menuClass = this.isMenuOpen ? "nav-open" : "";
  }

  public selectPage(): void {
    this.isMenuOpen = false;
    this.menuClass = "";
  }

}
