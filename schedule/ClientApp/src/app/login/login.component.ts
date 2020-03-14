import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginService: LoginService, private router: Router) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/');
    }
  }

  logIn(email: string, password: string, event: Event) {
    event.preventDefault();
    this.loginService.login(email, password).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
      },
      error => {
        if (error.status === 401) {
          console.error('401');
        }
        console.error(error);
      },
      () => this.navigate()
    );

  }

  navigate() {
    this.router.navigateByUrl('/');
  }
}
