import { Injectable, Inject } from "@angular/core";
import { Currency } from "src/types/Currency";
import { ApiListResponse } from "src/types/ApiListResponse";
import { HttpClient } from "@angular/common/http";
import endpoints from "src/app/config/endpoints";
import { Observable } from "rxjs/internal/Observable";


@Injectable({
    providedIn: 'root'
})
export class CurrencyService {
    constructor(private http: HttpClient, 
        @Inject('BASE_URL') private baseUrl: string){

    }

    getAll(): Observable<ApiListResponse<Currency>> {
       return this.http.get<ApiListResponse<Currency>>(this.baseUrl + endpoints.currency);
    }
}