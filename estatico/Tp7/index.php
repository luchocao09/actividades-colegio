<!DOCTYPE html>
<html>

<head>

<title>Cotizador de Crucero</title>

</head>


<body>


<h2>Cotizador de Crucero</h2>


<form method="POST">


Destino del crucero:

<select name="destino">

<option value="caribe">Caribe</option>

<option value="mediterraneo">Mediterráneo</option>

<option value="fiordos">Fiordos Noruegos</option>

</select>


<br><br>


Cantidad de pasajeros:

<input type="number" name="pasajeros">


<br><br>


Tipo de camarote:

<select name="camarote">

<option value="interior">Interior</option>

<option value="exterior">Exterior</option>

<option value="suite">Suite</option>

</select>


<br><br>


<input type="submit" name="calcular" value="Calcular">


</form>



<?php


if(isset($_POST["calcular"])){


$destino=$_POST["destino"];

$pasajeros=$_POST["pasajeros"];

$camarote=$_POST["camarote"];




// Validar camarote

if($camarote!="interior" && $camarote!="exterior" && $camarote!="suite"){


echo "Tipo de camarote inválido";

exit;


}




// Validar pasajeros


if(!is_numeric($pasajeros)){


echo "La cantidad de pasajeros debe ser numérica";

exit;


}



if($pasajeros < 1 || $pasajeros > 6){


echo "La cantidad de pasajeros debe estar entre 1 y 6";

exit;


}





// valor según destino


if($destino=="caribe"){


$valorBase=1100000;


}


else if($destino=="mediterraneo"){


$valorBase=1300000;


}


else if($destino=="fiordos"){


$valorBase=1500000;


}





// calcular total


$total=$valorBase*$pasajeros;



// descuento


if($pasajeros >= 3){


$descuento=$total*0.07;


$total=$total-$descuento;


echo "Se aplicó descuento del 7% <br>";


}




echo "<br>";

echo "Destino: ".$destino."<br>";

echo "Camarote: ".$camarote."<br>";

echo "Pasajeros: ".$pasajeros."<br>";

echo "Valor base: $".$valorBase."<br>";

echo "Total de la cotización: $".$total;



}



?>


</body>

</html>