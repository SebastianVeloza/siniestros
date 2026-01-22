# ADR-002: Modelado del dominio siguiendo DDD

## Estado

Aceptado

## Contexto

El sistema gestiona información crítica relacionada con siniestros viales,
por lo que era necesario un modelo de dominio claro y coherente.

## Decisión

Se aplicaron principios de Domain-Driven Design (DDD), definiendo entidades
centrales como Siniestros y entidades de soporte como Departamentos,
Ciudades y Tipos de Siniestro.

Las reglas del dominio se mantienen en la capa Domain, separadas de la
persistencia y de la presentación.

## Consecuencias

- El modelo refleja mejor el negocio.
- Se evita lógica dispersa en capas incorrectas.
- Se facilita la evolución del dominio.
