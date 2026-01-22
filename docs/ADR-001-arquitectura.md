# ADR-001: Uso de Clean Architecture

## Estado

Aceptado

## Contexto

La solución requiere ser mantenible, escalable y fácilmente testeable.
Además, se solicitó explícitamente una separación clara de responsabilidades
y un flujo de dependencias bien definido entre capas.

## Decisión

Se adoptó Clean Architecture como patrón principal de diseño, separando la
solución en las siguientes capas:

- Domain
- Application
- Infrastructure
- WebAPI

Las dependencias fluyen siempre hacia el dominio, evitando acoplamientos
innecesarios.

## Consecuencias

- La lógica de negocio queda aislada de frameworks y detalles técnicos.
- Se facilita la escritura de pruebas unitarias.
- El proyecto es más fácil de extender y mantener.
