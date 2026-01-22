# ADR-004: Uso de MediatR

## Estado
Aceptado

## Contexto
Se buscaba desacoplar los controladores de la lógica de aplicación y evitar
dependencias directas entre capas.

## Decisión
Se utilizó MediatR como mediador para enviar Commands y Queries desde los
controladores hacia sus respectivos Handlers.

## Consecuencias
- Los controladores quedan delgados (thin controllers).
- Se elimina el acoplamiento directo entre WebAPI y Application.
- Mejora la mantenibilidad del código.
