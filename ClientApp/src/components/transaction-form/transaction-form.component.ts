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

    public accounts: AccountSummary[];
    public categories: Category[];
    public accountId: string;
    public map: L.Map;
    public coords: number[] = null;
    public tag: string= '';
    public tags: string[] = [];

    public transactionForm = new FormGroup({
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

    addTag() {
        const tag = this.tag.toUpperCase().replace(/[^a-z0-9]/gi,'');;
        if(!this.tags.includes(tag) && tag.length > 1) {
            this.tags.push(tag);
            this.tag = '';
        }
    }

    removeTag(tag: string) {
        this.tags = this.tags.filter(t => t !== tag);
    }

    getAccountId() {
        this.routeService.paramMap
            .pipe(
                switchMap((params: ParamMap) => { 
                    return params.getAll('id')
                })
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
            this.setMap(new L.LatLng(position.coords.latitude, position.coords.longitude));
        }, error => {
            this.setMap(new L.LatLng(51.5, -0.09));
        })
    
    }

    onMapClick = (marker: L.Marker) => (e: L.LeafletMouseEvent) => {
        marker.setLatLng(e.latlng);
        this.coords = [e.latlng.lat, e.latlng.lng];
        console.log(this.coords)
    }

    private setMap(coords: L.LatLng) {
        if(!this.map) {
            this.coords = [coords.lat, coords.lng];
            const accessToken = "pk.eyJ1IjoiZ2llcmkwNyIsImEiOiJjazY2eWJnM3EwMzJmM2VtemZzc2s1dzcyIn0.VzXZOLhgdp8eqmlHPFsIew";
            this.map = L.map('mapid').setView(coords, 13);
            L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
            attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
            maxZoom: 18,
            id: 'mapbox/streets-v11',
            accessToken
        }).addTo(this.map);

        const customIcon = L.icon({
            iconUrl: 'assets/icon.png',
        });

        const marker = L.marker(coords, {icon: customIcon}).addTo(this.map);
        this.map.on('click', this.onMapClick(marker));
        }
    }

    onSubmit = () => {
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
                this.tags,
                !isNaN(category) && Number(category)
            );

            if(this.coords) {
                transactionRequest.addCoordinates(this.coords[0], this.coords[1])
            }

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