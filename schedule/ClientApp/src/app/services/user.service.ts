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
    if (search) {
      return this.http.get('/api/user/' + search);
    }
  }

  addRelation(relation) {
    return this.http.post(`/api/relation/`, relation);
  }

  editRelation(relation) {
    return this.http.put(`/api/relation/`, relation);
  }
}
