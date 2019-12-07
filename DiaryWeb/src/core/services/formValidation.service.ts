import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
    providedIn: 'root'
})
export class FormValidationService {
    private validationMessages: any;
    private form: FormGroup;

    public setFormData(form: FormGroup, validationMessages: any): void {
        this.validationMessages = validationMessages;
        this.form = form;
    }

    public markFormGroupTouched(): void {
        Object.values(this.form.controls).forEach(element => {
            element.markAsTouched();
        });
    }

    public validateForm(): any {
        const formErrors: any = {};

        Object.keys(this.form.controls).forEach(controlName => {
            const control = this.form.get(controlName);
            if ((control.invalid && (control.dirty || control.touched))) {
                const messages = this.validationMessages[controlName];

                // tslint:disable-next-line:forin
                for (const key in control.errors) {
                    formErrors[controlName] = messages[key];
                    break;
                }
            }
        });

        return formErrors;
    }
}
