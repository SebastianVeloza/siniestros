# ADR-007: Autenticación mediante JWT

## Estado

Aceptado

## Contexto

Era necesario proteger ciertos endpoints y permitir el control de sesión
de los usuarios.

## Decisión

Se implementó autenticación basada en JWT utilizando:

- Access Token
- Refresh Token
- Validación de Issuer y Audience

Los tokens se almacenan en cookies HTTP para facilitar la integración
con aplicaciones frontend.

## Consecuencias

- Seguridad sin estado (stateless).
- Soporte para logout real mediante invalidación del refresh token.
- Integración sencilla con clientes web.
