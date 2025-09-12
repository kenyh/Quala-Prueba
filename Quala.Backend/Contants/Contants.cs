namespace Quala.Backend.Constants
{
    public static class AppConstants
    {
        // Database Constants
        public static class Database
        {
            public const string DefaultConnection = "DefaultConnection";

        }

        // Stored Procedure Names
        public static class StoredProcedures
        {
            public const string UserGetByUsername = "kh_spUser_GetByUsername";
            public const string UserCreate = "kh_spUser_Create";

            public const string ProductGetAll = "kh_sp_GetAllProductos";
            public const string ProductGetById = "kh_sp_GetProductoById";
            public const string ProductCreate = "kh_sp_CreateProducto";
            public const string ProductUpdate = "kh_sp_UpdateProducto";
            public const string ProductDelete = "kh_sp_DeleteProducto";
            public const string ProductGetByCodigo = "kh_sp_GetProductoByCodigo";
        }

        // JWT Constants
        public static class Jwt
        {
            public const string Key = "Jwt:Key";
            public const string Issuer = "Jwt:Issuer";
            public const string Audience = "Jwt:Audience";
        }

        // Error Messages
        public static class ErrorMessages
        {
            public const string InvalidCredentials = "Credenciales inválidas";
            public const string UserNotFound = "Usuario no encontrado";
            public const string ProductNotFound = "Producto no encontrado";
            public const string UnauthorizedAccess = "Acceso no autorizado";
            public const string DatabaseError = "Error de base de datos";
            public const string ValidationError = "Error de validación";
            public const string ValidationUpdate = "ID del producto no coincide";
        }

        // Success Messages
        public static class SuccessMessages
        {
            public const string LoginSuccessful = "Login exitoso";
            public const string UserCreated = "Usuario creado exitosamente";
            public const string ProductCreated = "Producto creado exitosamente";
            public const string ProductUpdated = "Producto actualizado exitosamente";
            public const string ProductDeleted = "Producto eliminado exitosamente";
        }

    }
}