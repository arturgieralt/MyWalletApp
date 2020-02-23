import { Component, OnInit, ViewChild } from "@angular/core";
import { map } from "rxjs/operators";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { AccountInvite } from "src/types/AccountInvite";
import { AccountInviteService } from "src/services/accountInvite.service";
import { AuthorizeService } from "src/api-authorization/authorize.service";
import { AddAccountInviteRequest } from "src/types/AddAccountInviteRequest";
import ApiResponse from "src/types/ApiResponse";
import { AccountService } from "src/services/account.service";
import { ToastrService } from "ngx-toastr";
import { AccountSummary } from "src/types/AccountSummary";

@Component({
    selector: 'account-invite-list',
    styleUrls: ['./account-invite-list.component.css'],
    templateUrl: './account-invite-list.component.html'
})
export class AccountInviteListComponent implements OnInit {
    @ViewChild('formRef', { static: false }) formRef;
    
    public accounts: AccountSummary[];
    public accountInvitesSent: AccountInvite[] = [];
    public accountInvitesReceived: AccountInvite[] = [];
    public userName: string;

    public inviteForm = new FormGroup({
        email: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(100)]),
        account: new FormControl(null, [Validators.required, Validators.min(1)]),
        transactionWrite: new FormControl(true, [Validators.required]),
        transactionRead: new FormControl(true, [Validators.required]),
        accountWrite: new FormControl(true, [Validators.required]),
        accountDelete: new FormControl(false, [Validators.required])
    })


    constructor(
        private accountInviteService: AccountInviteService,
        private authorizeService: AuthorizeService,
        private accountService: AccountService, 
        private notificationService: ToastrService) {}

    ngOnInit() {
        this.authorizeService.getUser()
        .pipe(map(u => u && u.name))
        .subscribe(name => {
            this.userName = name;
            this.getAccounts();
            this.getAccountInvites();
        });
    }

    getAccounts() {
        this.accountService.getAll().subscribe(r => {
            this.accounts = [...r.items];
            if(this.accounts.length > 0) {
                this.inviteForm.patchValue({
                    account: this.accounts[0].id
                });
            }
            
        });
    }

    getAccountInvites() {
        this.accountInviteService.getAll()
        .subscribe(r => {
            const invites = r.items.reduce((acc, el) => {
                if(el.invited.name === this.userName) {
                    acc.received = [...acc.received, el];
                } else {
                    acc.sent = [...acc.sent, el];
                }
                return acc;
            }, 
            {
                sent: [],
                received: []
            })

            this.accountInvitesSent = [...invites.sent];
            this.accountInvitesReceived = [...invites.received];
        });
    }

    acceptAccountInvite(inviteId: number) {
        this.accountInviteService.accept(inviteId).subscribe(() => {
            this.getAccountInvites();
        })
    }

    deleteAccountInvite(inviteId: number) {
        this.accountInviteService.delete(inviteId).subscribe(() => {
            this.getAccountInvites();
        })
    }

    onSubmit = () => {
        if(this.inviteForm.valid) {
            const {
                email,
                account,
                transactionRead,
                transactionWrite,
                accountDelete,
                accountWrite,
            } = this.inviteForm.value;

            const inviteRequest = new AddAccountInviteRequest(
                Number(account),
                email,
                transactionRead,
                transactionWrite,
                accountWrite,
                accountDelete
            );

            this.accountInviteService
                .add(inviteRequest)
                .subscribe(this.afterSubmitAction);
        }
    }

        private afterSubmitAction = (r: ApiResponse) => {
            this.inviteForm.reset();
            this.formRef.resetForm();
            if(this.accounts.length > 0) {
                this.inviteForm.patchValue({
                    account: this.accounts[0].id
                });
            }
            this.notificationService.info(r.message);
            this.getAccountInvites();
        }
}