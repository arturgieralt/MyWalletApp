import { Component, OnInit, OnDestroy } from "@angular/core";
import { Subscription } from "rxjs";
import { AccountService } from "src/services/account.service";
import { AccountSummary } from "src/types/AccountSummary";

@Component({
    selector: 'account-summary-list',
    templateUrl: './account-summary-list.component.html'
})
export class AccountSummaryListComponent implements OnInit, OnDestroy {
    private accounts: AccountSummary[] = [];
    private getAccountsSubscription: Subscription;

    constructor(private accountService: AccountService) {}

    ngOnInit() {
        var observer = this.accountService.getAll();
        this.getAccountsSubscription = observer.subscribe(r => {
            this.accounts = r.items;
        })
    }

    ngOnDestroy() {
        this.getAccountsSubscription.unsubscribe();
    }
}