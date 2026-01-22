# ADR-012: Pruebas de integración con EF InMemory

## Estado

Aceptado

## Contexto

Era necesario probar los endpoints completos sin depender de una base
de datos real.

## Decisión

Se utilizó EF Core InMemory para pruebas de integración, configurando
un WebApplicationFactory personalizado.

Dado que InMemory no soporta transacciones, estas se ignoran durante
las pruebas.

## Consecuencias

- Pruebas rápidas y aisladas.
- Comportamiento consistente en producción.
