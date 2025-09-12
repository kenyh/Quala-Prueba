import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NavbarComponent],
  template: `
    <app-navbar *ngIf="showNavbar()"></app-navbar>
    <div class="content">
      <router-outlet></router-outlet>
    </div>
  `,
  styles: [`
    .content {
      padding: 24px;
      min-height: calc(100vh - 64px);
    }
  `]
})
export class AppComponent {
  showNavbar(): boolean {
    // No mostrar navbar en la página de login
    return !window.location.pathname.includes('/login');
  }
}