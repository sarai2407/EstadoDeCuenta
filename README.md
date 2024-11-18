Estado de Cuenta de Tarjeta de Crédito

Este proyecto es una solución completa que permite gestionar el estado de cuenta de tarjetas de crédito mediante una REST API desarrollada en ASP.NET Core y una interfaz web basada en Razor Pages y MVC. La aplicación interactúa con una base de datos SQL Server mediante procesos almacenados, brindando funcionalidades como la visualización de transacciones, cálculos de cuotas y exportación de datos(PDF y Excel).

	Características Principales:
		•	Visualización del estado de cuenta con detalles de movimientos mensuales.
		•	Cálculo de cuotas mínimas, totales y montos de contado con intereses.
		•	Registro y gestión de compras y pagos mediante formularios interactivos.
		•	Exportación de estados de cuenta en PDF.
		•	Exportación de compras de cuenta en excel.
		•	REST API documentada con Swagger para consumir datos desde el frontend.
		•	Arquitectura robusta que implementa patrones como CQRS, UnitOfWork y manejo global de excepciones.
		•	Tecnologías Utilizadas:
		•	Backend: ASP.NET Core 6 (API REST).
		•	Frontend: Razor Pages, jQuery, y MVC.
		•	Base de Datos: SQL Server con procesos almacenados (PL/SQL).
		•	Herramientas: AutoMapper, FluentValidation, Swagger, Healthcheck.
		•	Modelos de datos: DTO para API y View Model para el MVC

	Cómo Usarlo:

•	Primer paso he importante: 
		o	La base de datos se crea desde el proyecto EstadoCuenta.Api por lo cual en el archivo appsettings.json podrás encontrar la  conexión a la base, puedes modificarle segun tu conesion con SQLSERVER.
		o	En en archivo program.cs encontraras comentada las líneas 60-66 descomentalas, luego abre la consola de administrador de paquetes y ejecuta "Add-Migration DbEstado" seguido de "Update-Database"  luego ejecuta el proyecto, y tendrás las tablas ya 			generadas en la base de datos, después de eso puedes volver a comentar las líneas 60-66.
  		o	En el proyecto EstadoCuenta.Data encontraras una carpeta con Scripts, ahi encontraras un SQLQuery que contiene datos para crear tu  primer usuario, con su tarjeta asociada y transacciones para la misma
    			tambien los tipos de transacciones que en el orden correcto. Tambien estan los Scripts para generar los procedimientos almacenados, los cuales hay q ejecutarlos en la base de datos directamente
	


• Ejecuta la API y el cliente web para explorar el estado de cuenta de tarjetas de crédito. Al iniciar veras una pantalla donde te hara elegir entre crear un nuevo usuario o ver los existentes luego continua el flujo en ver los detalles de las tarjetas.


*  Prueba los endpoints con la colección de Postman incluida(Estas se realizaron con el usuario y tarjeta que estan en el SQLQuery que contiene los primeros datos) o directamente al ejecutar el proyecto EstadoCuenta.Api en el Swagger.
*  Personaliza configuraciones como tasas de interés y porcentajes en la clase TarjetaVariablesDto.



*La solucion esta dividida en 3 proyectos.*

EstadoCuenta.Api : en este vamos a encontrar la REST API, todos los endpoints que interactuan con la base de datos y los procedimientos almacenados, haciendo uso de UnitOfWork, interfaces y repositorios vamos a encontrar un los controladores de: Tarjeta, Transacciones
TipoTransacciones, Usuarios y Pdf, este ultimo contiene tanto el endpoint para descargar pdf como para descargar el excel, los demas controladores tienen procedimeintos que van directamente relacionados con las tablas de la base de datos. Para conectar con los modelos
de la base se hace uso de DTOs, y tambien el Automapper, algunos DTOs estan modificados para mostrar y recibir los datos necesarios, aqui tambien vamos a encontrar las clases de FluentValidation.


EstadoCuenta.Data : En esta se encuentra la migracion de la base de datos, los modelos,  y el DbContext. Para tener una buena conexion con la base de datos.

EstadoCuentaMVC: En este vamos a encontrar los controller que reciben y envian datos de y para los endpoints del proyecto EstadoCuenta.Api por medio de solicitudes Http, los modelos que se mapean para enviar y recibir los datos estan como ViewModels, tambien vamos a encontrar vistas haciendo uso de blazor y Bootstrap, y encontraremos un Middleware que hace uso de las GlobalExceptions.

cada proyecto integra la solucion para que la primera vista sea una bienvenida, que solicita crear usuarios si no ha registrado ninguno o peder ver los ya registrados, si nno pesee usuario lo redirige a crear usuario, luego a crear una tarjeta asociada a ese usuario, y una ves hecho esto puede decidir entre ver la lista de usuarios o ver el detalle de la tarjeta, en el detalle de tarjeta va a encontrar todos los datos relacionados con la tarjeta y el usuario desde el limite de la tarjeta hasta el Pago de Contado con Intereses, ademas de mostrar la lista de compras del mes actual, con la sumatoria de sus montos y la sumatoria de los montos de las compras de los ultimos dos meses, en esta vista es donde se permiten descargar tanto el excel como el pdf y podremos tambien dirigirnos a la vista de crear compra o crear deposito por medio de botones, y si en cambio quieren ver las transacciones tanto depositos como compras lo puede hacer mediante el boton ver transacciones, en esta vista se muestran las transacciones del mes actual pero tambien se muestra la opcion para dirigirse a la vista de ver todas las transacciones de la tarjeta, ambas en forma descendente por mes, y puede volver atras o directamente a la vista de detalle de tarjeta en cualquier momento
