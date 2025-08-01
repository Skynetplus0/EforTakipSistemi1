import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { WorkerService } from '../services/worker.service';
import { Worker } from '../models/worker.model';

@Component({
  selector: 'app-worker-list',
  templateUrl: './worker-list.component.html'
})
export class WorkerListComponent implements OnInit {
  workers: Worker[] = [];
  filteredWorkers: Worker[] = [];

  constructor(
    private workerService: WorkerService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      const term = (params['search'] || '').toLowerCase();
      console.log('🔍 URL’den gelen arama terimi:', term);
  
      this.workerService.getAllWorkers().subscribe((res) => {
        this.workers = res;
        
        this.filteredWorkers = term
          ? this.workers.filter(
              (w) =>
                w.name.toLowerCase().includes(term) ||
                w.surname.toLowerCase().includes(term)
            )
          : [...this.workers];
  
        console.log('🎯 Filtrelenmiş çalışanlar:', this.filteredWorkers);
      });
    });
  }
  goToDetails(id: string): void {
    this.router.navigate(['/workers', id]);
  }

  deleteWorker(id: string): void {
    this.workerService.deleteWorker(id).subscribe({
      next: () => {
        this.workers = this.workers.filter(w => w.id !== id);
      },
      error: (err) => {
        console.error('Silme hatası:', err);
      }
    });
  }


}