import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';

import { ProfileModel, UpdateProfileModel } from 'src/core/models';
import { ProfileService } from 'src/core/services/profile/profile.service';
import { FormValidationService } from 'src/core/services/formValidation.service';
import { UserRoles } from 'src/core/utilities/user-roles';
import { ValidationMessages } from 'src/core/utilities/validation-messages';
import { InputFileAccepts } from 'src/core/utilities/common-constants';
// import { InputFileAccepts } from 'src/app/utilities/common-constants';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.styl']
})
export class ProfileComponent implements OnInit {
  public userModel: ProfileModel;
  public updateUserModel = new UpdateProfileModel();
  public userRole: string;
  public isLoaded = false;
  public formErrors: any = {};
  public submitTouched = false;
  public updateForm: FormGroup;
  public inputFileAcceptImageTypes: string;

  constructor(
    private profileService: ProfileService,
    private formValidationService: FormValidationService) { }

  public ngOnInit(): void {
    this.setConstatns();
    this.uploadProfile();
  }

  public onSubmit(): void {
    this.submitTouched = true;
    if (this.updateForm.valid) {
      this.setValuesFromFormToModel();
      this.updateProfile();
    } else {
      this.formValidationService.markFormGroupTouched();
      // this.toastNotification.validationWarning();
      this.validateForm();
    }
  }

  private uploadProfile(): void {
    this.profileService.getProfle()
      .subscribe(result => {
          this.userModel = new ProfileModel()
          this.userModel.fullName = result.fullName;
          this.userModel.email = result.email;
          this.userModel.role = result.role;
          this.userModel.userId = result.userId;

          this.setUpdatedModel(result);
          this.createForm();
          this.isLoaded = true;
      });
  }

  private setUpdatedModel(model: ProfileModel): void {
    this.updateUserModel.userName = model.fullName.split(' ')[0];
    this.updateUserModel.userSurname = model.fullName.split(' ')[1];
    this.updateUserModel.email = model.email;
    this.updateUserModel.userRole = model.role;
  }

  private validateForm(): void {
    this.formErrors = this.formValidationService.validateForm();
  }

  private createForm(): void {
    this.updateForm = new FormGroup({
      userName: new FormControl(this.userModel.fullName, [Validators.required]),
      userMail: new FormControl(this.userModel.email,
        [
          Validators.required,
          Validators.email
        ])
    });
    this.formValidationService.setFormData(this.updateForm, ValidationMessages.UpdateProfile);
    this.updateForm.valueChanges.subscribe(() => this.validateForm());
  }

  private setValuesFromFormToModel(): void {
    const values = this.updateForm.getRawValue();
    this.updateUserModel.email = values.userMail;
    this.updateUserModel.userName = values.userName.split(' ')[0];
    this.updateUserModel.userSurname = values.userName.split(' ')[1];
    this.updateUserModel.userId = this.userModel.userId;
    this.updateUserModel.userRole = this.userModel.role;
  }

  private updateProfile(): void {
    this.profileService.updateProfle(this.updateUserModel)
      .subscribe(result => {
          this.userModel.fullName = result.fullName;
          this.userModel.email = result.email;

          // this.toastNotification.successMessage(NotificationMessages.Profile.SuccessUpdate);
      });
  }

  private setConstatns(): void {
    this.inputFileAcceptImageTypes = InputFileAccepts.imageTypes;
  }

  public uploadNewUserPicture(event: any): void {
    if (event.target.files.length > 0) {
        const file = event.target.files[0];

        const reader = new FileReader();
        reader.onload = () => this.userModel.userImage = reader.result;
        reader.readAsDataURL(file);

        this.updateUserModel.userImage = file;
    }
  }
}
