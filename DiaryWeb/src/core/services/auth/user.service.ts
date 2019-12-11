import { Injectable } from '@angular/core';

import { User } from 'src/core/models/auth/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private UserIdentifier = 'user';

  constructor() { }

  public getUserFromLocalStorage(): User {
    return JSON.parse(localStorage.getItem(this.UserIdentifier));
  }

  public setUserInLocalStorage(user: User): void {
    localStorage.setItem(this.UserIdentifier, JSON.stringify(user));
  }
}
