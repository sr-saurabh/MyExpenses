import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AuthResponse } from '../models/auth/authResponse';
import { Login } from '../models/auth/login';
import { Register } from '../models/auth/register';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl= environment.baseUrl;
  constructor(private http:HttpClient) {}

  login(loginData: Login){
    return this.http.post<AuthResponse>(
      `${this.baseUrl}/auth/login`,loginData,
    );
  }

  signup(registerData:Register){
    var s= this.http.post<AuthResponse>(`${this.baseUrl}/Auth/register`,registerData)
    return s;
  }


  isAuthorized():boolean{
    return !!localStorage.getItem('token');
  }
}
