import { Component, ViewChild } from "@angular/core";
import { CategoryService } from "src/services/category.services";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import AddCategoryRequest from "src/types/AddCategoryRequest";
import { MatSnackBar } from "@angular/material/snack-bar";
import ApiResponse from "src/types/ApiResponse";

@Component({
    selector: 'category-form',
    templateUrl: './category-form.component.html',
    styleUrls: ['./category-form.component.css']
})
export class CategoryFormComponent {

    @ViewChild('formRef', { static: false }) formRef;

    private categoryForm = new FormGroup({
        name: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)])
    })

    constructor(private categoryService: CategoryService, private notificationService: MatSnackBar){}

    onSubmit() {
        if(this.categoryForm.valid) {
            const request = new AddCategoryRequest(this.categoryForm.value.name)
            this.categoryService
                .add(request)
                .subscribe(this.afterSubmitAction);
        }
    }

    private afterSubmitAction = (r: ApiResponse) => {
        this.categoryForm.reset();
        this.formRef.resetForm();
        this.notificationService.open(r.message);
    }

}