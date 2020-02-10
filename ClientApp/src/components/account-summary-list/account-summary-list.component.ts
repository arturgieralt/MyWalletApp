import { Component, OnInit } from "@angular/core";
import { AccountService } from "src/services/account.service";
import { AccountSummary } from "src/types/AccountSummary";
import { Router } from "@angular/router";

@Component({
    selector: 'account-summary-list',
    styleUrls: ['./account-summary-list.component.css'],
    templateUrl: './account-summary-list.component.html'
})
export class AccountSummaryListComponent implements OnInit {
    public accounts: AccountSummary[] = [];

    constructor(private accountService: AccountService, private router: Router) {}

    ngOnInit() {
        this.getAccounts();
    }

    getAccounts() {
        this.accountService.getAll().subscribe(r => {
            this.accounts = [...r.items];
        })
    }

    deleteAccount(accountId: number) {
        this.accountService.delete(accountId).subscribe(() => {
            this.getAccounts();
        })
    }

    goToAccountDetails(accountId: number) {
        this.router.navigate(['accounts', accountId, 'transactions'])
    }
}