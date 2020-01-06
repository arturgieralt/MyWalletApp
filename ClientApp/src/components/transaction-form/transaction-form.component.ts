import { Component, ViewChild, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MatSnackBar } from "@angular/material/snack-bar";
import ApiResponse from "src/types/ApiResponse";
import { AccountService } from "src/services/account.service";
import { AccountSummary } from "src/types/AccountSummary";
import Category from "src/types/Category";
import { CategoryService } from "src/services/category.services";
import { TransactionService } from "src/services/transaction.service";
import AddTransactionRequest from "src/types/AddTransactionRequest";

@Component({
    selector: 'transaction-form',
    templateUrl: './transaction-form.component.html',
    styleUrls: ['./transaction-form.component.css']
})
export class TransactionFormComponent implements OnInit {

    @ViewChild('formRef', { static: false }) formRef;

    private accounts: AccountSummary[];
    private categories: Category[];

    private transactionForm = new FormGroup({
        name: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
        category: new FormControl(null, [Validators.required, Validators.min(1)]),
        account: new FormControl(null, [Validators.required, Validators.min(1)]),
        date: new FormControl(Date.now(), [Validators.required]),
        total: new FormControl(0, [Validators.required])
    })

    constructor(
        private accountService: AccountService, 
        private categoryService: CategoryService, 
        private notificationService: MatSnackBar,
        private transactionService: TransactionService
        ){}

    ngOnInit() {
        this.getCategories();
        this.getAccounts();
    }

    getCategories() {
        this.categoryService.getAll().subscribe(r => {
            this.categories = [...r.items];
        });
    }

    getAccounts() {
        this.accountService.getAll().subscribe(r => {
            this.accounts = [...r.items];
        });
    }

    onSubmit() {
        if(this.transactionForm.valid) {
            console.log('Adding')
            console.log(this.transactionForm)
        }
    }

    private afterSubmitAction = (r: ApiResponse) => {
        this.transactionForm.reset();
        this.formRef.resetForm();
        this.notificationService.open(r.message);
    }

}