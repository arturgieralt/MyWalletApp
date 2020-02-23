import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiListResponse } from "src/types/ApiListResponse";
import endpoints from "src/config/endpoints";
import { Observable } from "rxjs/internal/Observable";
import ApiResponse from "src/types/ApiResponse";
import { AccountInvite } from "src/types/AccountInvite";
import { AddAccountInviteRequest } from "src/types/AddAccountInviteRequest";


@Injectable({
    providedIn: 'root'
})
export class AccountInviteService {
    constructor(private http: HttpClient, 
        @Inject('BASE_URL') private baseUrl: string){

    }

    getAll(): Observable<ApiListResponse<AccountInvite>> {
       return this.http.get<ApiListResponse<AccountInvite>>(this.baseUrl + endpoints.accountInvite);
    }

    add(invite: AddAccountInviteRequest): Observable<ApiResponse> {
        return this.http.post<ApiResponse>(this.baseUrl + endpoints.accountInvite, invite);
    }

    delete(inviteId: number): Observable<ApiResponse> {
        return this.http.delete<ApiResponse>(this.baseUrl + endpoints.accountInvite + '/' + inviteId);
    }

    accept(inviteId: number): Observable<ApiResponse> {
        return this.http.put<ApiResponse>(this.baseUrl + endpoints.accountInvite + '/' + inviteId + '/accept', {});
    }
}