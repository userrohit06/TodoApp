import { Component, signal } from '@angular/core';
import { Todo } from '../../../core/services/todo';
import { Router } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { ApiRespone } from '../../../core/models/api-response.model';

@Component({
  selector: 'app-todo-create',
  imports: [FormsModule],
  templateUrl: './todo-create.html',
  styleUrl: './todo-create.css',
})
export class TodoCreate {
  model = {
    title: '',
    description: '',
  };

  isLoading = signal(false);
  error = signal<string | null>(null);

  constructor(private todoService: Todo, private router: Router) {}

  onSubmit(form: NgForm) {
    if (form.invalid) return;

    this.isLoading.set(true);
    this.error.set(null);

    this.todoService.createTodo(this.model).subscribe({
      next: (res: ApiRespone<any>) => {
        this.isLoading.set(false);
        this.router.navigate(['/todos']);
      },
      error: (err) => {
        this.isLoading.set(false);
        this.error.set(err.error?.message ?? 'Failed to create todo');
      },
    });
  }
}
