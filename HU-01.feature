feature:Compartir informacion
Scenario: Usuario comparte informe con otros usuarios
Given el usuario se ubica en la parte de los registros de informes 
And el usaurio hace click en el informe deseado
when le hace click a los tres punto del lado izquirdo 
And selecciona la opcion compartir 
then el usaurio selecciona los usarios con los que desea compartir 
and lo envia 
 