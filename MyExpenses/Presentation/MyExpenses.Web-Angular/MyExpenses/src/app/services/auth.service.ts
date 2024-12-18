import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { }
  // googleLogin(token: string): Observable<any> {
  //   return this.http.post<any>(`${this.baseUrl}/google-login`, { token });
  // }
}
