using System;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;

class Program
{
    public struct Cliente
    {
        public int numeroCliente;
        public string nombre,apellido;
        public int celular;
    }
    static void Main()
    {
        
        string opcion;
        int NC=0;
        List<Cliente> ListaClientes = new List<Cliente>();
        do
        {
            Console.Clear();
            Console.WriteLine("############################");
            Console.WriteLine(" SISTEMA GESTOR DE CLIENTES");
            Console.WriteLine("############################");
            Console.Write("1. Agregar cliente nuevo\n2. Mostrar clientes actuales\n3. Buscar cliente por apellido\n4. Borrar cliente\nElija una opcion: ");
            opcion = (Console.ReadLine() ?? "").Trim();
            switch (opcion)
            {
                case "1":
                    Console.Clear();
                    Cliente Cliente = new Cliente();
                    Console.Write("Ingrese su nombre: ");
                    Cliente.nombre = (Console.ReadLine() ?? "").ToUpper().Trim();
                    Console.Write("Ingrese su apellido: ");
                    Cliente.apellido = (Console.ReadLine() ?? "").ToUpper().Trim();
                    Console.Write("Ingrese su celular: ");
                    if(int.TryParse((Console.ReadLine() ?? "0").Trim(),out Cliente.celular))
                    NC++;
                    AgregarCliente(Cliente, ListaClientes,NC);
                    
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    MostrarClientes(ListaClientes);
                    Console.ReadKey();
                    break;

                case "3":
                    Console.Clear();
                    Console.Write("Ingrese su apellido: ");
                    string apellidoCliente = (Console.ReadLine() ?? "").ToUpper().Trim();
                    BuscarApellido(apellidoCliente,ListaClientes);
                    Console.ReadKey();
                    break;

                case "4":
                    int NumeroBorrar;
                    Console.Clear();
                    Console.Write("Ingrese el numero de cliente del cliente que quiere borrar: ");
                    if(int.TryParse((Console.ReadLine() ?? "0").Trim(),out NumeroBorrar)){}
                    BorrarCliente(NumeroBorrar,ListaClientes);
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
        }
        while (opcion != "5");
    }

    static List<Cliente> AgregarCliente(Cliente NuevoCliente, List<Cliente> ListaClientes,int NumeroCliente)
    {
        if (NuevoCliente.nombre=="" || NuevoCliente.apellido=="" || NuevoCliente.celular==0)
        Console.WriteLine("Los campos estan incompletos");
        else
        {
        NuevoCliente.numeroCliente=NumeroCliente;
        ListaClientes.Add(NuevoCliente);
        }
        return ListaClientes;
    }
    static void MostrarClientes(List<Cliente> ListaClientes)
    {
        if (ListaClientes.Count == 0)
            Console.WriteLine("No tiene clientes.");
        else
        {
            Console.WriteLine($"#######################");
            Console.WriteLine($"Sus clientes actuales son:");
            foreach (var Cliente in ListaClientes)
            Console.WriteLine($"NC:{Cliente.numeroCliente} - {Cliente.nombre} {Cliente.apellido} - {Cliente.celular}");
            Console.WriteLine($"#######################");
        }
        return;
    }
    static void BuscarApellido(string ApellidoCliente, List<Cliente> ListaClientes)
    {
        if (ApellidoCliente=="")
        Console.WriteLine("No ingreso un apellido.");
        else
        {
        int contador=0;
        List <int> IndicesApellido = new List<int>();
        foreach (var Cliente in ListaClientes)
        {
            if (Cliente.apellido == ApellidoCliente) 
            IndicesApellido.Add(contador);
            contador++;
        }  
        if (IndicesApellido.Count>0)
        {
        Console.WriteLine($"Los clientes con el apellido: {ApellidoCliente} son:");
        foreach (var indice in IndicesApellido)
        Console.WriteLine($"NC: {ListaClientes[indice].numeroCliente} - {ListaClientes[indice].nombre} {ListaClientes[indice].apellido} - {ListaClientes[indice].celular}");
        }
        else
        Console.WriteLine($"Ningun cliente tiene el apellido: {ApellidoCliente}.");
        }
        return;
    }
    static List<Cliente> BorrarCliente(int NumeroCliente, List<Cliente> ListaClientes)
    {
        if (NumeroCliente<=0)
            Console.WriteLine($"No ingreso un numero de cliente valido.");
        else
        {
        foreach (var Cliente in ListaClientes)
        {
            if (NumeroCliente==Cliente.numeroCliente){
            ListaClientes.Remove(Cliente);
            return ListaClientes;
            }
        }
        }
        return ListaClientes;

    }

}