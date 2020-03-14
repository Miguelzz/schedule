import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private http: HttpClient) {
  }

  register(name: string, lastName: string, email: string, phone: string, password: string, documentType: string, documentNumber: string) {
    return this.http.post('/api/register', {
      name: name,
      lastName: lastName,
      email: email,
      phone: phone,
      password: password,
      documentType: documentType,
      documentNumber: documentNumber,
    });
  }
}
