import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [

    {
        path: 'login',
        loadComponent: () => import('./components/login/login.component').then(m => m.LoginComponent)
    },
    {
        path: 'productos',
        loadComponent: () => import('./components/producto-list/producto-list.component').then(m => m.ProductoListComponent),
        canActivate: [authGuard]
    },
    {
        path: 'productos/nuevo',
        loadComponent: () => import('./components/producto-form/producto-form.component').then(m => m.ProductoFormComponent),
        canActivate: [authGuard]
    },
    { 
    path: 'productos/editar/:id', 
    loadComponent: () => import('./components/producto-form/producto-form.component').then(m => m.ProductoFormComponent),
    canActivate: [authGuard]
    },
    { path: '', redirectTo: '/productos', pathMatch: 'full' },
    { path: '**', redirectTo: '/productos' }
];
