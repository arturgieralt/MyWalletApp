import { Component, OnInit, OnDestroy } from "@angular/core";
import { Subscription } from "rxjs";
import { AccountService } from "src/services/account.service";
import { AccountSummary } from "src/types/AccountSummary";

@Component({
    selector: 'account-summary-list',
    styleUrls: ['./account-summary-list.component.css'],
    templateUrl: './account-summary-list.component.html'
})
export class AccountSummaryListComponent implements OnInit, OnDestroy {
    private accounts: AccountSummary[] = [];
    private getAccountsSubscription: Subscription;
    private columns: string[] = ['name', 'balance', 'transactionCount', 'currency', 'createdOn'];

    constructor(private accountService: AccountService) {}

    ngOnInit() {
        var observer = this.accountService.getAll();
        this.getAccountsSubscription = observer.subscribe(r => {
            this.accounts = [...r.items];
        })
    }

    ngOnDestroy() {
        this.getAccountsSubscription.unsubscribe();
    }
}