# ADR-004: Contenerización de la solución

## Estado

Aceptado

## Contexto

La solución desarrollada corresponde a una API REST en **.NET 8**, diseñada bajo principios de **Clean Architecture**, **Domain-Driven Design (DDD)** y **CQRS**, que depende de una base de datos **SQL Server** para la persistencia de información de siniestros viales.

Durante el desarrollo se identificó la necesidad de contar con un entorno que fuera:

- Reproducible
- Fácil de ejecutar por terceros
- Independiente del sistema operativo
- Alineado con prácticas modernas de despliegue

Por estas razones se decidió contenerizar la aplicación y sus dependencias.

---

## Decisión

Se decidió utilizar **Docker y Docker Compose** para contenerizar la solución, definiendo contenedores independientes para:

- API REST desarrollada en .NET 8
- Base de datos SQL Server 2022

Adicionalmente, se incorporó un contenedor de inicialización encargado de ejecutar un script SQL (`bd_siniestros.sql`) para la creación automática de la base de datos y su esquema.

La comunicación entre contenedores se realiza mediante una red Docker privada y la configuración sensible, como la cadena de conexión, se inyecta mediante variables de entorno.

---

## Alternativas consideradas

### Instalación manual de la base de datos

- Requiere pasos adicionales para el evaluador.
- No garantiza reproducibilidad del entorno.
- Dependiente del sistema operativo.

### Uso de bases de datos embebidas

- No representa un escenario real de producción.
- Limitaciones funcionales frente a SQL Server.
- No cumple con los requisitos de persistencia real.

---

## Consecuencias

### Positivas

- Entorno completamente reproducible.
- Ejecución de la solución con un solo comando.
- Eliminación de configuraciones manuales.
- Facilidad para evaluación y pruebas.
- Persistencia de datos mediante volúmenes Docker.
- Preparación para futuros despliegues en la nube.

### Negativas

- Incremento en la complejidad inicial de configuración.
- Dependencia de conocimientos básicos de Docker para mantenimiento.

---

## Implementación

- Se definió un **Dockerfile multi-stage** para la API, optimizando el proceso de build y el tamaño de la imagen.
- Se configuró **Docker Compose** para orquestar los contenedores de la API, la base de datos y el inicializador.
- Se implementó una espera activa para garantizar que SQL Server esté disponible antes de ejecutar el script de inicialización.

---

## Resultado

La solución puede ejecutarse completamente mediante:

```bash
docker compose up --build
```
