import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CategoryListComponent } from './features/TableList/category-list/category-list.component';
import { AddCategoryComponent } from './features/TableList/add-category/add-category.component';
import { WorkerListComponent } from './core/components/workerlist/worker-list.component';
import {  WorkerDetailComponent  } from './core/components/workerdetail/workerdetail/workerdetail.component';
import { AddProjectComponent } from './core/components/AddProjectComponent/add-project.component';
import { ProjectListComponent } from './core/components/ProjectList/project-list.component';
import { MonthlyReportComponent } from './core/components/MonthlyReport/monthly-report.component';
import { HomeComponent } from './core/components/HomeComponent/home/home.component';
import { LoginComponent } from './core/components/authloginComponent/login/login.component';

const routes: Routes = [
  {
    path: 'Pages/TableList',
    component: CategoryListComponent

  },

  {
  path: 'Pages/TableList/add',
  component: AddCategoryComponent

  },
  { path: 'workers', component: WorkerListComponent },
  { path: 'workers/:id', component:  WorkerDetailComponent },
  { path: 'Pages/AddProject', component: AddProjectComponent },
  { path: 'Pages/ProjectList', component: ProjectListComponent },
  { path: 'Pages/MonthlyReport', component: MonthlyReportComponent },
  { path: '', component: HomeComponent },

  {path: 'login' ,component:LoginComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
