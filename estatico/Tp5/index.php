<!DOCTYPE html>
<html>
<head>

<title>TP5</title>

<style>

.formulario{
    width:400px;
    background:#aaaaf5;
    padding:20px;
    border:2px solid black;
}

.resultado{
    margin-top:20px;
    padding:15px;
    font-size:22px;
    font-weight:bold;
}


</style>

</head>


<body>


<div class="formulario">

<form method="POST">


Talle:

<select name="talle">

<option value="S">S</option>
<option value="M">M</option>
<option value="L">L</option>
<option value="XL">XL</option>

</select>


<br><br>


Color:

<br>

<input type="checkbox" name="color" value="rojo">
Rojo

<br>

<input type="checkbox" name="color" value="azul">
Azul

<br>

<input type="checkbox" name="color" value="celeste">
Celeste

<br>

<input type="checkbox" name="color" value="verde">
Verde


<br><br>


Edad del cliente:

<input type="text" name="edad">


<br><br>


Sueldo:

<input type="text" name="sueldo">


<br><br>


<input type="submit" name="calcular" value="Calcular">


</form>


</div>



<?php


if(isset($_POST["calcular"])){


$talle=$_POST["talle"];
$edad=$_POST["edad"];
$sueldo=$_POST["sueldo"];


if(isset($_POST["color"])){

    $color=$_POST["color"];

}else{

    echo "Debe elegir un color";
    exit;

}



// validar edad

if(!is_numeric($edad)){

echo "La edad debe ser un número";
exit;

}


if($edad < 18 || $edad > 85){

echo "Edad fuera de rango";
exit;

}



// validar sueldo

if(!is_numeric($sueldo)){

echo "El sueldo debe ser numérico";
exit;

}



$precio = $sueldo;



// descuento por talle


if($talle=="S" || $talle=="M"){


    $precio = $precio - ($precio * 0.05);


    $mensaje="Tiene 5% de descuento";


}



else if($talle=="L" || $talle=="XL"){


    $precio = $precio + ($precio * 0.10);


    $mensaje="Tiene 10% de aumento";


}




// color del resultado


if($color=="rojo"){

$fondo="red";

}


else if($color=="azul"){

$fondo="blue";

}


else if($color=="verde"){

$fondo="green";

}


else{

$fondo="cyan";

}




echo "

<div class='resultado' style='background:$fondo;color:white'>

Talle: $talle <br>

Color: $color <br>

Precio final: $$precio <br>

$mensaje

</div>

";

}



?>


</body>
</html>