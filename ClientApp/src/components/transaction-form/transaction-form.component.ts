import { Component, ViewChild, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ToastrService } from 'ngx-toastr';
import ApiResponse from "src/types/ApiResponse";
import { AccountService } from "src/services/account.service";
import { AccountSummary } from "src/types/AccountSummary";
import Category from "src/types/Category";
import { CategoryService } from "src/services/category.services";
import { TransactionService } from "src/services/transaction.service";
import AddTransactionRequest from "src/types/AddTransactionRequest";
import { ActivatedRoute, ParamMap } from "@angular/router";
import { switchMap } from "rxjs/operators";
import { GeoLocationService } from "src/services/geolocation.service";
import * as L from 'leaflet';

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
    private map: L.Map;

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
        private notificationService: ToastrService,
        private routeService: ActivatedRoute,
        private transactionService: TransactionService,
        private geolocationService: GeoLocationService
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

    getLocation() {
        this.geolocationService.getCurrentPosition().subscribe(position => {
            if(!this.map) {
                const accessToken = "pk.eyJ1IjoiZ2llcmkwNyIsImEiOiJjazY2eWJnM3EwMzJmM2VtemZzc2s1dzcyIn0.VzXZOLhgdp8eqmlHPFsIew";
                const coords: L.LatLngExpression = [position.coords.latitude, position.coords.longitude];
                this.map = L.map('mapid').setView(coords, 13);
                L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
                attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
                maxZoom: 18,
                id: 'mapbox/streets-v11',
                accessToken
            }).addTo(this.map);

            const marker = L.marker(coords).addTo(this.map);

            function onMapClick(e: L.LeafletMouseEvent) {
                marker.setLatLng(e.latlng);
            }
            
            this.map.on('click', onMapClick);
        }
        })
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

            console.log(date);
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
        this.notificationService.info(r.message);
    }

}