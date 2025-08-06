# Get Sales Result API

---

## Tecnologias

- .NET Core 8.0
- ASP.NET Core Web API
- xUnit
- Inyeccion de dependencias
- Logger integrado
- Stopwatch para medir tiempo de ejecucion de metodos
- Arquitectura en capas (Domain, Application, Infra, WebApi)
- Patron Repository (mockeado en memoria)
- Swagger

---

## Estructura

get-sales-result/
├── Application/
│ ├── DTOs/ → Objetos de transferencia de datos
│ ├── Interfaces/ → Interfaces de servicios
│ └── Services/ → Lógica de negocio
├── Domain/
│ ├── Entities/ → Entidades del dominio
│ └── Enums/ → Enumeradores (tipo de vehículo, centro)
├── Infra/
│ └── Repositories/ → Implementación mock (InMemory)
├── WebApi/
│ ├── Controllers/ → Controladores REST
│ └── Middlewares/ → Middleware de logging y errores globales
├── get-sales-result-test/ → Proyecto de pruebas unitarias (xUnit)


---

### Insertar venta

**POST** `/api/sales`

```json
{
  "vehicleType": "Sport",
  "distributionCenter": "Center1"
}

    Calcula el precio según tipo

    Si es Sport, agrega un 7% extra

    Guarda la venta en memoria

2. Obtener volumen total de ventas

GET /api/sales/total-volume

    Retorna la suma total en USD de todas las ventas

3. Obtener volumen de ventas por centro

GET /api/sales/volume-by-center

    Retorna un diccionario con el volumen total vendido por cada centro

4. Obtener porcentaje de ventas por modelo y centro

GET /api/sales/percentages-by-model-and-center

    Devuelve un objeto con el porcentaje que representa cada modelo en cada centro respecto al total global
