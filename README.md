# 📚 Sistema de Gestión de Biblioteca (Examen T2)

Solución integral para la gestión de **Libros** y **Editoriales**, desarrollada con una arquitectura orientada a servicios (SOA) separando el Backend (API) del Frontend (MVC).

## 🚀 Tecnologías Utilizadas

* **Base de Datos:** SQL Server (Procedimientos Almacenados).
* **Backend:** ASP.NET Core Web API (.NET 6 / .NET 8).
* **Frontend:** ASP.NET Core MVC (Razor Views + Bootstrap 5).
* **Acceso a Datos:** ADO.NET con Patrón DAO y Repository.
* **Comunicación:** `HttpClient` y `Newtonsoft.Json`.

---

## 🏗️ Arquitectura de la Solución

La solución consta de dos proyectos principales:

1.  **`EXAMEN_T2` (Backend):**
    * Expone endpoints RESTful.
    * Maneja la lógica de negocio y acceso a datos (DAO).
    * Conexión a SQL Server mediante `Microsoft.Data.SqlClient`.
    * Documentación automática con **Swagger**.

2.  **`ExamenT2_Cliente` (Frontend):**
    * Aplicación Web MVC.
    * Consume la API mediante `HttpClient`.
    * Diseño responsivo con Bootstrap e iconos `Bootstrap-Icons`.
    * Validaciones de formularios (`DataAnnotations`).

---

## 📋 Funcionalidades Implementadas

### Módulo de Editoriales
* **Listar:** Visualización de editoriales con el nombre de su País (JOIN).
* **Filtrar:** Búsqueda por País mediante ComboBox (Dropdown) y por Nombre.
* **Crear:** Registro con validación y selección de País desde BD.
* **Editar/Eliminar:** Mantenimiento completo.

### Módulo de Libros
* **Listar:** Visualización de libros con el nombre de su Editorial (JOIN).
* **Filtrar:** Búsqueda dinámica por **Autor**.
* **Crear/Editar:** Carga dinámica de Editoriales en ComboBox.
* **Detalles:** Vista de solo lectura con diseño de tarjeta.

---


## 🚀 Guía de Inicio Rápido

Para ejecutar el sistema completo, sigue estos pasos:

### 1. Base de Datos
Restaura la base de datos `Biblioteca` en tu servidor SQL local. Asegúrate de que existan las tablas `Libro`, `Editorial` y `Pais` junto con sus procedimientos almacenados (`sp_Listar...`, `sp_Insertar...`, etc.).

### 2. Configuración
* **API:** Verifica que la cadena de conexión en `EXAMEN_T2/appsettings.json` apunte a tu servidor local.
* **Cliente:** El cliente web está preconfigurado para buscar la API en `http://localhost:7270`.

### 3. Ejecución
Ambos proyectos deben correr al mismo tiempo para que el sistema funcione.

**Opción Recomendada (Visual Studio):**
1.  Abre la solución.
2.  Configura **Proyectos de inicio múltiples** en las propiedades de la solución.
3.  Establecer acción **"Iniciar"** tanto para `EXAMEN_T2` como para `ExamenT2_Cliente`.
4.  Presiona **F5**.

---
## 📂 Estructura del Proyecto

El repositorio está organizado de la siguiente manera:

```text
ExamenT2_Completo/
│
├── 📁 EXAMEN_T2/         # PROYECTO BACKEND (Web API)
│   ├── Controllers/           # Endpoints (Libro, Editorial)
│   ├── Repositorio/           # DAO y Lógica de Datos
│   ├── appsettings.json       # Cadena de Conexión
│   └── ...
│
├── 📁 ExamenT2_Cliente/       # PROYECTO FRONTEND (MVC)
│   ├── Controllers/           # Lógica de consumo de API (HttpClient)
│   ├── Views/                 # Vistas Razor (Libro, Editorial)
│   └── ...
│
└── 📄 README.md               # Este archivo de documentación
```
**Desarrollo de Aplicaciones Web - Examen T2**
