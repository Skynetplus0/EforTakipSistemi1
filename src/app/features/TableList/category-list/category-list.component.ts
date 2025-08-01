import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit{

categories?:Category[];

categories2$?:Observable<Category[]>;

constructor(private categoryService:CategoryService)
{


}



  ngOnInit(): void {
  this.categoryService.getAllCategories()
  .subscribe({

next:(response)=>{
this.categories=response;

}

  });

  }

  deleteWorker(id: string): void {
    this.categoryService.deleteCategory(id).subscribe({
      next: () => {
        this.categories = this.categories?.filter(w => w.id !== id);
      },
      error: (err) => {
        console.error('Silme hatası:', err);
      }
    });
  }



}
