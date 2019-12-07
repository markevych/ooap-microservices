import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';

import { FormValidationService } from 'src/core/services/formValidation.service';
import { AccountService } from 'src/core/services/auth/account.service';
import { TokenService } from 'src/core/services/auth/token.service';
import { ResetPasswordModel } from 'src/core/models/auth/reset-password.model';
import { MustMatch } from 'src/core/utilities/validators/must-match.validator';
import { ValidationMessages } from 'src/core/utilities/validation-messages';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.styl']
})
export class ResetPasswordComponent implements OnInit {
  public formErrors: any = {};
  public submitTouched = false;
  public updateForm: FormGroup;
  public resetModel = new ResetPasswordModel();
  private formValidationService = new FormValidationService();

  constructor(
    private tokenService: TokenService,
    private accountService: AccountService,
    //private toastNotification: ToastNotificationService,
  ) { }

  public ngOnInit(): void {
    this.createForm();
  }

  public onSubmit(): void {
    this.submitTouched = true;
    if (this.updateForm.valid) {
      this.setValuesFromFormToModel();
      this.resetModel.RefreshToken = this.tokenService.getRefreshToken();
      this.accountService.resetPassword(this.resetModel)
        .subscribe(result => {
          if (result) {
            //this.toastNotification.successMessage('You`ve successfully update your password!');
          } else {
            //this.toastNotification.showApiErrorMessage(result);
          }
        });
    } else {
      this.formValidationService.markFormGroupTouched();
      //this.toastNotification.validationWarning();
      this.validateForm();
    }
  }

  private validateForm(): void {
    this.formErrors = this.formValidationService.validateForm();
  }

  private createForm(): void {
    this.updateForm = new FormGroup({
      oldPassword: new FormControl('', [Validators.required]),
      newPassword: new FormControl('', [Validators.required]),
      repeatPassword: new FormControl('', [Validators.required]),
    }, { validators: MustMatch('newPassword', 'repeatPassword') });
    this.formValidationService.setFormData(this.updateForm, ValidationMessages.ResetPassword);
    this.updateForm.valueChanges.subscribe(() => this.validateForm());
  }

  private setValuesFromFormToModel(): void {
    const values = this.updateForm.getRawValue();
    this.resetModel.OldPassword = values.oldPassword;
    this.resetModel.NewPassword = values.newPassword;
  }
}
