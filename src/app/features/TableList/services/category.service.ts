import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Category } from '../models/category.model';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http:HttpClient) { }

addCategory(model: AddCategoryRequest):Observable<void>{
  const headers = new HttpHeaders({
    'ngrok-skip-browser-warning': 'true'
  });


return this.http.post<void>('https://f5efc48e435c.ngrok-free.app/api/Worker/CreateWorkers',model,{headers});

}

getAllCategories():Observable<Category[]>{

  const headers = new HttpHeaders({
    'ngrok-skip-browser-warning': 'true'
  });

return this.http.get<Category[]>('https://f5efc48e435c.ngrok-free.app/api/Worker/GetAllWorkers',{headers})

}

deleteCategory(id: string): Observable<any> {

  const headers = new HttpHeaders({
    'ngrok-skip-browser-warning': 'true'
  });
  return this.http.delete(`https://f5efc48e435c.ngrok-free.app/api/Worker/${id}`,{headers});
}

}
