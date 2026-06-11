using System;
using System.ComponentModel;
using System.Formats.Tar;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Timers;
class N4
{   
    static void Main()
    {
        float Numero;
        List<float> ListaNumeros = new List<float>();
        do
        {
        Console.Write("Ingrese un numero [0 para PARAR]: ");
        if(float.TryParse(Console.ReadLine() ?? "",out Numero))
        ListaNumeros.Add(Numero);
        } while (Numero!=0);
        EsPar(ListaNumeros);
        EsPositivo(ListaNumeros);
        Mayor(ListaNumeros);
    }
    static void EsPar(List<float> Numeros)
    {
    if (Numeros.Count()<0)
    Console.WriteLine("No ingreso ningun numero.");    
    else
    {
    foreach(var N in Numeros)
    {
    if(N%2==0)
    Console.WriteLine($"{N} es par");
    else
    Console.WriteLine($"{N} es impar");
    }
    }
    return ;
    }
    static void EsPositivo(List<float> Numeros)
    {
    if (Numeros.Count()<0)
    Console.WriteLine("No ingreso ningun numero.");    
    else
    foreach(var N in Numeros)
    if(N>0)
    Console.WriteLine($"{N} es positivo");
    else
    Console.WriteLine($"{N} es negativo");
    return;
    }
    static void Mayor(List<float> Numeros)
    {   
        if (Numeros.Count()<0)
        Console.WriteLine("No ingreso ningun numero.");    
        else
        {
        float NumeroMayor=Numeros[0];
        foreach(var N in Numeros)
        if(N>NumeroMayor)
        NumeroMayor=N;
        Console.WriteLine($"El numero mayor de la lista es: {NumeroMayor}");
        }
        return;
    }
}