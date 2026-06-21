<!DOCTYPE html>
<html>

<head>

<title>Pedido de aberturas</title>

</head>


<body>


<h2>Pedido de aberturas</h2>


<form method="POST">


Cantidad de aberturas:

<input type="text" name="cantidad">


<br><br>


Tipo de abertura:


<select name="tipo">


<?php


$aberturas = array(

    "puerta corrediza"=>50000,
    "ventana"=>30000

);



foreach($aberturas as $nombre=>$precio){

    echo "<option value='$nombre'>$nombre</option>";

}



?>


</select>



<br><br>


Tamaño:


<select name="tamano">


<?php


$tamanos = array(

    "200x200"=>0.20,
    "150x200"=>0,
    "150x60"=>0.05,
    "40x30"=>-0.07,
    "150x110"=>0.15

);



foreach($tamanos as $medida=>$porcentaje){

    echo "<option value='$medida'>$medida</option>";

}



?>


</select>



<br><br>



¿Tiene envío?


<input type="checkbox" name="envio" value="si">



<br><br>



<input type="submit" name="calcular" value="Calcular">



</form>




<?php



// función para calcular tamaño

function calcularTamano($precio,$tamano,$tamanos){


    $porcentaje=$tamanos[$tamano];


    if($porcentaje > 0){


        $precio = $precio + ($precio * $porcentaje);


    }


    else if($porcentaje < 0){


        $precio = $precio + ($precio * $porcentaje);


    }



    return $precio;


}




// función para agregar envío

function agregarEnvio($total,$envio){


    if($envio==true){


        $total=$total+5000;


    }


    return $total;


}




if(isset($_POST["calcular"])){




$cantidad=$_POST["cantidad"];

$tipo=$_POST["tipo"];

$tamano=$_POST["tamano"];





// validar cantidad


if(!is_numeric($cantidad)){


echo "La cantidad debe ser numérica";

exit;


}



if($cantidad < 1 || $cantidad > 100){


echo "La cantidad debe estar entre 1 y 100";

exit;


}






$precio=$aberturas[$tipo];





// calcular precio por tamaño


$precioFinal=calcularTamano(
    $precio,
    $tamano,
    $tamanos
);





// cantidad de aberturas


$total=$precioFinal*$cantidad;





// checkbox envío


if(isset($_POST["envio"])){

    $tieneEnvio=true;

}

else{

    $tieneEnvio=false;

}





$total=agregarEnvio($total,$tieneEnvio);





echo "<h3>";

echo "Tipo: ".$tipo."<br>";

echo "Tamaño: ".$tamano."<br>";

echo "Cantidad: ".$cantidad."<br>";

echo "Total: $".$total;


echo "</h3>";



}



?>


</body>

</html>