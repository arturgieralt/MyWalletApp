import { Component, OnInit, OnDestroy } from "@angular/core";
import { Subscription } from "rxjs";
import { AccountService } from "src/services/account.service";
import { AccountSummary } from "src/types/AccountSummary";
import Transaction from "src/types/Transaction";
import { TransactionService } from "src/services/transaction.service";
import { ActivatedRoute, ParamMap } from "@angular/router";
import { switchMap } from "rxjs/operators";

@Component({
    selector: 'transaction-list',
    styleUrls: ['./transaction-list.component.css'],
    templateUrl: './transaction-list.component.html'
})
export class TransactionListComponent implements OnInit, OnDestroy {
    private transactions: Transaction[] = [];
    private getTransactionSubscription: Subscription;
    private columns: string[] = ['name', 'total', 'category', 'date', 'transactionType'];

    constructor(
        private transactionService: TransactionService,
        private routeService: ActivatedRoute) {}

    ngOnInit() {
        const transactions$ = this.getTransactionStream();
        this.getTransactionSubscription = transactions$.subscribe(r => {
            this.transactions = [...r.items];
        })
    }

    ngOnDestroy() {
        this.getTransactionSubscription.unsubscribe();
    }

    getTransactionStream() {
        return this.routeService.paramMap.pipe(
            switchMap((params: ParamMap) => {
                const accountId = params.get('id');
                return this.transactionService.getAll(accountId);
            })
        );
    }
}