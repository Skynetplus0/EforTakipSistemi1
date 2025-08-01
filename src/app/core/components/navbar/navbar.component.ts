import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../authloginComponent/services/auth.service';
import { User } from '../authloginComponent/models/user.model';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  searchTerm: string = '';
  user?: User;

  constructor(private router: Router,private authService:AuthService) {}

  ngOnInit(): void {
      this.authService.user()
      .subscribe({
        next:(response)=>{
          //console.log(response);
          this.user=response;
        }
      });
      this.user=this.authService.getUser();

  }

  onLogout():void{
    this.authService.logout();
    this.router.navigateByUrl('/');



  }
  onSearchSubmit(event: Event): void {
    event.preventDefault(); // formun refresh yapmasını engelle
    const trimmed = this.searchTerm.trim();
    if (trimmed) {
      this.router.navigate(['/workers'], { queryParams: { search: trimmed } });
    }
  }


}
