int largo;
Console.Write("Ingrese el largo de su array: ");
while (!(int.TryParse(Console.ReadLine(), out largo)))
{
    Console.WriteLine("Valor invalido.");
    Console.ReadKey();
    Console.Clear();
    Console.Write("Ingrese el largo de su array: ");
}
float[] notas = new float[largo];
float evaluacion;
for (int i = 0; i < largo; i++)
{
    Console.Clear();
    Console.Write("Ingrese una nota: ");
    while (!(float.TryParse(Console.ReadLine(), out evaluacion)))
        { 
            Console.WriteLine("Valor invalido.");
            Console.ReadKey();
            Console.Clear();
            Console.Write("Ingrese una nota: ");
        }
    notas[i] = evaluacion;
}
Console.Clear();
float menor = notas[0];
float mayor = notas[0];
for (int i = 0; i < largo; i++)
    {
        if (notas[i] < menor)
        {
            menor = notas[i];
        }
        if (notas[i] > mayor)
        {
            mayor = notas[i];
        }
        Console.WriteLine($"En la posicion [{i}] = {notas[i]}");
    }
Console.WriteLine($"El numero menor es: {menor}");
Console.WriteLine($"El numero mayor es: {mayor}");