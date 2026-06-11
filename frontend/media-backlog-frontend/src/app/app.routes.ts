import { Routes } from '@angular/router';
import { Login } from './core/auth/pages/login/login';
import { authGuard } from './core/auth/auth-guard';
import { Logout } from './core/auth/pages/logout/logout';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'home'
    },
    {
        path: 'login',
        component: Login
    },
    {
        path: 'logout',
        component: Logout
    },
    {
        path: 'home',
        canActivate: [authGuard],
        loadChildren: () =>
            import('./features/home/home.routes')
                .then(m => m.HOME_ROUTES)
    },
    {
        path: 'media',
        canActivate: [authGuard],
        loadChildren: () =>
            import('./features/media/shared/routes/media.routes').then(m => m.MEDIA_ROUTES)
    },
    {
        path: '**',
        redirectTo: 'home'
    }

];
