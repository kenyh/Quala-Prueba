import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

// NgZorro imports
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzMessageService } from 'ng-zorro-antd/message';

import { ProductoService } from '../../services/producto.service';
import { Producto } from '../../interfaces/producto.interface';

@Component({
  selector: 'app-producto-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NzCardModule,
    NzFormModule,
    NzInputModule,
    NzInputNumberModule,
    NzSelectModule,
    NzSwitchModule,
    NzButtonModule
  ],
  templateUrl: './producto-form.component.html',
  styleUrls: ['./producto-form.component.css']
})
export class ProductoFormComponent implements OnInit {
  private productoService = inject(ProductoService);
  private message = inject(NzMessageService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  producto: Producto = {
    codigoProducto: 0,
    nombre: '',
    descripcion: '',
    referenciaInterna: '',
    precioUnitario: 0,
    estado: true,
    unidadMedida: ''
  };

  isEdit = false;
  loading = false;

  // Opciones para unidad de medida
  unidadesMedida = [
    'Unidad',
    'Docena',
    'Gramo',
    'Kilogramo',
    'Litro',
    'Mililitro',
    'Metro',
    'Centímetro'
  ];

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.isEdit = true;
      this.cargarProducto(Number(id));
    }
  }

  cargarProducto(id: number): void {
    this.loading = true;
    this.productoService.getProductoById(id).subscribe({
      next: (data) => {
        this.producto = data;
        this.loading = false;
      },
      error: (error) => {
        this.message.error('Error al cargar el producto');
        console.error('Error:', error);
        this.loading = false;
      }
    });
  }

  guardarProducto(): void {
    this.loading = true;

    const observable = this.isEdit
      ? this.productoService.updateProducto(this.producto.id!, this.producto)
      : this.productoService.createProducto(this.producto);

    observable.subscribe({
      next: () => {
        this.message.success(
          `Producto ${this.isEdit ? 'actualizado' : 'creado'} correctamente`
        );
        this.router.navigate(['/productos']);
      },
      error: (error) => {
        this.message.error(
          `Error al ${this.isEdit ? 'actualizar' : 'crear'} el producto`
        );
        console.error('Error:', error);
        this.loading = false;
      }
    });
  }

  cancelar(): void {
    this.router.navigate(['/productos']);
  }

  formatCurrency = (value: number) => {
    return `$ ${value}`; // Formats the value as currency
  }

}