import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ProjectService } from '../services/project.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html'
})
export class AddProjectComponent {
  name: string = '';

  constructor(
    private projectService: ProjectService,
    private router: Router,
    private cookieService:CookieService
  ) {}

  addProject(): void {
    if (!this.name.trim()) {
      alert('Proje adı boş olamaz!');
      return;
    }

    const newProject = { 
        name: this.name,
        description: '',
        hours: 0,  
        workerId: '00000000-0000-0000-0000-000000000000',
        date: new Date()
     };

    console.log('Yeni proje gönderiliyor:', newProject);
    console.log("Token gönderiliyor:", this.cookieService.get('Authorization')); 
    this.projectService. addSimpleProject(newProject).subscribe({
      next: () => {
        console.log('✅ Proje eklendi');
        this.router.navigate(['/workers']); // Yönlendirme opsiyonel
      },
      error: (err) => {
        console.error('❌ Proje ekleme hatası: ', err);
      }
    });
  }
}