<?php
    $materiales_precio=array('algarrobo'=>1,
                    'cedro'=>0.8,
                    'pino'=>0.9
);
    $tipo_mueble=array(
        'cama'=>45000,
        'sopa'=>48000,
        'mesa'=>30000,
        'modular'=>70000,
        'ropero'=>65000 
);

?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <form action="" method="post">
    <label for="">Cantidad de muebles</label>  
    <input type="text" name="cant_m" id="">
    <br>
    <label for="">Tipo de muelle</label>
    <select name="tipo_m" id="">
        <?php
        foreach($tipo_mueble as $tipo => $precio)
            {
                echo '<option value="'.$precio.'">'.$tipo.'<option>';
            }
        ?>
    </select>
    <br>
    <label for="">Material</label> 
    <select name="material" id="">
        <?php
        foreach($materiales_precio as $material => $porcentaje)
        {
            echo '<option value="'.$porcentaje.'">'.$material.'<option>';
        }
        ?>
    </select>
    <br>
    <input type="submit" value="Calcular">
    </form>
</body>
</html>

<?php
if($_POST)
$errores="";
if(!is_numeric($_POST['cant_m']))
$errores.="La cantidad solo puede ser numerica.<br>";
if($_POST['cant_m']<=0 || $_POST['cant_m']>30)
$errores.="La cantidad de muebles tiene que estar en entre 1-30.<br>";
$cant_m=$_POST['cant_m'];
$tipo_m=$_POST['tipo_m'];
$material=$_POST['material'];
$total_compra=$tipo_m*$material;
if($_POST['cant_m']>10) $total_compra*=0.9;
else if($_POST['cant_m']<5)$total_compra*=0.85;

if($errores=="")
echo $total_compra;
else
echo $errores;
?>