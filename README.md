# 🚦 API REST de Siniestros Viales – .NET 8

Este proyecto implementa una **API REST** en **.NET 8** para el registro y consulta de siniestros viales.  
La solución está diseñada para ser escalable, clara y fácil de integrar con sistemas de análisis o visualización.

---

## 📋 Funcionalidades principales

- 📥 **Registro de siniestros viales** con los siguientes datos:
  - Identificador único del siniestro
  - Fecha y hora del evento
  - Departamento y ciudad
  - Tipo de siniestro
  - Vehículos involucrados
  - Número de víctimas
  - Descripción opcional

- 🔎 **Consulta de siniestros** mediante filtros:
  - Por **departamento**
  - Por **rango de fechas**
  - Combinación de ambos filtros
  - Soporte de **paginación**

---

## 🎯 Objetivo

Facilitar la gestión y análisis de siniestros viales, ofreciendo una API flexible y escalable que pueda integrarse con sistemas de reporte, visualización o análisis estadístico.

---

## ⚙️ Tecnologías utilizadas

- .NET 8  
- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server (o base de datos relacional equivalente)  
- Swagger para documentación interactiva  

---

## 📄 Documentación incluida

- **Modelo de dominio**  
- **ADRs (Architecture Decision Records)**  
- **Registro de tiempos y módulos críticos** en caso de no completar la solución  

---

## 🚀 Instalación y ejecución

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/usuario/siniestros.git
   cd siniestros
   ```

2.Configurar la base de datos en appsettings.json.
3.Ejecutar las migraciones:
4.Levantar el proyecto:
```
dotnet run
```

