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
o	En en archivo program.cs encontraras comentada las líneas 60-66 descomentalas, luego abre la consola de administrador de paquetes y ejecuta "Add-Migration DbEstado" seguido de "Update-Database"  luego ejecuta el proyecto, y tendrás las tablas ya generadas en la base de datos, después de eso puedes volver a comentar las líneas 60-66.
	


•	Ejecuta la API y el cliente web para explorar el estado de cuenta de tarjetas de crédito. Al iniciar veras una pantalla donde te hara elegir entre crear un nuevo usuario o ver los existentes luego continua el flujo en ver los detalles de las tarjetas.

	Prueba los endpoints con la colección de Postman incluida o directamente al ejecutar el proyecto EstadoCuenta.Api en el Swagger.

	Personaliza configuraciones como tasas de interés y porcentajes en la clase TarjetaVariablesDto.
