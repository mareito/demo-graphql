# Product Management - Monorepo

Aplicación de gestión de productos con arquitectura monorepo. Backend en .NET 8 con Clean Architecture y GraphQL, frontend en React con Vite y Tailwind CSS.

## Estructura del Proyecto

```
/
├── backend/                          # Backend .NET 8
│   ├── ProductManagement.sln         # Solución .NET
│   ├── ProductManagement.Domain/     # Entidades del dominio
│   ├── ProductManagement.Application/# Lógica de aplicación y GraphQL
│   ├── ProductManagement.Infrastructure/ # EF Core y PostgreSQL
│   └── ProductManagement.WebAPI/     # API con HotChocolate
└── frontend/                         # Frontend React + Vite
    └── src/
        ├── components/               # Componentes React
        └── services/                 # Cliente GraphQL
```

## Requisitos Previos

- .NET 8 SDK
- Node.js 18+ y npm
- PostgreSQL 12+

## Configuración de Base de Datos

1. Asegúrate de tener PostgreSQL corriendo en `localhost:5432`
2. Crea la base de datos:
   ```sql
   CREATE DATABASE AranzaDB;
   ```
3. Las credenciales por defecto son:
   - Usuario: `postgres`
   - Contraseña: `admin`

## Instalación y Ejecución

### Backend

```powershell
# Navegar al directorio raíz
cd backend/ProductManagement.WebAPI

# Aplicar migraciones a la base de datos
dotnet ef database update --project ../ProductManagement.Infrastructure

# Ejecutar el backend
dotnet run
```

El backend estará disponible en:
- API: `http://localhost:5000`
- GraphQL Playground: `http://localhost:5000/graphql`

### Frontend

**IMPORTANTE**: Debido a restricciones de PowerShell, debes instalar las dependencias manualmente.

```powershell
# Navegar al directorio frontend
cd frontend

# Instalar dependencias (ejecutar desde CMD o ajustar política de ejecución)
# Opción 1: Usar CMD
cmd /c "npm install"

# Opción 2: Cambiar política de ejecución temporalmente (PowerShell como Admin)
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process
npm install

# Ejecutar el frontend
npm run dev
```

El frontend estará disponible en: `http://localhost:5173`

## Operaciones GraphQL

### Query: Obtener Productos

```graphql
query GetProducts {
  getProducts {
    id
    name
    description
    price
    stock
    categoryId
    category {
      id
      name
      description
    }
  }
}
```

### Mutation: Añadir Producto

```graphql
mutation AddProduct($input: AddProductInputInput!) {
  addProduct(input: $input) {
    id
    name
    description
    price
    stock
    categoryId
  }
}
```

Variables:
```json
{
  "input": {
    "name": "Producto Ejemplo",
    "description": "Descripción del producto",
    "price": 99.99,
    "stock": 10,
    "categoryId": 1
  }
}
```

## Datos de Prueba

Para probar la aplicación, primero necesitas crear categorías en la base de datos:

```sql
INSERT INTO "Categories" ("Name", "Description", "CreatedAt")
VALUES 
  ('Electrónica', 'Dispositivos electrónicos', NOW()),
  ('Ropa', 'Prendas de vestir', NOW()),
  ('Alimentos', 'Productos alimenticios', NOW());
```

## Características

### Backend
- ✅ Clean Architecture (Domain, Application, Infrastructure, WebAPI)
- ✅ GraphQL con HotChocolate
- ✅ Entity Framework Core con PostgreSQL
- ✅ CORS configurado para frontend
- ✅ Migraciones de base de datos

### Frontend
- ✅ React 18 con Vite
- ✅ Tailwind CSS para estilos
- ✅ Axios para peticiones HTTP
- ✅ Cliente GraphQL personalizado
- ✅ Componente de lista de productos
- ✅ Modal para añadir productos con validación
- ✅ Interfaz responsiva

## Tecnologías Utilizadas

**Backend:**
- .NET 8
- HotChocolate (GraphQL)
- Entity Framework Core
- Npgsql (PostgreSQL)

**Frontend:**
- React 18
- Vite
- Tailwind CSS
- Axios

## Solución de Problemas

### Error de PowerShell con npm
Si encuentras errores al ejecutar npm, usa una de estas soluciones:
1. Ejecuta desde CMD: `cmd /c "npm install"`
2. Cambia la política de ejecución temporalmente (como Admin):
   ```powershell
   Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process
   ```

### Error de conexión a PostgreSQL
Verifica que PostgreSQL esté corriendo y las credenciales en `appsettings.json` sean correctas.

### Error de CORS
Asegúrate de que el frontend esté corriendo en `http://localhost:5173` o actualiza la política CORS en `Program.cs`.
