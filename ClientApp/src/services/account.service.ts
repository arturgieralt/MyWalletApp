import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AccountSummary } from "src/types/AccountSummary";
import { ApiListResponse } from "src/types/ApiListResponse";
import endpoints from "src/config/endpoints";
import { Observable } from "rxjs/internal/Observable";
import { AddAccountRequest } from "src/types/AddAccountRequest";
import ApiResponse from "src/types/ApiResponse";


@Injectable({
    providedIn: 'root'
})
export class AccountService {
    constructor(private http: HttpClient, 
        @Inject('BASE_URL') private baseUrl: string){

    }

    getAll(): Observable<ApiListResponse<AccountSummary>> {
       return this.http.get<ApiListResponse<AccountSummary>>(this.baseUrl + endpoints.account);
    }

    add(account: AddAccountRequest): Observable<ApiResponse> {
        return this.http.post<ApiResponse>(this.baseUrl + endpoints.account, account);
    }
}