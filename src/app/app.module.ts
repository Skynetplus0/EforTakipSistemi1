import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { CategoryListComponent } from './features/TableList/category-list/category-list.component';
import { AddCategoryComponent } from './features/TableList/add-category/add-category.component';
import { FormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {  WorkerDetailComponent } from './core/components/workerdetail/workerdetail/workerdetail.component';
import { CommonModule } from '@angular/common';
import { WorkerListComponent } from './core/components/workerlist/worker-list.component';
import { AddProjectComponent } from './core/components/AddProjectComponent/add-project.component';
import { ProjectListComponent } from './core/components/ProjectList/project-list.component';
import { MonthlyReportComponent } from './core/components/MonthlyReport/monthly-report.component';
import { NgChartsModule } from 'ng2-charts';
import { HomeComponent } from './core/components/HomeComponent/home/home.component';
import { LoginComponent } from './core/components/authloginComponent/login/login.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CategoryListComponent,
    AddCategoryComponent,
    WorkerDetailComponent,
    WorkerListComponent,
    AddProjectComponent,
    ProjectListComponent,
    MonthlyReportComponent,
    HomeComponent,
    LoginComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    CommonModule,
    NgChartsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
