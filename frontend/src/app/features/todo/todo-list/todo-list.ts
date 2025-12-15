import { CommonModule, DatePipe, NgIf } from '@angular/common';
import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TodoModel } from '../../../core/models/todo.model';
import { Todo } from '../../../core/services/todo';
import { ApiRespone } from '../../../core/models/api-response.model';

@Component({
  selector: 'app-todo-list',
  imports: [CommonModule, DatePipe, RouterLink, NgIf],
  templateUrl: './todo-list.html',
  styleUrl: './todo-list.css',
})
export class TodoList {
  todos = signal<TodoModel[]>([]);
  isLoading = signal(false);
  error = signal<string | null>(null);

  constructor(private todoService: Todo) {}

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos() {
    this.isLoading.set(true);
    this.error.set(null);

    this.todoService.getTodos().subscribe({
      next: (res: ApiRespone<TodoModel[]>) => {
        this.todos.set(res.data ?? []);
        this.isLoading.set(false);
      },
      error: (err) => {
        this.isLoading.set(false);
        this.error.set(err.error?.message ?? 'Failed to load todos');
      },
    });
  }
}
