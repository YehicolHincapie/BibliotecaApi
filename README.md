# Biblioteca API - Sistema de Consulta de Catálogo (.NET 8)

Backend modular desarrollado bajo los principios de **Clean Architecture**, **Domain-Driven Design (DDD)** y **CQRS (Command Query Responsibility Segregation)** con **MediatR** y **Entity Framework Core**.

---

## 🏛 Arquitectura de la Solución

```
src/
├── Core/
│   ├── Biblioteca.Domain/         # Entidades puras de dominio (Libro, Autor, Categoria)
│   └── Biblioteca.Application/    # DTOs, Queries CQRS con MediatR e Interfaces
├── Infrastructure/
│   └── Biblioteca.Infrastructure/ # DbContext, Mapeos Fluent API, Migraciones y SeedData
└── Presentation/
    └── Biblioteca.Api/            # Controladores REST, Inyección de Dependencias y Swagger UI
```

---

## 🚀 Endpoints de Consulta (CQRS)

| Método | Endpoint | Caso de Uso / Descripción |
| :--- | :--- | :--- |
| `GET` | `/api/libros` | **Query 1**: Obtiene todos los libros con Id, Título, ISBN, Año, Autor y Categoría. |
| `GET` | `/api/libros/{id}` | **Query 2**: Obtiene un libro por ID con información detallada de Autor y Categoría. |
| `GET` | `/api/libros/categoria/{categoriaId}` | **Query 3**: Obtiene los libros pertenecientes a una categoría específica. |

---

## ⚙️ Configuración y Ejecución

### 1. Requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (Opcional, la API incluye soporte dual InMemory para desarrollo rápido)

### 2. Ejecutar la API
```bash
dotnet run --project src/Presentation/Biblioteca.Api/Biblioteca.Api.csproj
```

La interfaz Swagger UI estará disponible automáticamente en la raíz:
👉 **`http://localhost:5248/`** o **`http://localhost:5000/`**

### 3. Configuración de Base de Datos (`appsettings.json`)
- Para usar **InMemory Database**: `"UseInMemoryDatabase": true`
- Para usar **SQL Server**:
  ```json
  "UseInMemoryDatabase": false,
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BibliotecaDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
  ```

### 4. Aplicar Migraciones en SQL Server
```bash
dotnet ef database update --project src/Infrastructure/Biblioteca.Infrastructure/Biblioteca.Infrastructure.csproj --startup-project src/Presentation/Biblioteca.Api/Biblioteca.Api.csproj
```
