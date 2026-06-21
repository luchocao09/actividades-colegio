<!DOCTYPE html>
<html>
<head>
    <title>Simulador Propiedad</title>

    <style>
        body{
            font-family: Arial;
        }

        .formulario{
            width: 500px;
            background: #aaaaf5;
            border: 3px solid black;
            padding: 15px;
        }

        .resultado{
            margin-top:20px;
            font-size:22px;
            font-weight:bold;
            padding:10px;
        }

        .aprobado{
            background:#3048ff;
            color:white;
        }

        .desaprobado{
            background:red;
            color:white;
        }

    </style>
</head>

<body>


<div class="formulario">

<form method="POST">


Nombre del cliente:
<input type="text" name="nombre">
<br><br>


Tipo de transacción:

<input type="radio" name="transaccion" value="Venta" checked>
Venta

<input type="radio" name="transaccion" value="Alquiler">
Alquiler

<br><br>


Tipo de propiedad:

<select name="propiedad">
<option>Casa</option>
<option>Departamento</option>
<option>Duplex</option>
</select>


<br><br>


Cantidad de habitaciones:

<select name="habitaciones">
<option>1</option>
<option>2</option>
<option>3</option>
<option>4</option>
</select>


<br><br>


Sueldo actual del cliente:

<input type="text" name="sueldo">


<br><br>


<input type="submit" name="calcular" value="Calcular">


</form>

</div>


<?php


if(isset($_POST["calcular"])){

    $nombre = $_POST["nombre"];
    $transaccion = $_POST["transaccion"];
    $propiedad = $_POST["propiedad"];
    $habitaciones = $_POST["habitaciones"];
    $sueldo = $_POST["sueldo"];



    // Validar nombre vacío
    if(empty($nombre)){

        echo "<div class='resultado desaprobado'>
        El nombre no puede estar vacío
        </div>";

        exit;
    }


    // Validar números en nombre
    if(preg_match('/[0-9]/', $nombre)){

        echo "<div class='resultado desaprobado'>
        El nombre no puede contener números
        </div>";

        exit;
    }



    // Validar sueldo letras
    if(!is_numeric($sueldo)){

        echo "<div class='resultado desaprobado'>
        El sueldo debe ser numérico
        </div>";

        exit;
    }



    // Validar sueldo mínimo 5 dígitos

    if(strlen($sueldo) < 5){

        echo "<div class='resultado desaprobado'>
        El sueldo no puede ser menor a 5 dígitos
        </div>";

        exit;
    }



    echo "<h3>
    $nombre desea realizar una $transaccion de una $propiedad de $habitaciones ambiente(s).
    </h3>";



    // Evaluación del sueldo


    if($sueldo > 18000 && $sueldo <= 60000){

        echo "
        <div class='resultado aprobado'>
        Aprobado: renovar contrato cada 2 años
        </div>";

    }


    else if($sueldo > 60000){


        echo "
        <div class='resultado aprobado'>
        Aprobado: renovar contrato cada 3 años
        </div>";

    }


    else{


        echo "
        <div class='resultado desaprobado'>
        Desaprobado
        </div>";

    }

b
}


?>


</body>
</html>