using System;
using System.Formats.Tar;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
class N3
{   
    static void Main()
    {
        string opcion;
        float numero_1,numero_2;
        do
        {
        Console.Clear();
        Console.WriteLine("###############");
        Console.WriteLine("  CALCULADORA");
        Console.WriteLine("###############");
        Console.Write("1. Suma\n2. Resta\n3. Multiplicacion\n4. Division\n5. Salir\nElija una operacion: ");
        opcion = (Console.ReadLine() ?? "").Trim();
        Console.Write("Ingrese un numero: ");
        if(float.TryParse(Console.ReadLine() ?? "",out numero_1)){}
        Console.Write("Ingrese un segundo numero: ");
        if(float.TryParse(Console.ReadLine() ?? "",out numero_2)){}
        switch (opcion)
        {
            case "1":
            Console.Clear();
            Console.WriteLine($"La suma de {numero_1} + {numero_2} es: {Sumar(numero_1,numero_2)}");
            Console.ReadKey();
            break;
            case "2":
            Console.Clear();
            Console.WriteLine($"La resta de {numero_1} - {numero_2} es: {Restar(numero_1,numero_2)}");
            Console.ReadKey();
            break;
            case "3":
            Console.Clear();
            Console.WriteLine($"La multiplicacion de {numero_1} x {numero_2} es: {Multiplicar(numero_1,numero_2)}");
            Console.ReadKey();
            break;
            case "4":
            Console.Clear();
            Console.WriteLine($"La division de {numero_1} / {numero_2} es: {Division(numero_1,numero_2)}");
            Console.ReadKey();
            break;
            default:
            Console.Clear();
            if (opcion=="")
            Console.WriteLine($"No ingreso nada");
            else
            Console.WriteLine($"{opcion} no es una opcion valida.");
            Console.ReadKey();
            break;
        }
        } while (opcion!="5");
    }
    static float Sumar(float a, float b)
    {
    return a+b; 
    }
    static float Restar(float a, float b)
    {
    return a-b; 
    }
    static float Multiplicar(float a, float b)
    {
    return a*b; 
    }
    static float Division(float a, float b)
    {
        if (b==0)
        {
        Console.WriteLine("No existe la division por 0.");
        return b;
        }
        return a/b; 
    }
}