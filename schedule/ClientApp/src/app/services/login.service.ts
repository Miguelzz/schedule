import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) {
  }

  login(email: string, password: string) {
    return this.http.post('/api/login', {
      email: email,
      password: password,
    });
  }
}
