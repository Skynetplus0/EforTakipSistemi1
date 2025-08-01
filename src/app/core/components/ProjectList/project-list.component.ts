import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../services/project.service';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {
  projects: any[] = [];

  constructor(private projectService: ProjectService) {}

  ngOnInit(): void {
    this.loadProjects();
  }

  loadProjects(): void {
    this.projectService.getUnassignedProjects().subscribe({
      next: (res) => this.projects = res,
      error: (err) => console.error('Listeleme hatası:', err)
    });
  }

  deleteProject(id: string): void {
    this.projectService.softDeleteProject(id).subscribe({
      next: () => {
        this.projects = this.projects.filter(p => p.id !== id);
      },
      error: (err) => console.error('Silme hatası:', err)
    });
  }
}