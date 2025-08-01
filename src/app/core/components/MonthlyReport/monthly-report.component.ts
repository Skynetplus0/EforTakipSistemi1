import { Component } from '@angular/core';
//import { WorkerService } from '../../services/worker.service';
import { WorkerService } from '../services/worker.service';
import { ChartConfiguration } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import { ChartOptions } from 'chart.js';

@Component({
  selector: 'app-monthly-report',
  templateUrl: './monthly-report.component.html',
  styleUrls: ['./monthly-report.component.css']
})
export class MonthlyReportComponent {
  selectedMonth: string = '';
  reports: any[] = [];
  
  pieLabels: string[] = [];
  pieData: number[] = [];
  pieChartType: ChartConfiguration<'pie'>['type'] = 'pie';

  public pieChartPlugins = [ChartDataLabels];

  public pieChartOptions: ChartOptions<'pie'> = {
    responsive: true,
    plugins: {
      datalabels: {
        formatter: (value, context) => {
          const total = context.chart.data.datasets[0].data.reduce((a: any, b: any) => a + b, 0);
          const percentage = (value / total) * 100;
          return percentage.toFixed(1) + '%';
        },
        color: '#fff',
        font: {
          weight: 'bold',
          size: 14,
        }
      },
      legend: {
        position: 'bottom'
      }
    }
  };


  constructor(private workerService: WorkerService) {}

  onMonthChange(): void {
    if (!this.selectedMonth) return;
    const [year, month] = this.selectedMonth.split('-').map(Number);

    this.workerService.getMonthlySummary(year, month).subscribe({
      next: (data) => {
        this.reports = data;
        this.setPieChartData(data);
        
      },
      error: (err) => console.error('Rapor alınamadı:', err),
    });
  }
  setPieChartData(data: any[]): void {
    const summary: { [key: string]: number } = {};
    data.forEach(worker => {
      worker.projects.forEach((p: any) => {
        summary[p.name] = (summary[p.name] || 0) + p.hours;
      });
    });

    this.pieLabels = Object.keys(summary);
    this.pieData = Object.values(summary);
  }
 
}