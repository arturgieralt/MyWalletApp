import { Component, OnInit } from "@angular/core";
import Transaction from "src/types/Transaction";
import { TransactionService } from "src/services/transaction.service";
import { ActivatedRoute, ParamMap } from "@angular/router";
import { switchMap } from "rxjs/operators";

@Component({
    selector: 'transaction-list',
    styleUrls: ['./transaction-list.component.css'],
    templateUrl: './transaction-list.component.html'
})
export class TransactionListComponent implements OnInit {
    private transactions: Transaction[] = [];
    private columns: string[] = ['name', 'total', 'category', 'date', 'transactionType'];

    constructor(
        private transactionService: TransactionService,
        private routeService: ActivatedRoute) {}

    ngOnInit() {
        this.getTransactions().subscribe(r => {
            this.transactions = [...r.items];
        })
    }

    getTransactions() {
        return this.routeService.paramMap.pipe(
            switchMap((params: ParamMap) => {
                const accountId = params.get('id');
                return this.transactionService.getAll(accountId);
            })
        );
    }
}