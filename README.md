# API de Siniestros Viales

API REST desarrollada en .NET 8 para el registro y consulta de siniestros viales,
permitiendo filtros por departamento y rango de fechas, paginación y autenticación
mediante JWT.

## Arquitectura

La solución está basada en **Clean Architecture**, **Domain-Driven Design (DDD)** y **CQRS**,
con una clara separación de responsabilidades entre capas:

- **Domain**: Entidades y contratos del dominio.
- **Application**: Casos de uso (Commands / Queries) y lógica de aplicación.
- **Infrastructure**: Persistencia, repositorios y acceso a datos (EF Core).
- **WebAPI**: Exposición de endpoints REST, middlewares y autenticación.

El flujo de dependencias siempre apunta hacia el dominio.

## Seguridad

Se implementó autenticación basada en **JWT** con las siguientes características:

- Generación de **Access Token** y **Refresh Token**
- Almacenamiento de tokens en **cookies HTTP**
- Endpoint de logout que invalida el refresh token
- Middleware personalizado para:
  - Extraer el token desde cookies o headers
  - Validar el token y establecer el usuario en el contexto HTTP

## Middlewares personalizados

### ErrorHandlingMiddleware

Middleware global para el manejo centralizado de errores:

- Captura excepciones no controladas
- Registra el error con log4net
- Retorna una respuesta JSON uniforme

### JwtMiddleware

Middleware encargado de:

- Leer el token desde cookies o header Authorization
- Validar el token usando el servicio de autenticación
- Asignar el `ClaimsPrincipal` al `HttpContext`

## Pruebas

Se implementaron:

- **Pruebas unitarias** para Commands y Queries
- **Pruebas de integración** para Controllers
- EF Core InMemory para pruebas
- Manejo explícito de la limitación de transacciones en InMemory

## Contenerización

La aplicación fue contenerizada usando Docker y Docker Compose,
incluyendo tanto la API como la base de datos SQL Server.

Se definieron contenedores independientes para:

- API (.NET 8)
- Base de datos (SQL Server 2022)

La comunicación entre contenedores se realiza mediante una red Docker
privada, y los datos se persisten mediante volúmenes.

### Ejecución

```bash
docker compose up --build
```

## Ejecución del proyecto

1. Clonar el repositorio

```bash
git clone https://github.com/usuario/siniestros-api.git
```

3. Ejecutar script de BD/bd_siniestros.sql en sql server para crear la base de datos.
4. Configurar la cadena de conexión en `appsettings.json`
5. Ejecutar el proyecto WebAPI
6. Acceder a Swagger en:
   http://localhost:{puerto}/swagger

## Consideraciones finales

Durante el desarrollo se priorizó:

- Mantenibilidad
- Separación de responsabilidades
- Código testeable
- Buenas prácticas de arquitectura

## Algunas decisiones se documentan en los ADRs ubicados en la carpeta `/docs/adrs`.

## Decisiones Arquitectónicas

- Se implementa una **arquitectura en capas** para separar responsabilidades.
- Se utiliza **ASP.NET Core Web API (.NET 8)**.
- **Entity Framework Core** es usado como ORM.
- Se aplican principios **KISS** y **YAGNI**, evitando sobreingeniería.
- Los catálogos (Departamento, Ciudad, Tipo de Siniestro) se modelan como
  tablas independientes para garantizar integridad referencial.
