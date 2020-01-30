import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiListResponse } from "src/types/ApiListResponse";
import endpoints from "src/config/endpoints";
import { Observable } from "rxjs/internal/Observable";
import ApiResponse from "src/types/ApiResponse";
import Category from "src/types/Category";
import AddCategoryRequest from "src/types/AddCategoryRequest";


@Injectable({
    providedIn: 'root'
})
export class CategoryService {
    constructor(private http: HttpClient, 
        @Inject('BASE_URL') private baseUrl: string){

    }

    getAll(): Observable<ApiListResponse<Category>> {
       return this.http.get<ApiListResponse<Category>>(this.baseUrl + endpoints.category);
    }

    add(category: AddCategoryRequest): Observable<ApiResponse> {
        return this.http.post<ApiResponse>(this.baseUrl + endpoints.category, category);
    }

    delete(categoryId: number): Observable<ApiResponse> {
        return this.http.delete<ApiResponse>(this.baseUrl + endpoints.category + '/' + categoryId);
    }
}
