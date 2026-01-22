# ADR-010: Middleware global de manejo de errores

## Estado

Aceptado

## Contexto

Se requería un manejo consistente de errores en toda la API.

## Decisión

Se implementó un middleware global de manejo de excepciones que:

- Captura errores no controlados
- Registra el error con log4net
- Devuelve respuestas JSON uniformes

## Consecuencias

- Respuestas más claras para el cliente.
- Mejor trazabilidad de errores.
- Código más limpio en los controladores.
