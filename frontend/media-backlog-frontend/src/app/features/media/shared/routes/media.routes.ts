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
      import('../pages/media-view-simple/media-view-simple').then(m => m.MediaViewSimple)
  }, {
    path: 'edit/:type/:id',
    loadComponent: () =>
      import('../pages/media-edit/media-edit').then(m => m.MediaEdit)
  }
];