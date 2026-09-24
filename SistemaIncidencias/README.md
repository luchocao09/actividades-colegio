# Sistema de Incidencias

Aplicación web para gestionar incidencias urbanas con un backend en ASP.NET Core y un frontend estático en HTML/JavaScript.

## Requisitos

Antes de ejecutar el proyecto, asegurate de tener instalados estos elementos:

- .NET SDK 10.0
- MariaDB o MySQL compatible
- Git
- Un navegador moderno (Chrome, Edge, Firefox, etc.)
- Opcional: Python 3 para servir el frontend localmente

## Estructura del proyecto

- `IncidenciasAPI/`: backend en ASP.NET Core
- `Frontend/`: interfaz web del sistema
- `Diagrama_de_Clase.drawio` y `Diagrama(DER).drawio`: diagramas del proyecto

## Configuración de la base de datos

La API está configurada para usar una base de datos MySQL/MariaDB local con esta cadena de conexión por defecto:

```json
"DefaultConnection": "server=localhost;port=3306;database=sist_incidencias_v2;user=root;password=;"
```

### 1. Crear la base de datos

Desde MariaDB/MySQL ejecutá:

```sql
CREATE DATABASE sist_incidencias_v2;
```

Si preferís usar otra base de datos o credenciales, editá el archivo:

- `IncidenciasAPI/appsettings.json`

## Requisitos de seguridad / JWT

La aplicación usa autenticación JWT. La configuración actual incluye una clave por defecto en:

- `IncidenciasAPI/appsettings.json`

En un entorno real conviene cambiarla por una clave más segura y no dejarla fija en el código.

## Pasos para ejecutar la API

Abrí una terminal en la raíz del proyecto y ejecutá:

```bash
cd IncidenciasAPI

dotnet restore

dotnet build

dotnet run
```

La API quedará disponible en:

- `http://localhost:5281`
- Swagger en: `http://localhost:5281/swagger`

> La URL del backend está definida en `launchSettings.json` y también la usa el frontend.

## Pasos para ejecutar el frontend

Hay dos formas sencillas:

### Opción 1: Servirlo localmente con Python

```bash
cd Frontend
python -m http.server 5500
```

Luego abrí en el navegador:

- `http://localhost:5500`

### Opción 2: Abrir directamente el archivo HTML

También podés abrir `Frontend/index.html` en el navegador, pero en algunos casos es menos confiable por restricciones de seguridad del navegador. La opción recomendada es servirlo con un servidor local.

## Acceso a la app

Una vez levantados ambos servicios:

- Frontend: `http://localhost:5500`
- API: `http://localhost:5281`
- Swagger: `http://localhost:5281/swagger`

Desde la interfaz principal podrías iniciar sesión o registrarte como usuario/administrador según el flujo definido en el proyecto.

## Migraciones de base de datos (si hace falta)

El proyecto ya incluye migraciones en `IncidenciasAPI/Migrations/`. Si la base de datos está vacía y querés crear las tablas automáticamente:

```bash
cd IncidenciasAPI

dotnet tool install --global dotnet-ef

dotnet ef database update
```

Si `dotnet ef` no está disponible en tu entorno, instalalo como se indicó antes y volvé a ejecutar el comando.

## Problemas comunes

### Error de conexión a MySQL/MariaDB

- Verificá que el servicio de base de datos esté corriendo.
- Revisá que la base `sist_incidencias_v2` exista.
- Confirmá que el usuario/contraseña de la conexión sean correctos.

### El frontend no se conecta con la API

- Comprobá que la API esté levantada en `http://localhost:5281`.
- Revisá que el archivo `Frontend/js/config.js` tenga la URL correcta.

### Error de compilación de .NET

- Verificá que tengas instalado .NET SDK 10.0.
- Ejecutá:

```bash
dotnet --version
```

## Resumen rápido

```bash
cd IncidenciasAPI
dotnet restore
dotnet build
dotnet run

cd ../Frontend
python -m http.server 5500
```

Luego abrí:

- `http://localhost:5500`
- `http://localhost:5281/swagger`

## Nota final

Este proyecto está pensado para uso local de desarrollo. Para desplegarlo en producción, se recomienda:

- cambiar la cadena de conexión a un entorno real,
- usar credenciales seguras para la base de datos,
- guardar la clave JWT en variables de entorno,
- configurar HTTPS y CORS de forma más restrictiva.

Hecho por Fernando Franco, Gonzalo LaFuente, Gonzalo Cortaberria y Benjamin Gonzales
