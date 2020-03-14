import { Injectable } from '@angular/core';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUser() {
    return this.http.get('/api/user');
  }

  getUsers(search) {
    return this.http.get('/api/user/' + search);
  }
}
