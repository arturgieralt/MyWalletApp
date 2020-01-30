import { Component, OnInit } from "@angular/core";
import { CategoryService } from "src/services/category.services";
import Category from "src/types/Category";

@Component({
    selector: 'category-list',
    templateUrl: './category-list.component.html'
})
export class CategoryListComponent implements OnInit {
    private categories: Category[] = [];

    constructor(private categoryService: CategoryService) {}

    ngOnInit() {
        this.getCategories();
    }

    getCategories() {
        this.categoryService.getAll().subscribe(r => {
            this.categories = [...r.items];
        })
    }

    deleteCategory(categoryId: number) {
        this.categoryService.delete(categoryId).subscribe(() => {
            this.getCategories();
        })
    }
}