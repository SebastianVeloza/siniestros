# ADR-002: Uso de catálogos para datos de referencia

## Estado

Aceptado

## Contexto

El sistema requiere registrar y consultar siniestros viales filtrando por:

- Departamento
- Ciudad
- Tipo de siniestro

Estos valores corresponden a **datos de referencia estables**, utilizados
frecuentemente en filtros y reportes, y compartidos por múltiples registros
de siniestros.

Se evaluó la forma de modelar estos datos dentro del sistema.

---

## Decisión

Se decidió modelar departamentos, ciudades y tipos de siniestro como
**catálogos independientes**, relacionados mediante claves foráneas al
siniestro.

Estos catálogos son gestionados como entidades de solo lectura en la mayoría
de los casos.

---

## Justificación

- Evita duplicación de información textual.
- Garantiza consistencia de los datos.
- Facilita filtros eficientes en consultas.
- Permite validación referencial.
- Reduce errores de escritura (typos).
- Mejora la normalización del modelo relacional.

Desde el enfoque de **DDD**, los catálogos representan **bounded contexts
estables** sin lógica de negocio compleja, ideales para ser tratados como
datos de referencia.

---

## Alternativas consideradas

### Uso de strings directamente en el siniestro

- Duplicación de valores.
- Inconsistencias en nombres.
- Filtros menos eficientes.
- Dificulta mantenimiento y reportes.

### Uso de enums

- Poco flexible ante cambios.
- Requiere despliegue para modificar valores.
- No adecuado para catálogos extensos (ciudades).

---

## Consecuencias

### Positivas

- Datos consistentes y normalizados.
- Consultas más eficientes.
- Facilidad para reportes y estadísticas.
- Posibilidad de reutilizar catálogos en otros sistemas.

### Negativas

- Mayor número de tablas.
- Requiere carga inicial de datos (seed).

---

## Notas adicionales

Los catálogos pueden cargarse automáticamente mediante scripts SQL o procesos
de inicialización al levantar la infraestructura, garantizando su
disponibilidad desde el primer uso del sistema.
