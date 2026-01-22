# ADR-011: Pruebas unitarias

## Estado

Aceptado

## Contexto

La calidad del código y la mantenibilidad eran criterios de evaluación.

## Decisión

Se implementaron pruebas unitarias para:

- Handlers de Commands
- Handlers de Queries

Utilizando mocks para repositorios y Unit of Work.

## Consecuencias

- Detección temprana de errores.
- Mayor confianza en los cambios.
