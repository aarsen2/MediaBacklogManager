import { Routes } from '@angular/router';

export const MEDIA_ROUTES: Routes = [
  {
    path: 'create',
    loadComponent: () =>
      import('../../movies/pages/movie-creation/movie-creation').then(m => m.MovieCreation)
  },
  {
    path: 'view/:type/:id',
    loadComponent: () =>
      import('../pages/media-view/media-view').then(m => m.MediaView)
  }
];