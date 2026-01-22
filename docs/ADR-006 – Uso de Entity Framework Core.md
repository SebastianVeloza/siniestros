# ADR-006: Uso de Entity Framework Core

## Estado

Aceptado

## Contexto

Se requería un ORM moderno, compatible con .NET 8 y con soporte para LINQ,
migraciones y pruebas.

## Decisión

Se seleccionó Entity Framework Core como ORM principal, utilizando SQL Server
como base de datos en producción.

## Consecuencias

- Desarrollo más rápido.
- Integración natural con .NET.
- Soporte para pruebas con InMemory.
