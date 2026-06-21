<!DOCTYPE html>
<html>

<head>

<title>Simulador de préstamo</title>

</head>


<body>


<h2>Simulador de préstamo</h2>


<form method="POST">


Cargo:

<select name="cargo">


<?php

$miarray_assoc=array(
    'operario'=>20000,
    'cadete'=>25000,
    'gerente'=>60000,
    'jefe'=>45000
);


// recorrer array en el select

foreach($miarray_assoc as $cargo=>$sueldo){

    echo "<option value='$cargo'>$cargo</option>";

}


?>


</select>


<br><br>


Monto pedido:

<input type="number" name="monto">


<br><br>


Cantidad de cuotas:


<select name="cuotas">

<option value="12">12</option>

<option value="24">24</option>

<option value="36">36</option>

<option value="48">48</option>

<option value="84">84</option>

</select>


<br><br>


<input type="submit" name="calcular" value="Calcular">


</form>



<?php



// función para calcular préstamo

function calcularCuota($sueldo,$monto,$cuotas){



    // interés según sueldo


    if($sueldo > 30000){

        $interes = $monto * 0.10;

    }

    else{

        $interes = $monto * 0.20;

    }



    // si tiene 36 cuotas o más

    if($cuotas >= 36){

        $interes = $interes + ($monto * 0.10);

    }



    // sumar interés al monto

    $total = $monto + $interes;



    // calcular cuota

    $valorCuota = $total / $cuotas;



    return $valorCuota;

}





if(isset($_POST["calcular"])){




$cargo=$_POST["cargo"];

$monto=$_POST["monto"];

$cuotas=$_POST["cuotas"];





// buscar sueldo del cargo


$sueldo=$miarray_assoc[$cargo];





if(!is_numeric($monto)){


echo "El monto debe ser numérico";

exit;


}





$resultado = calcularCuota(
    $sueldo,
    $monto,
    $cuotas
);





echo "<h3>";

echo "Cargo: ".$cargo."<br>";

echo "Sueldo: $".$sueldo."<br>";

echo "Monto pedido: $".$monto."<br>";

echo "Cuotas: ".$cuotas."<br>";

echo "Valor de cada cuota: $".round($resultado,2);


echo "</h3>";



}




?>



</body>

</html>