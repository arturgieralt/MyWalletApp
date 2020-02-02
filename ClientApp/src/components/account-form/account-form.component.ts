import { Component, ViewChild, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MatSnackBar } from "@angular/material/snack-bar";
import ApiResponse from "src/types/ApiResponse";
import { AddAccountRequest } from "src/types/AddAccountRequest";
import { Currency } from "src/types/Currency";
import { AccountService } from "src/services/account.service";
import { CurrencyService } from "src/services/currency.service";

@Component({
    selector: 'account-form',
    templateUrl: './account-form.component.html',
    styleUrls: ['./account-form.component.css']
})
export class AccountFormComponent implements OnInit {

    @ViewChild('formRef', { static: false }) formRef;

    private currencies: Currency[];

    private accountForm = new FormGroup({
        name: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
        currency: new FormControl(null, [Validators.required, Validators.min(1)])
    })

    constructor(
        private accountService: AccountService, 
        private currencyService: CurrencyService, 
        private notificationService: MatSnackBar
        ){}

    ngOnInit() {
        this.currencyService.getAll().subscribe(r => {
            this.currencies = [...r.items];
        })
    }

    onSubmit() {
        if(this.accountForm.valid) {
            const request = new AddAccountRequest(this.accountForm.value.name, this.accountForm.value.currency)
            this.accountService
                .add(request)
                .subscribe(this.afterSubmitAction);
        }
    }

    private afterSubmitAction = (r: ApiResponse) => {
        this.accountForm.reset();
        this.formRef.resetForm();
        this.notificationService.open(r.message);
    }

}