# NubimetricsTest
Examen para Nubimetrics

## Configuracion BD

Pasos para configurar la Base de datos:

1. Agregar el ConnectionString de la BD donde quieres realizar las pruebas en `appsettings.json` del proyecto `API`
2. Abrir en Visual Studio Code el terminal `Package Manager Console` y marcar como proyecto por defecto `API.Models`.
3. Agregar el siguiente comando `Update-Database` y ejecutarlo en la consola.
4. Ejecutar el endpoint de POST para crear un usuario y validar que la aplicación vaya bien.

## Patrones de diseños utilizados

- [Repository](/README.md): Se usó este patron de diseño para la conexión con la base de datos, de esta manera, si se quiere cambiar a otro ORM, se modificar la clase que la implementa.
- [Strategy](/README.md): Solamente se usó en PaisesController para realizar 2 tareas dependiendo del caso, si solicitaba informacion del pais Argentina, o de otro.
- [DependencyInjection](/README.md): Basicamente se usó en todo el ejercicio, para separar las tareas de cada clase y no depender unas de otras.
