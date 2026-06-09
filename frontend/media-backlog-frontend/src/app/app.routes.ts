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
        path: 'movies',
        canActivate: [authGuard],
        loadChildren: () =>
            import('./features/movies/movies.routes').then(m => m.MOVIES_ROUTES)
    },
    {
        path: '**',
        redirectTo: 'home'
    }

];
