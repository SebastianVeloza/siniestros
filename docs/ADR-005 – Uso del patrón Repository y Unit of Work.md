# ADR-005: Repository y Unit of Work

## Estado

Aceptado

## Contexto

Era necesario abstraer el acceso a datos y evitar que la lógica de negocio
dependiera directamente de Entity Framework Core.

## Decisión

Se implementó el patrón Repository para cada agregado del dominio y un
Unit of Work para coordinar transacciones y persistencia.

## Consecuencias

- Menor acoplamiento con la infraestructura.
- Código más testeable.
- Control centralizado de la persistencia.
