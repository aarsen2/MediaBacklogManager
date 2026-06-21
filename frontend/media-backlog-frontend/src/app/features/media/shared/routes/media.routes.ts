import { Routes } from '@angular/router';

export const MEDIA_ROUTES: Routes = [
  {
    path: 'create',
    loadComponent: () =>
      import('../pages/media-creation/media-creation').then(m => m.MediaCreation)
  },
  {
    path: 'view/:type/:id',
    loadComponent: () =>
      import('../pages/media-view/media-view').then(m => m.MediaView)
  }
];