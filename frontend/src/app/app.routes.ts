import { Routes } from '@angular/router';
import { AuthLayout } from './layout/auth-layout/auth-layout';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { MainLayout } from './layout/main-layout/main-layout';
import { authGuard } from './core/guards/auth-guard';
import { TodoList } from './features/todo/todo-list/todo-list';
import { TodoCreate } from './features/todo/todo-create/todo-create';

export const routes: Routes = [
  // public routes
  {
    path: 'auth',
    component: AuthLayout,
    children: [
      { path: 'login', component: Login },
      { path: 'register', component: Register },
      { path: '', pathMatch: 'full', redirectTo: 'login' },
    ],
  },

  // private routes
  {
    path: '',
    component: MainLayout,
    canActivate: [authGuard],
    children: [
      { path: 'todos', component: TodoList },
      { path: 'todos/create', component: TodoCreate },
      { path: '', pathMatch: 'full', redirectTo: 'todos' },
    ],
  },

  // fallback: anything unknown -> home
  { path: '**', redirectTo: '' },
];
