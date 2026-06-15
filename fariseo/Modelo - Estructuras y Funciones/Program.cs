using System;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;

    public struct Docente
    {
        public string Nombre, Apellido;
        public long DNI;
    }
class Program
{
    static int Dividir(int a, int b) => b==0 ? -1 :a / b;
    static void Main()
    {
        Docente NuevoDocente1 = new Docente { Nombre = "Miguel", Apellido = "Ramirez", DNI = 999999 };
        List<Docente> ListaDocentes = new List<Docente> { NuevoDocente1 };
        Console.WriteLine($"La resta de 2 - 3 es:" + Dividir(2, 3));
        Docente NuevoDocente2 = new Docente { Nombre = "Gonzalo", Apellido = "Cao", DNI = 777777 };
        ListaDocentes.Add(NuevoDocente2);
        Docente NuevoDocente3 = new Docente { Nombre = "Ponce", Apellido = "Fasito", DNI = 888888 };
        ListaDocentes.Add(NuevoDocente3);
        foreach (var Docente in ListaDocentes)
            Console.WriteLine($"{Docente.Nombre} - {Docente.Apellido} - {Docente.DNI}");
        Dividir(2,0);
    }
}