import { Injectable } from '@angular/core';
import { ApiRespone } from '../models/api-response.model';
import { environment } from '../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { TokenStorage } from './token-storage';
import { Observable, tap } from 'rxjs';

interface RegisterRequest {
  name: string;
  email: string;
  password: string;
}

interface LoginRequest {
  email: string;
  password: string;
}

type AuthResponse = ApiRespone<string>;

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private baseUrl = `${environment.apiUrl}/api/Auth`;

  constructor(private http: HttpClient, private tokenStorage: TokenStorage) {}

  register(body: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.baseUrl}/register`, body);
  }

  login(body: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.baseUrl}/login`, body).pipe(
      tap((res) => {
        if (res.data) {
          this.tokenStorage.setToken(res.data);
        }
      })
    );
  }

  logout(): Observable<ApiRespone<null>> {
    return this.http.post<ApiRespone<null>>(`${this.baseUrl}/logout`, {}).pipe(
      tap(() => {
        this.tokenStorage.clearToken();
      })
    );
  }
}
