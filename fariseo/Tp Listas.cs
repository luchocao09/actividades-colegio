List<string> compras = new List<string>();
do
{
    Console.Clear();
    string producto = "";
    do
    {
        Console.WriteLine("Ingrese un producto [fin para cerrar]: ");
        producto = (Console.ReadLine() ?? string.Empty).ToLower();
        if (producto != "fin" && producto != "")
            compras.Add(producto);
    }
    while (producto != "fin");
}
while (compras.Count < 1);

Console.Clear();
Console.WriteLine("Esta es su lista: ");
foreach (var i in compras)
{
    Console.WriteLine(i);
}


Console.WriteLine("Ingrese un elemento a borrar");
string elem_borrar = (Console.ReadLine() ?? string.Empty).ToLower();
if (compras.Remove(elem_borrar))
{
    compras.Remove(elem_borrar);
    Console.Clear();
    if (compras.Count == 0)
    {
        Console.WriteLine("No quedan productos en la lista.");
    }
    else
    {
        Console.WriteLine($"Borro {elem_borrar} y ahora su lista es: ");
        foreach (var i in compras)
        {
            if (i != elem_borrar)
                Console.WriteLine(i);
        }
    }
}
else
{
    Console.WriteLine($"{elem_borrar} no estaba en la lista.");
}
Console.ReadKey();
Console.WriteLine("Hecho por Fernando Franco 6to 3ra");