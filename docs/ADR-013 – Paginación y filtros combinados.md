# ADR-013: Paginación y filtros combinados

## Estado

Aceptado

## Contexto

La API debe permitir consultar siniestros por departamento, rango de fechas
y combinación de ambos, con paginación.

## Decisión

Se implementó un modelo de consulta flexible usando IQueryable, aplicando
los filtros de forma dinámica y retornando resultados paginados.

## Consecuencias

- Consultas eficientes.
- Mayor flexibilidad para el cliente.
