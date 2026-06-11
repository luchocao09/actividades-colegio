using System;
using System.Formats.Tar;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
class N2
{   
    static void Main()
    {
        Console.Write("Ingrese su nombre: ");
        string nombre = (Console.ReadLine() ?? "").Trim();
        Saludar(nombre);
        Console.ReadKey();
    }
    static void Saludar(string nombre)
    {
        if (nombre=="")
        Console.WriteLine("No ingreso ningun nombre.");
        else
        Console.WriteLine($"Hola {nombre}");
        return;
    }

    
}