import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiRespone } from '../models/api-response.model';
import { TodoModel } from '../models/todo.model';

interface CreateTodoRequest {
  title: string;
  description: string;
}

@Injectable({
  providedIn: 'root',
})
export class Todo {
  private baseUrl = `${environment.apiUrl}/api/Todo`;

  constructor(private http: HttpClient) {}

  getTodos(): Observable<ApiRespone<TodoModel[]>> {
    return this.http.get<ApiRespone<TodoModel[]>>(`${this.baseUrl}/all`);
  }

  createTodo(body: CreateTodoRequest): Observable<ApiRespone<any>> {
    return this.http.post<ApiRespone<any>>(`${this.baseUrl}/create`, body);
  }
}
