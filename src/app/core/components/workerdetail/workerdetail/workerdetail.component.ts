
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WorkerService } from '../../services/worker.service';
@Component({
  selector: 'app-workerdetail',
  templateUrl: './workerdetail.component.html',
  styleUrls: ['./workerdetail.component.css']
})
export class WorkerDetailComponent implements OnInit {
  workerId!: string;
  selectedMonth: string = '';
  report: any[] = [];
  allProjects: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private workerService: WorkerService
  ) {}

  ngOnInit(): void {
    this.workerId = this.route.snapshot.paramMap.get('id')!;
    this.fetchAllProjects();
  }

  fetchAllProjects(): void {
    this.workerService.getAllProjects().subscribe({
      next: (res) => {
        // sadece workerId'si null olan projeleri göster
        this.allProjects = res.filter(p => !p.workerId);
      },
      error: (err) => console.error('Proje listesi alınamadı:', err),
    });
  }

  onMonthChange(): void {
    if (!this.selectedMonth) return;

    const [year, month] = this.selectedMonth.split('-').map(Number);

    this.workerService
      .getMonthlyReport(this.workerId, year, month)
      .subscribe({
        next: (data) => {
          this.report = data;
        },
        error: (err) => {
          console.error('Hata:', err);
        },
      });
  }

  save(item: any): void {
    if (!item.projectName || item.projectName.trim() === '') {
      alert('Lütfen proje ismi seçin.');
      return;
    }


    if (!item.id) {
      // ID yoksa yeni proje ekle
      const newProject = {
        workerId: this.workerId,
        date: item.date,
        name: item.projectName,
        description: item.description,
        hours: item.hours,
      };
      console.log('Yeni proje gönderiliyor:', newProject);
      this.workerService.createProject(newProject).subscribe({
        next: () => {
          console.log('✅ Yeni proje eklendi');
          this.onMonthChange(); // Güncelle
        },
        error: (err) => console.error('❌ Ekleme hatası:', err),
      });
    } else {
      // Güncelleme
      const updated = { //buraya bak id kısmı
        workerId:this.workerId,
        id: item.id,
        date: item.date,
        name: item.projectName,
        description: item.description,
        hours: item.hours,
      };
  
      this.workerService.updateProject(item.id, updated).subscribe({
        next: () => console.log('✅ Güncellendi'),
        error: (err) => console.error('❌ Güncelleme hatası:', err),
      });
    }
  }

  isWeekend(dateString: string): boolean {
    const date = new Date(dateString);
    const day = date.getDay(); // 0: Pazar, 6: Cumartesi
    return day === 0 || day === 6;
  }

  restrictHoursInput(event: KeyboardEvent): void {
    const allowedKeys = ['Backspace', 'ArrowLeft', 'ArrowRight', 'Tab'];
    if (allowedKeys.includes(event.key)) return;
  
    const key = Number(event.key);
    if (isNaN(key) || key < 0 || key > 9) {
      event.preventDefault();
    }
  }

}