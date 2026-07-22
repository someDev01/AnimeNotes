import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-auth',
  imports: [FormsModule],
  templateUrl: './auth.html',
  styleUrl: './auth.css',
})
export class Auth {

  constructor(private http: HttpClient){}
  
  isLogin: boolean = true;

  login: string = '';
  password: string = '';
  repeatPassword: string = '';

  toastMessage: string = '';
  showToast: boolean = false;
  toastType: 'success' | 'error' = 'success';

  changeMode(value: boolean){
    this.isLogin = value;
  }

  submit() {

  if (this.login.trim() === '') {
    this.showNotification("Введите логин", "error");
    return;
  }

  if (this.password.trim() === '') {
    this.showNotification("Введите пароль", "error");
    return;
  }

  if (!this.isLogin && this.repeatPassword !== this.password) {
    this.showNotification("Пароли не совпадают", "error");
    return;
  }

  if (this.isLogin) {

    this.http.post<any>(
      "http://45.155.102.22:7135/Auth/api/login",
      {
        login: this.login,
        password: this.password
      }
    ).subscribe({

      next: (result) => {

        localStorage.setItem("session", result.session);

        this.showNotification("Успешный вход", "success");

        setTimeout(() => {
          window.location.reload();
        }, 500);

      },

      error: (err) => {

        this.showNotification(err.error, "error");

      }

    });

  }
  else {

    this.http.post(
      "http://45.155.102.22:7135/Auth/api/register",
      {
        login: this.login,
        password: this.password
      }
    ).subscribe({

      next: () => {

        this.showNotification("Аккаунт создан", "success");

      },

      error: (err) => {

        this.showNotification(err.error, "error");

      }

    });

  }

}

  showNotification(message: string, type: 'success' | 'error') {

    console.log('уведомленеи');
    
    this.toastMessage = message;
    this.toastType = type;
    this.showToast = true;

    setTimeout(() => {
      console.log('скрыли уведомление');
      
      this.showToast = false;
    }, 2000);

  }
}
