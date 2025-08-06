# Get Sales Result API

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

## Estructura

get-sales-result/  
- Application/  
  - DTOs/  
  - Interfaces/  
  - Services/  
- Domain/  
  - Entities/  
  - Enums/  
- Infra/  
  - Repositories/  
- WebApi/  
  - Controllers/  
  - Middlewares/  
- get-sales-result-test/  

## Endpoints

1. Insertar venta  
- POST /api/sales  
- Body ejemplo:  
  {  
    "vehicleType": "Sport",  
    "distributionCenter": "Center1"  
  }  
- Calcula el precio según tipo de vehículo.  
- Si es Sport, agrega 7% de impuesto adicional.  
- Guarda la venta en repositorio en memoria.  

2. Obtener volumen total de ventas  
- GET /api/sales/total-volume  
- Retorna suma total en USD de todas las ventas.  

3. Obtener volumen de ventas por centro  
- GET /api/sales/volume-by-center  
- Retorna diccionario con centro y total vendido en USD.  

4. Obtener porcentaje de ventas por modelo y centro  
- GET /api/sales/percentages-by-model-and-center  
- Retorna objeto con cada centro y subdiccionario de modelos y porcentajes.  
