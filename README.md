Aplicación de contactos en Angular8 y .netCore

------------------------------
CREAR BASE DE DATOS DE PRUEBA:

* en el archivo appsettings.json, cambie la cadeda de conexión a su base de datos local

* crear una base de datos con el nombre document

* desde la consola correr el comando utilizando DataAccess como proyecto predeterminado

```
PM> Add-Migration "InitialCreate"
```
* luego correr consola Factory. esta creara 500 usuarios de prueba con distintas relaciones

* iniciar proyecto web. este instalara los paquetes de angular asi que demorara un poco en la primera ejecución


------------------------------
CREAR UN USUARIO:

* diríjase a registrar y ingrese los datos solicitados
* la cedula y el celular deben tener máximo 10 caracteres
y el password mas de 5

* luego ingrese correo y contraseña e inicie sesión
* en la parte inferior derecha encontrara un botón con un mas dele click y lo diseccionara a una vista para que busques un contacto
* luego selecciona el tipo de relación que tienes con el y dale agregar a contactos
* por ultimo puede editar el tipo de relación dando click sobre tu contacto y cambiar su tipo de relación.




