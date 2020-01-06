import { Injectable, Inject } from "@angular/core";
import { ApiListResponse } from "src/types/ApiListResponse";
import { HttpClient } from "@angular/common/http";
import endpoints from "src/config/endpoints";
import { Observable } from "rxjs/internal/Observable";
import Transaction from "src/types/Transaction";


@Injectable({
    providedIn: 'root'
})
export class TransactionService {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string){}

    getAll(accountId?: string): Observable<ApiListResponse<Transaction>> {
        let url = this.baseUrl + endpoints.transaction;
        console.log(accountId)
        if(accountId) {
            url = url +  `?AccountId=${accountId}`
        }
       return this.http.get<ApiListResponse<Transaction>>(url);
    }
}