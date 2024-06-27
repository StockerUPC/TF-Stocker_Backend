 
feature: Historial de transaccion 
Scenario: Usuario quiere ver historial de transacciones de su empresa
Given el usuario selecciona la categoria de operaciones 
when El usario seleccona en ver el Historial
and selecciona la fecha 
then se mostrara todo el registro de esa fecha 
