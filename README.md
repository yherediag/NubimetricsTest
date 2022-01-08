# NubimetricsTest
Examen para Nubimetrics

# Configuracion BD
Para poder ejecutar el Endpoint de Usuarios, es necesario crear una Base de datos y ejecutar el script que se encuentra en la carpeta "Scripts" llamado "CreateTable_Usuarios.sql".
Luego, se debe agregar la cadena de conexión de la base de datos en el archivo "appsettings.json", exactamente en la seccion "ConnectionStrings.NubimetricsDatabase".
De ultimo, ejecutar el endpoint de POST para crear un usuario y validar que la aplicación vaya bien.
