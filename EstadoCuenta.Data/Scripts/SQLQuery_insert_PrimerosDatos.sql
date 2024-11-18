use EstadoCuentaDB

-- Insertar un usuario
INSERT INTO Usuario (Nombre, Apellidos, Telefono, Email, Dui)
VALUES ('Juan', 'P�rez', '12345678', 'jperez@gmail.com', '12345678-9');

-- Insertar una tarjeta
INSERT INTO Tarjeta (NumTarjeta, LimiteCredito, Saldo, SaldoDisponible, IdUsuario)
VALUES ('1234567812345678', 1500.00, 100.00, 1400.00, 1);

-- Insertar los tipos de transacci�n (Compra y Deposito)
INSERT INTO TipoTransaccion (TipoDeTransaccion) VALUES
('Compra'),
('Deposito');

-- Insertar una compra
INSERT INTO Transaccion (Fecha, Monto, Descripcion, SaldoDisponible, IdTipoTransaccion, NumTarjeta)
VALUES (2024-11-12 15:43:39.6770000 , 200.00, 'Compra en tienda A', 1300.00, 1, '1234567812345678');

-- Insertar un dep�sito
INSERT INTO Transaccion (Fecha, Monto, Descripcion, SaldoDisponible, IdTipoTransaccion, NumTarjeta)
VALUES (2024-11-15 18:43:39.6770000 , 100.00, 'Pago de tarjeta', 1400.00, 2, '1234567812345678');