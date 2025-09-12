import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

// NgZorro imports
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal'; 
import { NzModalModule } from 'ng-zorro-antd/modal'; 
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzTagModule } from 'ng-zorro-antd/tag'; // 

import { ProductoService } from '../../services/producto.service';
import { Producto } from '../../interfaces/producto.interface';

@Component({
  selector: 'app-producto-list',
  standalone: true,
  imports: [
    CommonModule,
    NzTableModule,
    NzButtonModule,
    NzIconModule,
    NzCardModule,
    NzSpinModule,
    NzTagModule,
    NzModalModule
  ],
  templateUrl: './producto-list.component.html',
  styleUrls: ['./producto-list.component.css']
})
export class ProductoListComponent implements OnInit {
  private productoService = inject(ProductoService);
  private message = inject(NzMessageService);
  private modal = inject(NzModalService);
  private router = inject(Router);

  productos: Producto[] = [];
  loading = true;

  ngOnInit(): void {
    this.cargarProductos();
  }

  cargarProductos(): void {
    this.loading = true;
    this.productoService.getAllProductos().subscribe({
      next: (data) => {
        console.log("consultar productos",data);
        this.productos = data;
        this.loading = false;
      },
      error: (error) => {
        this.message.error('Error al cargar los productos');
        console.error('Error:', error);
        this.loading = false;
      }
    });
  }

  crearProducto(): void {
    this.router.navigate(['/productos/nuevo']);
  }

  editarProducto(id: number): void {
    this.router.navigate(['/productos/editar', id]);
  }

  eliminarProducto(id: number): void {
    this.modal.confirm({
      nzTitle: '¿Estás seguro de eliminar este producto?',
      nzContent: 'Esta acción no se puede deshacer.',
      nzOkText: 'Sí, eliminar',
      nzOkType: 'primary',
      nzOkDanger: true,
      nzCancelText: 'Cancelar',
      nzOnOk: () => {
        this.productoService.deleteProducto(id).subscribe({
          next: () => {
            this.message.success('Producto eliminado correctamente');
            this.cargarProductos();
          },
          error: (error) => {
            this.message.error('Error al eliminar el producto');
            console.error('Error:', error);
          }
        });
      }
    });
  }

  // Formatear precio para mostrar
  formatearPrecio(precio: number): string {
    return new Intl.NumberFormat('es-CO', {
      style: 'currency',
      currency: 'COP'
    }).format(precio);
  }

  // Formatear estado para mostrar
  formatearEstado(estado: boolean): string {
    return estado ? 'Activo' : 'Inactivo';
  }
}