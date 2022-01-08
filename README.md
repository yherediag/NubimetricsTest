# NubimetricsTest
Examen para Nubimetrics

## Configuracion BD

Pasos para configurar la Base de datos:

1. Para poder ejecutar el Endpoint de Usuarios, es necesario crear una Base de datos y ejecutar el siguiente script [Script para creación de usuarios](API/Scripts/CreateTable_Usuarios.sql)
2. Luego, se debe agregar la cadena de conexión de la base de datos en el archivo [AppSettings](API/appsettings.json), exactamente en la seccion "ConnectionStrings.NubimetricsDatabase".
3. De ultimo, ejecutar el endpoint de POST para crear un usuario y validar que la aplicación vaya bien.
