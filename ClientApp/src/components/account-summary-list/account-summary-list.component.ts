import { Component, OnInit } from "@angular/core";
import { AccountService } from "src/services/account.service";
import { AccountSummary } from "src/types/AccountSummary";

@Component({
    selector: 'account-summary-list',
    styleUrls: ['./account-summary-list.component.css'],
    templateUrl: './account-summary-list.component.html'
})
export class AccountSummaryListComponent implements OnInit {
    private accounts: AccountSummary[] = [];
    private columns: string[] = ['name', 'balance', 'transactionCount', 'currency', 'createdOn'];

    constructor(private accountService: AccountService) {}

    ngOnInit() {
        this.accountService.getAll().subscribe(r => {
            this.accounts = [...r.items];
        })
    }
}