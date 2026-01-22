# ADR-001: Modelado de vehículos y víctimas en los siniestros

## Estado

Aceptado

## Contexto

Dentro del requerimiento funcional se establece que un siniestro vial debe
permitir registrar información como:

- Tipo de siniestro
- Vehículos involucrados
- Número de víctimas

Durante el análisis del dominio surgió la decisión de determinar si los
vehículos y las víctimas debían modelarse como entidades independientes
relacionadas al siniestro o si debían representarse como atributos agregados
del mismo.

El objetivo principal del sistema es el registro y consulta de siniestros,
con filtros por ubicación y rango de fechas, sin requerimientos explícitos
de trazabilidad individual de vehículos o personas.

---

## Decisión

Se decidió **NO modelar vehículos ni víctimas como entidades independientes**
y, en su lugar, representarlos como **atributos agregados del siniestro**:

- `vehiculos_involucrados` como valor numérico.
- `numero_victimas` como valor numérico.

El siniestro actúa como **Aggregate Root**, concentrando toda la información
relevante del evento.

---

## Justificación

- No existe requerimiento de consultar vehículos o víctimas de forma individual.
- No se requiere mantener historial, estado ni atributos propios de cada vehículo o persona.
- Evita complejidad innecesaria en el modelo de dominio.
- Reduce el número de tablas y relaciones.
- Mejora el rendimiento en consultas de lectura.
- Mantiene el enfoque del dominio en el evento (siniestro) y no en sus componentes.

Desde el punto de vista de **Domain-Driven Design**, los vehículos y víctimas
no presentan comportamiento ni identidad propia dentro del dominio actual,
por lo que se tratan como **Value Objects simples**.

---

## Alternativas consideradas

### Modelar vehículos como entidad

- Mayor complejidad del modelo.
- Requeriría relaciones uno-a-muchos.
- No aporta valor al caso de uso actual.

### Modelar víctimas como entidad

- Implica manejo de datos sensibles.
- Requeriría reglas adicionales de privacidad.
- Fuera del alcance funcional del sistema.

---

## Consecuencias

### Positivas

- Modelo de dominio más simple y enfocado.
- Menor complejidad técnica.
- Implementación más rápida.
- Consultas más eficientes.

### Negativas

- No es posible consultar información individual de vehículos o personas.
- Si el dominio evoluciona, podría requerir refactorización.

---

## Notas de evolución

En caso de que el sistema requiera en el futuro:

- Detalle individual de vehículos
- Tipología de víctimas
- Relación persona–vehículo

El modelo puede evolucionar incorporando nuevas entidades sin romper
la compatibilidad del sistema actual.
