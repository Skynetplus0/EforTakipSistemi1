import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../authloginComponent/services/auth.service';

@Injectable({ providedIn: 'root' })
export class ProjectService {
  private apiUrl = 'https://f5efc48e435c.ngrok-free.app/api/Projects';

  constructor(private http: HttpClient,private cookieService: CookieService
    ,private authService:AuthService
  ) {}

  createProject(body: any): Observable<any> {
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'true'
    });

    return this.http.post(`https://f5efc48e435c.ngrok-free.app/api/Projects/CreateProjects1`, body,{headers});
  }

  getAllProjects(): Observable<any[]> {
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'true'
    });

    return this.http.get<any[]>('https://f5efc48e435c.ngrok-free.app/api/Projects/GetAllProjects',{headers});
  }

  addProject(project: { name: string }): Observable<any> {
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'true'
    });
    
    return this.http.post(`https://f5efc48e435c.ngrok-free.app/api/Projects/CreateProjects`, project,
      {headers:{
        'Authorization': this.cookieService.get('Authorization')

      }


      }
    );
  }

  addSimpleProject(project: { name: string }): Observable<any> {
    const headers6 = new HttpHeaders({
      'ngrok-skip-browser-warning': 'true'
    });

    const token = this.cookieService.get('Authorization');
    const token1='eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJpc21ldDFAbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJSZWFkZXIiLCJleHAiOjE3NTM0NTI0NjAsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyNjUiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0MjAwIn0.YwkLGFxE9u83rMnG_BvtPlVbuGDKU9VBTa6gtqWbiI0'

    const token4 = this.authService.getToken();//en son
    const headers4 = new HttpHeaders({
      'Authorization': `Bearer ${token4}`,
      'Content-Type': 'application/json',
      'ngrok-skip-browser-warning': 'true'
    });


    const headers = new HttpHeaders().set('Authorization', token1);
    const headers1 = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token1}` // 🔑 Token burada kullanılıyor
    });
 
    console.log(token);
    console.log(token4);
    return this.http.post(`${this.apiUrl}/AddSimpleProject`, project,
    {headers:headers4//:
     // {
     // 'Authorization':this.cookieService.get('Authorization')//`Bearer ${token}`//this.cookieService.get('Authorization')

   //  }


    }
    );
  }


  deleteProject(id: string): Observable<any> {
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'true'
    });
    return this.http.put(`https://f5efc48e435c.ngrok-free.app/api/Projects/SoftDelete/${id}`, {},{headers});
  }

  getUnassignedProjects(): Observable<any[]> {
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'true'
    });
    return this.http.get<any[]>(`https://f5efc48e435c.ngrok-free.app/api/Projects/Unassigned`,{headers});
  }
  
  softDeleteProject(id: string): Observable<any> {
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'true'
    });
    return this.http.put(`https://f5efc48e435c.ngrok-free.app/api/Projects/SoftDelete/${id}`, {},{headers});
  }


}