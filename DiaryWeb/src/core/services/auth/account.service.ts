import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import decode from 'jwt-decode';

import { TokenService } from './token.service';
import { UserService } from './user.service';
import { LoginResultModel, LoginModel, User, RefreshTokenModel } from 'src/core/models';
import { ApiRoutes } from 'src/core/utilities/api-routes';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  public user: User;

  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
    private userService: UserService
  ) { }

  public authenticate(model: LoginModel): Observable<LoginResultModel> {
    return this.http.post<LoginResultModel>(ApiRoutes.authenticate, model)
      .pipe(map(result => {
        this.updateLoginationData(result);
        return result;
      }));
  }

  public logOut(): void {
    this.user = null;
    this.tokenService.removeTokens();
  }

  public refreshToken(model: RefreshTokenModel): Observable<LoginResultModel> {
    return this.http.post<LoginResultModel>(`${ApiRoutes.refreshToken}`, model);
  }

  private updateLoginationData(loginResult: LoginResultModel): void {
    if (loginResult !== null) {
      const token = loginResult.accessToken;
      this.tokenService.setAccessToken(token);
      this.tokenService.setRefreshToken(loginResult.refreshToken);

      const user = {
        UserRole: decode(token).role,
        Id: loginResult.userId,
        UserName: loginResult.userName
      } as User;
      this.user = user;
      this.userService.setUserInLocalStorage(user);
    }
  }
}
