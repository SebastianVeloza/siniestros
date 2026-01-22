# ADR-003: Uso de CQRS

## Estado

Aceptado

## Contexto

La aplicación requiere operaciones de escritura (registro de siniestros)
y operaciones de lectura con filtros, paginación y combinaciones de criterios.

## Decisión

Se implementó el patrón CQRS separando:

- Commands para escritura
- Queries para lectura

Cada caso de uso se maneja mediante un Handler específico usando MediatR.

## Consecuencias

- Código más claro y enfocado por responsabilidad.
- Mayor facilidad para pruebas unitarias.
- Posibilidad de escalar lecturas y escrituras de forma independiente.
