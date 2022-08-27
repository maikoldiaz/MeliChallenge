# MeliChallenge

**Repositorio (https://github.com/maikoldiaz/MeliChallenge.git)**

## El Reto: Maximiza el uso de un cupon

Mercado Libre está implementando un nuevo beneficio para los usuarios que más usan la plataforma con un cupón de cierto monto gratis que les permitirá comprar tantos ítems marcados como favoritos sea posible, siempre que no excedan el monto del cupón.
Para esto se está analizando construir una API que dado una lista de item_id y el monto total pueda devolver la lista de ítems que maximice el total gastado.

Premisas:
- Sólo se puede comprar una unidad por item_id.
- No hay preferencia en la cantidad total de ítems.
- El precio puede contener hasta 2 decimales.



## Solución Al Problema
**Nivel 1 y Nivel 2**

**Swagger (https://meli-challenge.azurewebsites.net/index.html)**

Endpoint "coupon" es el encargado de retornar los item y el valor que se puede usar al momento de recibir un cupon.

https://meli-challenge.azurewebsites.net/coupon/

```
{
  "item_ids": [
    "MCO928039047",
    "MCO933562900",
    "MCO869185478",
    "MCO894891259",
    "MCO870314784"
  ],
  "amount": 1000000
}
```


Endpoint "product" que tiene habilitado el metodo get, se encarga de retornar los 5 productos mas gustados.

https://meli-challenge.azurewebsites.net/product/


## Stack Tecnológico Solución
 
- .NET Core 6.0 (Framework desarrollo)
- Sql server (Dase de datos)
- Azure (Infraestructura nube para despliegue del servicio y base de datos)
- MSTest (Pruebas Unitarias)


- Se usa Arquitectura Limpia para construir la aplicación, con el objetivo de desacoplar los componentes, y que la lógica de negocio no dependa del framework y del motor de la base de datos.
- Se trato de seguir los lineamientos de los principios SOLID y clean code.

## Instrucciones para levantar la app

1. Obtener el codigo fuente.
```
git clone https://github.com/maikoldiaz/MeliChallenge.git
```
2. Posicionarse sobre la carpeta Develop, donde se encuentra el código fuente del proyecto.
```
cd MeliChallenge/Develop/
```
3. Construir la imagen de Docker.
```
docker build . -f .dockerfile -t melichallenge --no-cache
```
4. Levantar el contenedor con la imagen de Docker previamente construido.

```
docker run -d -p 8080:80 melichallenge
```

5. Abrir el navegador de su preferencia y en la barra superior pegar este link: [melicahllenge](http://localhost:8080)
 
