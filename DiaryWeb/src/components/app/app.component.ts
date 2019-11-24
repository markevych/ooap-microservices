import { Component, OnInit } from '@angular/core';
import { Router, NavigationStart, NavigationEnd } from '@angular/router';
import { Subscription } from 'rxjs';

import { TokenService } from 'src/core/services/auth/token.service';
import { AccountService } from 'src/core/services/auth/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.styl']
})
export class AppComponent implements OnInit {
  private subscription: Subscription;

  constructor(
    private router: Router,
    private tokenService: TokenService,
    private accountService: AccountService) { }

  public ngOnInit(): void {
  }

  public logOutUser(): void {
    this.accountService.logOut();
    this.router.navigate(['/login']);
  }

  public get isAuthenticated(): boolean {
    return this.tokenService.getAccessToken() !== null;
  }
}
