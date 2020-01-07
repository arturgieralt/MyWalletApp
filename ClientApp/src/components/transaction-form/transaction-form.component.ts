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
import { ActivatedRoute, ParamMap } from "@angular/router";
import { switchMap } from "rxjs/operators";
import TransactionType, { getTransactionTypes, ITransactionType } from "src/types/TransactionType";

@Component({
    selector: 'transaction-form',
    templateUrl: './transaction-form.component.html',
    styleUrls: ['./transaction-form.component.css']
})
export class TransactionFormComponent implements OnInit {

    @ViewChild('formRef', { static: false }) formRef;

    private accounts: AccountSummary[];
    private categories: Category[];
    private accountId: string;

    private transactionForm = new FormGroup({
        name: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
        category: new FormControl(null, [Validators.required, Validators.min(1)]),
        account: new FormControl(null, [Validators.required, Validators.min(1)]),
        date: new FormControl(new Date(), [Validators.required]),
        total: new FormControl(0, [Validators.required]),
        type: new FormControl(0, [Validators.required])
    })

    constructor(
        private accountService: AccountService, 
        private categoryService: CategoryService, 
        private notificationService: MatSnackBar,
        private routeService: ActivatedRoute,
        private transactionService: TransactionService
        ){}

    ngOnInit() {
        this.getAccountId();
        this.getCategories();
        this.getAccounts();
    }

    getAccountId() {
        this.routeService.paramMap
            .pipe(
                switchMap((params: ParamMap) => params.get('id'))
            ).subscribe(id => {
                this.accountId = id;
                this.transactionForm.patchValue({
                    account: this.accountId
                });
            });
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
            const {
                name,
                category,
                date,
                total,
                type,
                account
            } = this.transactionForm.value;

            const transactionRequest = new AddTransactionRequest(
                name,
                Number(account),
                date,
                Number(total),
                Number(type),
                !isNaN(category) && Number(category)
            );

            this.transactionService
                .add(transactionRequest)
                .subscribe(this.afterSubmitAction);
        }
    }

    private afterSubmitAction = (r: ApiResponse) => {
        this.transactionForm.reset();
        this.formRef.resetForm();
        this.notificationService.open(r.message);
    }

}