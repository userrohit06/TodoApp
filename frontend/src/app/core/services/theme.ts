import { Injectable } from '@angular/core';

export type ThemeType = 'light' | 'dark';

@Injectable({
  providedIn: 'root',
})
export class Theme {
  private storageKey = 'app-theme';

  constructor() {
    const saved = (localStorage.getItem(this.storageKey) as ThemeType | null) ?? 'light';
    this.setTheme(saved);
  }

  get currentTheme(): ThemeType {
    return (localStorage.getItem(this.storageKey) as ThemeType | null) ?? 'light';
  }

  setTheme(theme: ThemeType) {
    const root = document.documentElement;

    if (theme === 'dark') {
      root.classList.add('dark');
    } else {
      root.classList.remove('dark');
    }

    localStorage.setItem(this.storageKey, theme);
  }

  toggleTheme() {
    this.setTheme(this.currentTheme === 'light' ? 'dark' : 'light');
  }
}
