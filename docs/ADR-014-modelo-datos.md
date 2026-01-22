# ADR-002: Uso de tablas maestras

## Contexto

Se deben almacenar departamentos, ciudades y tipos de siniestro.

## Decisión

Se crean tablas independientes para estos catálogos en lugar de
almacenarlos como texto libre.

## Consecuencias

- Integridad referencial
- Evita duplicidad de datos
- Facilita reportes y validaciones
