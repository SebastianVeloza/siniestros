# ADR-008: Uso de cookies para tokens JWT

## Estado

Aceptado

## Contexto

Se buscaba evitar el almacenamiento de tokens en localStorage por razones
de seguridad.

## Decisión

Los tokens JWT se almacenan en cookies HTTP, permitiendo que el backend
los lea automáticamente y reduciendo riesgos de XSS.

## Consecuencias

- Mayor seguridad.
- Mejor manejo de sesiones.
- Necesidad de middleware personalizado.
