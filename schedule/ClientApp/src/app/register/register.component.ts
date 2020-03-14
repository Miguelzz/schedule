import { Component, OnInit } from '@angular/core';
import { RegisterService } from '../services/register.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private registerService: RegisterService, private router: Router) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/');
    }
  }

  register(name: string, lastName: string, email: string, phone: string, password: string,
    documentType: string, documentNumber: string, event: Event) {
    event.preventDefault();
    this.registerService.register(name, lastName, email, phone, password, documentType, documentNumber).subscribe(
      (res: any) => {
        localStorage.setItem('token', '');
        this.router.navigateByUrl('/');
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
