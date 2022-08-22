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

- Endpoint "coupon" es el encargado de retornar los item y el valor que se puede usar al momento de recibir un cupon.

https://maikoldiaz.com/api/coupon/

## Stack Tecnológico Solución
 
- .NET Core 6.0 (Framework desarrollo)
- Sql server (Dase de datos)
- Azure (Infraestructura nube para despliegue del servicio y base de datos)
- MSTest (Pruebas Unitarias)


- Se usa Arquitectura Limpia para construir la aplicación, con el objetivo de desacoplar los componentes, y que la lógica de negocio no dependa del framework y del motor de la base de datos.
- Se trato de seguir los lineamientos de los principios SOLID y clean code.
 
