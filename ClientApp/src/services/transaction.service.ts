import { Injectable, Inject } from "@angular/core";
import { ApiListResponse } from "src/types/ApiListResponse";
import { HttpClient } from "@angular/common/http";
import endpoints from "src/config/endpoints";
import { Observable } from "rxjs/internal/Observable";
import Transaction from "src/types/Transaction";
import AddTransactionRequest from "src/types/AddTransactionRequest";
import ApiResponse from "src/types/ApiResponse";


@Injectable({
    providedIn: 'root'
})
export class TransactionService {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string){}

    getAll(accountId?: string): Observable<ApiListResponse<Transaction>> {
        let url = this.baseUrl + endpoints.transaction;
        if(accountId) {
            url = url +  `?AccountId=${accountId}`
        }
       return this.http.get<ApiListResponse<Transaction>>(url);
    }

    add(transaction: AddTransactionRequest): Observable<ApiResponse> {
        return this.http.post<ApiResponse>(this.baseUrl + endpoints.transaction, transaction);
    }

    delete(transactionId: number): Observable<ApiResponse> {
        return this.http.delete<ApiResponse>(this.baseUrl + endpoints.transaction + '/' + transactionId);
    }
}