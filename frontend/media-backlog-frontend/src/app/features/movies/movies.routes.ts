import { Routes } from '@angular/router';

export const MOVIES_ROUTES: Routes = [
  {
    path: 'create',
    loadComponent: () =>
      import('./pages/movie-creation/movie-creation').then(m => m.MovieCreation)
  },
];