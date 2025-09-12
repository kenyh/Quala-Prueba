# 📦 Sistema de Gestión de Productos - QUALA

Este proyecto es una aplicación **fullstack** para la gestión de productos de la empresa **QUALA**, desarrollada con **Angular 17+ (Frontend)** y **.NET 6 (Backend)** con **SQL Server** como base de datos.

---

## 🚀 Tecnologías Utilizadas

### Backend
- .NET 6  
- Dapper (Micro-ORM)  
- SQL Server  
- JWT Authentication  
- Swagger/OpenAPI  

### Frontend
- Angular 17+  
- Ng-Zorro (UI Component Library)  
- RxJS (Programación Reactiva)  
- TypeScript  

---

## 📋 Estructura de la Base de Datos

### Tablas Creadas

**`kh_producto`**
| Campo             | Tipo                | Descripción                          |
|-------------------|---------------------|--------------------------------------|
| Id                | INT PRIMARY KEY IDENTITY | ID único del producto              |
| CodigoProducto    | INT NOT NULL UNIQUE | Código único del producto            |
| Nombre            | NVARCHAR(250) NOT NULL | Nombre del producto                 |
| Descripcion       | NVARCHAR(250) NOT NULL | Descripción del producto            |
| ReferenciaInterna | NVARCHAR(100) NOT NULL | Referencia interna                  |
| PrecioUnitario    | DECIMAL(18,2) NOT NULL | Precio unitario (≥ 0)               |
| Estado            | BIT NOT NULL DEFAULT 1 | Estado (1=Activo, 0=Inactivo)       |
| UnidadMedida      | NVARCHAR(50) NOT NULL | Unidad de medida                    |
| FechaCreacion     | DATETIME2 NOT NULL DEFAULT GETDATE() | Fecha de creación |


**`kh_users`**
| Campo             | Tipo                | Descripción                          |
|-------------------|---------------------|--------------------------------------|
| Id                | INT PRIMARY KEY IDENTITY | ID único del producto              |
| Username            | NVARCHAR(250) NOT NULL | Nombre del usuario                 |
| PasswordHash       | NVARCHAR(250) NOT NULL | Contraseña del usuario              |
| Email | NVARCHAR(100) NOT NULL | Email del usuario                                |
---

## 📊 Procedimientos Almacenados

### 🔍 Procedimientos de Consulta
| Procedimiento              | Descripción               | Parámetros        |
|-----------------------------|---------------------------|-------------------|
| `kh_sp_GetAllProductos`     | Obtiene todos los productos | -                 |
| `kh_sp_GetProductoById`     | Obtiene producto por ID   | `@Id INT`         |
| `kh_sp_GetProductoByCodigo` | Obtiene producto por código | `@CodigoProducto INT` |

### ✨ Procedimientos de Manipulación
| Procedimiento           | Descripción            | Parámetros |
|--------------------------|------------------------|------------|
| `kh_sp_CreateProducto`   | Crea un nuevo producto | `@CodigoProducto INT, @Nombre NVARCHAR(250), @Descripcion NVARCHAR(250), @ReferenciaInterna NVARCHAR(100), @PrecioUnitario DECIMAL(18,2), @Estado BIT, @UnidadMedida NVARCHAR(50)` |
| `kh_sp_UpdateProducto`   | Actualiza un producto  | `@Id INT, @CodigoProducto INT, @Nombre NVARCHAR(250), @Descripcion NVARCHAR(250), @ReferenciaInterna NVARCHAR(100), @PrecioUnitario DECIMAL(18,2), @Estado BIT, @UnidadMedida NVARCHAR(50)` |
| `kh_sp_DeleteProducto`   | Elimina un producto    | `@Id INT` |

---

## 🛠️ Configuración del Proyecto

### Prerrequisitos
- .NET 6 SDK  
- Node.js 18+  
- SQL Server  
- Angular CLI: `npm install -g @angular/cli`  

### Backend (.NET 6)
```bash
cd Backend
# Configurar connection string en appsettings.json
dotnet restore
dotnet run
```

### Frontend (Angular 17)
```bash
cd Frontend
npm install
# Configurar API URL en src/app/app.config.ts
ng serve
```

---

## 🔐 Autenticación

### Credenciales por Defecto
- Usuario: `prueba`   
- Contraseña: `admin`  

### Endpoints de Autenticación
- `POST /api/auth/login` → Iniciar sesión  
  **Body:**
  ```json
  { "username": "prueba", "password": "admin" }
  ```

---

## 📡 API Endpoints

### Productos
| Método | Endpoint                     | Descripción             |
|--------|-------------------------------|-------------------------|
| GET    | `/api/productos`              | Obtener todos los productos |
| GET    | `/api/productos/{id}`         | Obtener producto por ID |
| GET    | `/api/productos/codigo/{codigo}` | Obtener producto por código |
| POST   | `/api/productos`              | Crear nuevo producto    |
| PUT    | `/api/productos/{id}`         | Actualizar producto     |
| DELETE | `/api/productos/{id}`         | Eliminar producto       |

---

## 🎯 Funcionalidades Implementadas

✅ **CRUD Completo de Productos**  
- Crear, leer, actualizar y eliminar productos  
- Validaciones en frontend, backend y base de datos  
- Interfaz de usuario responsive con Ng-Zorro  

✅ **Sistema de Autenticación JWT**  
- Login seguro con tokens JWT  
- Protección de rutas con guards  
- Interceptor automático para headers de autenticación  

✅ **Validaciones de Negocio**  
- Código de producto único  
- Precio unitario positivo  
- Longitudes máximas de campos  
- Campos requeridos  

---

## 🚀 Uso de la Aplicación

1. Iniciar Sesión con las credenciales proporcionadas.  
2. Gestionar Productos:
   - Ver listado de productos
   - Crear nuevos productos
   - Editar productos existentes
   - Eliminar productos

---


## 📦 Estructura del Proyecto

```
QualaProductApp/
├── Backend/                 # API .NET 6
│   ├── Controllers/         # Controladores API
│   ├── Models/              # Modelos de datos
│   ├── Repositories/        # Acceso a datos
│   ├── Services/            # Lógica de negocio
│   └── Data/                # Contexto de base de datos
├── Frontend/                # Angular 17
│   ├── src/app/
│   │   ├── components/      # Componentes UI
│   │   ├── services/        # Servicios API
│   │   ├── interfaces/      # Interfaces TypeScript
│   │   └── guards/          # Protección de rutas
└── Database/                # Scripts SQL
    ├── Tables/              # Creación de tablas
    └── StoredProcedures/    # Procedimientos almacenados
```

---

## 🔧 Troubleshooting

- **Error de CORS**  
  Verificar que el backend tenga configurado CORS para el frontend.  
  Revisar las URLs en los archivos de configuración.  

- **Error de Conexión a BD**  
  Verificar connection string en `appsettings.json`.  
  Asegurar que SQL Server esté corriendo.  

- **Error de Autenticación**  
  Verificar que el token JWT se esté enviando correctamente.  
  Revisar la configuración de JWT en ambos lados.  
