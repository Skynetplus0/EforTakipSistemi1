import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Worker } from '../models/worker.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WorkerService {

  private apiUrl = 'https://f5efc48e435c.ngrok-free.app/api/Worker';

  constructor(private http: HttpClient) {}

  deleteWorker(id: string): Observable<any> {
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'true'
    });
    return this.http.put(`https://f5efc48e435c.ngrok-free.app/api/Worker/SoftDelete/${id}`, {},{headers});
  }

  getAllWorkers(): Observable<Worker[]> {
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'true'
    });
    return this.http.get<Worker[]>(`https://f5efc48e435c.ngrok-free.app/api/Worker/GetAllWorkers`,{headers});
  }

  getWorkerById(id: string): Observable<Worker> {
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'true'
    });
    return this.http.get<Worker>(`${this.apiUrl}/${id}`,{headers});
  }

  getMonthlyReport(workerId: string, year: number, month: number): Observable<any[]> {
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'true'
    });
    
    return this.http.get<any[]>(`${this.apiUrl}/monthly?workerId=${workerId}&year=${year}&month=${month}`,{headers});
  }



  updateProject(id: string, body: any): Observable<any> {
   
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'true'
    });
   
    return this.http.put(`${this.apiUrl.replace('Worker', 'Projects')}/${id}`, body,{headers});
  }
  
  getAllProjects(): Observable<any[]> {
    
    const headers = new HttpHeaders({
      'ngrok-skip-browser-warning': 'true'
    });
    
    return this.http.get<any[]>(`https://f5efc48e435c.ngrok-free.app/api/Projects/GetAllProjects`,{headers});
  }
  
createProject(body: any): Observable<any> {
  const headers = new HttpHeaders({
    'ngrok-skip-browser-warning': 'true'
  });
  
  return this.http.post(`https://f5efc48e435c.ngrok-free.app/api/Projects/CreateProjects`, body,{headers});
}

getMonthlySummary(year: number, month: number): Observable<any[]> {
 
  const headers = new HttpHeaders({
    'ngrok-skip-browser-warning': 'true'
  });
 
  return this.http.get<any[]>(`https://f5efc48e435c.ngrok-free.app/api/Worker/summary?year=${year}&month=${month}`,{headers});
}

}