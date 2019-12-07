import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { ProfileModel, UpdateProfileModel } from '../../models';
import { ApiRoutes } from 'src/core/utilities/api-routes';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  constructor(private http: HttpClient) { }

  public getProfle(): Observable<ProfileModel> {
    return this.http.get<ProfileModel>(ApiRoutes.userProfile);
  }

  public updateProfle(updateModel: UpdateProfileModel): Observable<ProfileModel> {
    return this.http.put<ProfileModel>(ApiRoutes.userProfile, updateModel);
  }
}
