# ADR-009: Middleware personalizado para JWT

## Estado

Aceptado

## Contexto

Se necesitaba leer el token desde cookies o headers y establecer el usuario
autenticado en el contexto HTTP.

## Decisión

Se implementó un middleware personalizado que:

- Extrae el token desde cookies o Authorization header
- Valida el token
- Asigna el ClaimsPrincipal al HttpContext

## Consecuencias

- Mayor flexibilidad en el manejo de autenticación.
- Código centralizado.
