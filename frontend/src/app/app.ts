import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Auth } from "./pages/auth/auth";
import { Content } from './pages/content/content';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [
    Auth,
    Content,
    
  ]
})
export class App implements OnInit {

  isAuth = false;

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef){}

  ngOnInit(): void {    
    const session = localStorage.getItem("session");
    
    if(!session) return;
    
    this.http.get("https://localhost:7135/Auth/api/me",
      {
        headers:{
          Session: session
        }
      }
    ).subscribe({
      next: () => {
        this.isAuth = true;
        this.cdr.detectChanges();
        console.log(this.isAuth);        
        
      },
      error: () => {
        localStorage.removeItem("session");
        this.isAuth=false;
      }
    });  
  }

}
