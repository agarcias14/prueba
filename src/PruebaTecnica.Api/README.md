# Prueba Técnica: CRUD Full Stack (.NET 9 + Blazor Server + SQL Server)

## 📋 Descripción General

Aplicación Full Stack que implementa un **CRUD completo** para la gestión de productos, desarrollada con:

- **Backend**: API REST en ASP.NET Core 9 con Entity Framework Core
- **Frontend**: Blazor Server (.NET 9)
- **Base de datos**: SQL Server (LocalDB)
- **Documentación**: Swagger UI
- **Control de versiones**: Git + GitHub (flujo con ramas `desarrollo` y `produccion`)

---

## 🎯 Tipo de Aplicación

**Blazor Server** (.NET 9) - Aplicación web con renderizado interactivo del lado del servidor mediante SignalR.

---

## 🏗️ Arquitectura del Proyecto

---

## 🎯 Funcionalidades Implementadas

### **Entidad Producto**

| Campo | Tipo | Restricciones |
|-------|------|---------------|
| **Id** | int | PK, Identity |
| **Codigo** | string(20) | Requerido, **único**, máx. 20 caracteres |
| **Nombre** | string(100) | Requerido, máx. 100 caracteres |
| **Precio** | decimal(18,2) | Requerido, **> 0** |
| **Stock** | int | Requerido, **>= 0** |
| **Activo** | bool | Default: `true` |
| **CreatedAt** | datetime | Asignado automáticamente al crear |
| **UpdatedAt** | datetime? | Asignado automáticamente al actualizar |

### **Endpoints API REST**

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| **GET** | `/api/productos` | Listar productos (con filtros opcionales) |
| **GET** | `/api/productos/{id}` | Obtener producto por ID |
| **POST** | `/api/productos` | Crear nuevo producto |
| **PUT** | `/api/productos/{id}` | Actualizar producto existente |
| **DELETE** | `/api/productos/{id}` | Eliminar producto (**soft delete**: `Activo = false`) |

### **Filtros de Búsqueda**

- `?codigo=ABC` - Buscar por código (contiene)
- `?nombre=Laptop` - Buscar por nombre (contiene)
- `?activo=tru	e` - Filtrar por estado activo/inactivo

**Ejemplo:**

````````

# Response

````````

### **Validaciones Implementadas**

✅ **Backend (API)**:
- Código único (índice en base de datos)
- Precio debe ser mayor a 0
- Stock no puede ser negativo
- Validación de duplicados al crear/editar
- Respuestas claras 400 (Bad Request) y 404 (Not Found)

✅ **Frontend (Blazor)**:
- Validaciones con DataAnnotations en formularios
- Confirmación modal antes de eliminar
- Feedback visual con mensajes de éxito/error
- Deshabilitación de botones durante operaciones asíncronas
- Validación de longitud máxima de campos

---

### **2. Configurar la Base de Datos**

#### **Opción A: Ejecutar migraciones (Recomendado)**

````````

# Response


````````

Si no tienes la herramienta `dotnet ef` instalada:

````````

# Response



#### **Opción B: Verificar que LocalDB está instalado**


Si aparece `MSSQLLocalDB`, está listo.

### **3. Verificar Connection String**

Editar `PruebaTecnica.Api/appsettings.json` si es necesario:


---

## ▶️ Ejecución del Proyecto

### **Opción 1: Usando Visual Studio 2022 (Recomendado)**

1. Abrir archivo `prueba.sln` en Visual Studio
2. **Configurar múltiples proyectos de inicio**:
   - Click derecho en la solución → **Propiedades**
   - Seleccionar **Proyectos de inicio múltiples**
   - Establecer acción **Iniciar** para:
     - `PruebaTecnica.Api`
     - `PruebaTecnica.Ui`
3. Presionar **F5** o click en **Iniciar**

### **Opción 2: Línea de comandos (2 terminales)**

#### **Terminal 1 - Ejecutar API**


✅ **API disponible en**: `http://localhost:5100`  
✅ **Swagger UI**: `http://localhost:5100/swagger`

#### **Terminal 2 - Ejecutar UI Blazor**


✅ **Aplicación web**: `http://localhost:5200`

---

## 🧪 Pruebas y Validación

### **1. Probar la API con Swagger**

1. Abrir navegador en `http://localhost:5100/swagger`
2. Probar endpoints en este orden:

**Crear producto:**


**Listar productos:**


**Obtener por ID:**


**Actualizar producto:**


**Eliminar (soft delete):**


**Verificar filtros:**




### **2. Probar la UI Blazor**

1. Abrir `http://localhost:5200`
2. Navegar a **Productos** en el menú lateral
3. Probar funcionalidades:
   - ✅ **Listado** con filtros por código/nombre/estado
   - ✅ **Crear** nuevo producto con validaciones
   - ✅ **Ver detalle** de un producto
   - ✅ **Editar** producto existente
   - ✅ **Eliminar** con diálogo de confirmación (soft delete)
   - ✅ Verificar que los productos eliminados aparecen como "Inactivo"

### **3. Verificar Base de Datos**

En Visual Studio:
- **View** → **SQL Server Object Explorer**
- Expandir: `(localdb)\MSSQLLocalDB` → `Databases` → `PruebaTecnicaDb` → `Tables`
- Click derecho en `dbo.Productos` → **View Data**

O usar SQL Server Management Studio:



---

## 🔄 Flujo de Trabajo Git/GitHub

### **Estructura de Ramas**

- **`produccion`**: Rama estable con código de producción
- **`desarrollo`**: Rama de integración para desarrollo
- **`feature/*`**: Ramas para nuevas funcionalidades

### **Flujo Implementado**




### **Proceso de Desarrollo**

1. **Crear rama feature desde desarrollo:**

2. **Desarrollar y hacer commits:**

3. **Merge a desarrollo:**


4. **Pull Request de desarrollo → produccion:**
   - Crear PR en GitHub
   - Agregar descripción y checklist
   - Revisar cambios
   - Merge del PR

---

## 📸 Evidencias

Ver carpeta `/evidencias` en el repositorio con:

- ✅ **1-swagger-crud.gif**: Video del CRUD funcionando en Swagger
- ✅ **2-blazor-ui-crud.gif**: Video de la UI Blazor en acción
- ✅ **3-base-datos-productos.png**: Captura de la tabla Productos con datos
- ✅ **4-github-ramas.png**: Captura de las ramas en GitHub
- ✅ **5-pull-request-merged.png**: Captura del Pull Request mergeado

---

## 🛠️ Tecnologías y Librerías

### **Backend**
- **ASP.NET Core 9** - Framework web
- **Entity Framework Core 9** - ORM para base de datos
- **SQL Server LocalDB** - Base de datos local
- **Swashbuckle 6.x** - Generación de documentación Swagger

### **Frontend**
- **Blazor Server (.NET 9)** - Framework UI interactivo
- **Bootstrap 5.3** - Framework CSS
- **Bootstrap Icons** - Iconografía

### **Herramientas**
- **Git & GitHub** - Control de versiones
- **Visual Studio 2022** - IDE
- **ScreenToGif** - Grabación de evidencias

---

## 📦 Estructura de Commits (Ejemplos)




---

## 🚨 Notas Importantes

### **Soft Delete**
Los productos **NO se eliminan físicamente** de la base de datos. Al ejecutar DELETE, se marca el campo `Activo = false`. Esto permite:
- Mantener historial de productos
- Recuperar productos eliminados si es necesario
- Auditoría de cambios

### **CORS**
El backend está configurado para aceptar peticiones desde:
- `http://localhost:5200` (Blazor UI en desarrollo)
- `https://localhost:5201` (HTTPS alternativo)

### **LocalDB**
La base de datos se crea automáticamente con las migraciones de Entity Framework. No requiere instalación adicional de SQL Server completo.

### **Puertos**
- **API**: `http://localhost:5100`
- **UI**: `http://localhost:5200`

---

## 🐛 Troubleshooting

### **Error: "Cannot connect to database"**
✅ **Solución:**




### **Error: "CORS policy blocked"**
✅ **Solución:**
- Verificar que ambos proyectos (API y UI) estén corriendo
- Verificar en `PruebaTecnica.Api/Program.cs` que CORS esté habilitado:





### **Error: "Swagger not loading"**
✅ **Solución:**
- Verificar que la API esté corriendo en `http://localhost:5100`
- Navegar directamente a `http://localhost:5100/swagger`

### **Error: "The ConnectionString property has not been initialized"**
✅ **Solución:**
- Verificar que existe el archivo `appsettings.json` con el ConnectionString
- Verificar que el proyecto API sea el proyecto de inicio

### **Error: "Botones en Blazor no funcionan"**
✅ **Solución:**
- Verificar que las páginas tengan `@rendermode InteractiveServer`
- Limpiar y reconstruir la solución

---

## 📚 Referencias y Documentación

- [Documentación oficial ASP.NET Core](https://learn.microsoft.com/aspnet/core)
- [Documentación Blazor](https://learn.microsoft.com/aspnet/core/blazor)
- [Entity Framework Core](https://learn.microsoft.com/ef/core)
- [Swagger/OpenAPI](https://swagger.io/specification/)
- [Git Flow](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow)

---

## 👨‍💻 Autor

**Axel Garcia**  
📧 Email:agarcias14@gmail.com  
🔗 GitHub: [agarcias14](https://github.com/agarcias14)

---

## 📄 Licencia

Este proyecto fue desarrollado como **prueba técnica** para evaluación de habilidades en desarrollo Full Stack con .NET.

---

## 🙏 Agradecimientos

Proyecto desarrollado siguiendo las mejores prácticas de:
- Arquitectura limpia (Clean Architecture)
- Principios SOLID
- Patrones de diseño (Repository, Service Layer)
- Convenciones de .NET

---

_Última actualización: Marzo 2026_

git add .
git commit -m "fix: Reparar Index.razor y eliminar errores de compilación"
git push origin feature/crud-productos-api---

## 🚀 Cómo Ejecutar la Solución Completa

### **Prerrequisitos**
- .NET 9 SDK instalado
- SQL Server LocalDB (incluido con Visual Studio)
- Git

---

### **1. Clonar el repositorio**


### **2. Restaurar dependencias**

### **3. Aplicar migraciones a la base de datos**

### **4. Ejecutar la API (Terminal 1)**

**URLs de la API:**
- API: `http://localhost:5100`
- Swagger UI: `http://localhost:5100/swagger`

### **5. Ejecutar el Frontend Blazor (Terminal 2 - Nueva ventana)**

**URL de Blazor:**
- Blazor UI: `http://localhost:5096`

---

## 🧪 Probar la Aplicación

### **Opción 1: Desde Swagger (API directa)**
1. Abre `http://localhost:5100/swagger`
2. Expande los endpoints y prueba el CRUD completo

### **Opción 2: Desde Blazor UI (Frontend)**
1. Abre `http://localhost:5096`
2. Click en **"Productos"** en el menú
3. Prueba crear, editar, ver detalle y eliminar productos

---

## 📊 Arquitectura del Proyecto



---

## 🔧 Tecnologías Utilizadas

- **.NET 9**
- **ASP.NET Core Web API**
- **Entity Framework Core** (migraciones + SQL Server)
- **Blazor Server** (renderizado interactivo)
- **SQL Server LocalDB**
- **Swagger/OpenAPI** (documentación API)
- **Bootstrap 5** (estilos UI)

---

## 📸 Evidencias

Las capturas de pantalla y videos del funcionamiento completo se encuentran en la carpeta `/evidencias/` (pendiente de agregar).

---

## 🌿 Flujo de Ramas Git

- **`main`**: Rama principal (código inicial)
- **`desarrollo`**: Rama de desarrollo (integración de features)
- **`produccion`**: Rama de producción (releases estables)
- **`feature/*`**: Ramas de características específicas

### **Pull Request realizado:**
`desarrollo` → `produccion` (Release v1.0)

---

## 👨‍💻 Autor

Álvaro García - [GitHub](https://github.com/agarcias14)

---

## 📝 Notas

- El **soft delete** está implementado: al eliminar un producto, solo se marca como `Activo = false`.
- Las **validaciones** están implementadas tanto en backend (API) como en frontend (Blazor).
- El **código único** se valida a nivel de base de datos con un índice único.

