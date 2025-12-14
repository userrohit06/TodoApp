import { CommonModule, DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TodoModel } from '../../../core/models/todo.model';
import { Todo } from '../../../core/services/todo';
import { ApiRespone } from '../../../core/models/api-response.model';

@Component({
  selector: 'app-todo-list',
  imports: [CommonModule, DatePipe, RouterLink],
  templateUrl: './todo-list.html',
  styleUrl: './todo-list.css',
})
export class TodoList {
  todos: TodoModel[] = [];
  isLoading = false;
  error: string | null = null;

  constructor(private todoService: Todo) {}

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos() {
    this.isLoading = true;
    this.error = null;

    this.todoService.getTodos().subscribe({
      next: (res: ApiRespone<TodoModel[]>) => {
        this.todos = res.data ?? [];
        this.isLoading = false;
      },
      error: (err) => {
        this.error = err.error?.message ?? 'Failed to load todos';
        this.isLoading = false;
      },
    });
  }
}
