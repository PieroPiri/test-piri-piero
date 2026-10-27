# Parcial 2 - Inscripciones a Talleres

API REST para gestionar las inscripciones a los talleres optativos de Extensión Universitaria de la UCSE.

## Proyectos

- `Talleres.Logica`: entidades, `InscripcionService` con las reglas de negocio y repositorios JSON en `Data/`.
- `Talleres.Api`: Web API ASP.NET Core con Swagger, controllers y DTOs.
- `Talleres.Tests`: tests NUnit del servicio.
- `Frontend`: HTML, CSS y JS que consulta los talleres.

## Levantar la API

```bash
cd Talleres.Api
dotnet run --launch-profile http
```

La API queda en `http://localhost:5270` y Swagger en `http://localhost:5270/swagger`.

Al iniciar se crea `talleres.json` con los 4 talleres precargados. Las inscripciones se guardan en `inscripciones.json` (si no existe, se arranca con la lista vacía).

## Endpoints

| Método | Endpoint | Descripción |
|---|---|---|
| GET | `/api/talleres` | Lista los talleres con sus cupos disponibles |
| GET | `/api/inscripciones?dni=` | Lista las inscripciones, filtradas por DNI si se envía |
| POST | `/api/inscripciones` | Registra una inscripción |
| PUT | `/api/inscripciones/{id}/baja` | Da de baja una inscripción |

Ejemplo de body para el POST:

```json
{ "tallerId": 1, "dni": 40111222, "nombreAlumno": "Juan Pérez" }
```

Códigos de respuesta: `201` alta, `200` baja, `400` con el motivo si se viola una regla, `404` si el taller o la inscripción no existen.

## Abrir el frontend

Con la API corriendo, abrir `Frontend/index.html` en el navegador (o con Live Server de VS Code).

## Ejecutar los tests

```bash
dotnet test
```
