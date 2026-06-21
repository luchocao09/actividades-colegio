<!DOCTYPE html>
<html>

<head>

<title>Corralón</title>

</head>


<body>


<h2>Compra de materiales</h2>


<form method="POST">


Cantidad de materiales pedidos:

<input type="text" name="cantidad">


<br><br>


Tipo de material:


<select name="material">


<?php


$materiales = array(

    "arena"=>6000,
    "cal"=>6500,
    "ceresita"=>2500,
    "cemento"=>5500

);


foreach($materiales as $nombre=>$precio){

    echo "<option value='$nombre'>$nombre</option>";

}


?>


</select>


<br><br>


Financiación cuotas:


<select name="cuotas">

<option value="1">1 cuota</option>

<option value="6">6 cuotas</option>

<option value="12">12 cuotas</option>

<option value="24">24 cuotas</option>


</select>


<br><br>


<input type="submit" name="calcular" value="Calcular">


</form>




<?php



// función para calcular compra

function calcularCompra($cantidad,$precio,$cuotas){



    $total = $cantidad * $precio;



    // intereses según cuotas


    if($cuotas == 12){


        $interes = $total * 0.10;


    }

    else if($cuotas == 24){


        $interes = $total * 0.15;


    }

    else{


        $interes = 0;


    }



    $totalFinal = $total + $interes;



    $valorCuota = $totalFinal / $cuotas;



    return array($totalFinal,$valorCuota);



}





if(isset($_POST["calcular"])){



$cantidad=$_POST["cantidad"];

$material=$_POST["material"];

$cuotas=$_POST["cuotas"];





// validar cantidad numérica


if(!is_numeric($cantidad)){


echo "La cantidad debe ser numérica";

exit;


}





// validar cantidad entre 0 y 100


if($cantidad < 0 || $cantidad > 100){


echo "La cantidad debe estar entre 0 y 100";

exit;


}





$precio=$materiales[$material];





$resultado = calcularCompra(
    $cantidad,
    $precio,
    $cuotas
);



$total=$resultado[0];

$cuota=$resultado[1];





echo "<h3>";

echo "Material: ".$material."<br>";

echo "Cantidad: ".$cantidad."<br>";

echo "Cuotas: ".$cuotas."<br>";

echo "Total de compra: $". $total ."<br>";

echo "Valor de cada cuota: $".round($cuota,2);


echo "</h3>";



}



?>



</body>

</html>