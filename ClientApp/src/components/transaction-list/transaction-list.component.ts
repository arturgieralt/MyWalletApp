import { Component, OnInit } from "@angular/core";
import Transaction from "src/types/Transaction";
import { TransactionService } from "src/services/transaction.service";
import { ActivatedRoute, ParamMap } from "@angular/router";
import { switchMap } from "rxjs/operators";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
    selector: 'transaction-list',
    styleUrls: ['./transaction-list.component.css'],
    templateUrl: './transaction-list.component.html'
})
export class TransactionListComponent implements OnInit {
    public transactions: Transaction[] = [];
    public transactionForm = new FormGroup({
        type: new FormControl(0, [Validators.required])
    })

    constructor(
        private transactionService: TransactionService,
        private routeService: ActivatedRoute) {}

    ngOnInit() {
        this.getTransactions();
    }

    getTransactions() {
        this.routeService.paramMap.pipe(
            switchMap((params: ParamMap) => {
                const accountId = params.get('id');
                return this.transactionService.getAll(accountId);
            })
        ).subscribe(r => {
            this.transactions = [...r.items];
        });
    }

    deleteTransaction(transactionId: number) {
        this.transactionService.delete(transactionId).subscribe(() => {
            this.getTransactions();
        })
    }
}