<!DOCTYPE html>
<html>

<head>
<title>Ejercicios PHP</title>
</head>


<body>


<h2>Simulador de envío de paquete</h2>


<form method="POST">


Dirección:

<input type="text" name="direccion">


<br><br>


¿Es expreso?

<input type="checkbox" name="expreso" value="si">


<br><br>


Código postal:

<input type="text" name="codigo">


<br><br>


Descripción del paquete:

<input type="text" name="descripcion">


<br><br>


Tipo de envío:


<select name="envio">

<option value="domicilio">Domicilio</option>

<option value="pick">Punto Pick</option>

<option value="comercio">Sucursal del comercio</option>

</select>


<br><br>


<input type="submit" name="calcular" value="Calcular">


</form>



<?php


if(isset($_POST["calcular"])){


$direccion=$_POST["direccion"];
$codigo=$_POST["codigo"];
$descripcion=$_POST["descripcion"];
$envio=$_POST["envio"];


// validar dirección

if(empty($direccion)){

echo "Debe ingresar dirección";

exit;

}



// validar código postal

if(!is_numeric($codigo)){

echo "El código postal debe ser numérico";

exit;

}


if(strlen($codigo)!=4){

echo "El código postal debe tener 4 dígitos";

exit;

}



// validar descripción

if(preg_match('/[0-9]/',$descripcion)){


echo "La descripción no puede contener números";

exit;


}



// precio del envío

if($envio=="domicilio"){

$valor=2000;

}

else if($envio=="pick"){

$valor=3000;

}

else{

$valor=2500;

}




// expreso

if(isset($_POST["expreso"])){

$valor=$valor + ($valor*0.15);

$mensaje="Envío expreso";

$color="red";


}

else{


$mensaje="Envío normal";

$color="purple";


}





echo "

<h3 style='background:$color;color:white;padding:10px'>

$mensaje <br><br>

Dirección: $direccion <br>

Código postal: $codigo <br>

Descripción: $descripcion <br>

Total: $$valor

</h3>

";


}



?>



</body>

</html>