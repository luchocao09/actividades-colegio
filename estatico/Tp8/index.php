<!DOCTYPE html>
<html>

<head>
<title>Reserva Hotel</title>
</head>


<body>


<h2>Reserva de Hotel</h2>


<form method="POST">


Tipo de habitación:

<select name="habitacion">

<option value="standard">Standard</option>

<option value="deluxe">Deluxe</option>

<option value="suite">Suite</option>

</select>


<br><br>


Cantidad de días:

<input type="number" name="dias">


<br><br>


Destino:

<select name="destino">

<option value="bariloche">Bariloche</option>

<option value="jujuy">Jujuy</option>

</select>


<br><br>


All inclusive:

<input type="checkbox" name="all" value="si">


<br><br>


<input type="submit" name="calcular" value="Calcular">


</form>



<?php



// función para validar días

function validarDias($dias){

    if($dias < 1 || $dias > 30){

        return false;

    }

    return true;

}



// función para calcular precio

function calcularReserva($habitacion,$dias,$all,$precios){


    $total = $precios[$habitacion] * $dias;



    if($all == true){

        $total = $total + ($total * 0.40);

    }


    return $total;

}




if(isset($_POST["calcular"])){




$habitacion=$_POST["habitacion"];

$dias=$_POST["dias"];

$destino=$_POST["destino"];





// array asociativo

$precios = array(

"standard"=>70000,

"deluxe"=>90000,

"suite"=>110000

);





// validar días


if(!is_numeric($dias)){


echo "Los días deben ser números";

exit;


}


if(!validarDias($dias)){


echo "La cantidad de días debe estar entre 1 y 30";

exit;


}




// checkbox

if(isset($_POST["all"])){

$all=true;

}

else{

$all=false;

}





$total = calcularReserva(
    $habitacion,
    $dias,
    $all,
    $precios
);





echo "<h3>";

echo "Destino: ".$destino."<br>";

echo "Habitación: ".$habitacion."<br>";

echo "Días: ".$dias."<br>";

echo "Total: $".$total;


echo "</h3>";



}



?>



</body>

</html>