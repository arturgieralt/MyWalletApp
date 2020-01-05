import { Component, OnInit, OnDestroy } from "@angular/core";
import { CategoryService } from "src/services/category.services";
import Category from "src/types/Category";
import { Subscription } from "rxjs";

@Component({
    selector: 'category-list',
    templateUrl: './category-list.component.html'
})
export class CategoryListComponent implements OnInit, OnDestroy {
    private categories: Category[] = [];
    private getCategoriesSubscription: Subscription;

    constructor(private categoryService: CategoryService) {}

    ngOnInit() {
        var observer = this.categoryService.getAll();
        this.getCategoriesSubscription = observer.subscribe(r => {
            this.categories = [...r.items];
        })
    }

    ngOnDestroy() {
        this.getCategoriesSubscription.unsubscribe();
    }
}