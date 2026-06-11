using System;
using System.Collections.Generic;
namespace AgendaSencilla
{
    class Program
    {
        // 1) Estructura pública
        public struct Contacto
        {
            public string nombre;
            public string apellido;
            public int telefono;
        }
        static void Main()
        {
            List<Contacto> agenda = new List<Contacto>();
            int opcion;
            Console.WriteLine("Bienvenido a la agenda");
            // 2) Menú con do-while
            do
            {
                MostrarMenu();
                opcion = int.Parse(Console.ReadLine());
                Console.Clear();
// 3) Control con switch
            
switch (opcion)
                {
                    // 4) Llamadas a las funciones
                    case 1:
                        AgregarContacto(agenda);
                        break;
                    case 2:
                        BorrarContacto(agenda);
                        break;
                    case 3:
                        BuscarContacto(agenda);
                        break;
                    case 4:
                        MostrarTodos(agenda);
                        break;
                    case 5:
                        Console.WriteLine("Saliendo de la agenda.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            } while (opcion != 5);
        }
        // --- FUNCIONES DEL PROGRAMA ---
        public static void MostrarMenu()
        {
            Console.WriteLine("********* MENÚ DE OPCIONES *********");
            Console.WriteLine("1. Agregar contacto");
            Console.WriteLine("2. Borrar contacto por apellido");
            Console.WriteLine("3. Buscar por apellido contacto");
            Console.WriteLine("4. Mostrar todos los contactos");
            Console.WriteLine("5. Salir");
            Console.WriteLine("*************************************");
            Console.Write("Ingrese el número de opción: ");
        }
        public static void AgregarContacto(List<Contacto> lista)
        {
            Contacto nuevoContacto = new Contacto();
            Console.WriteLine("Ingrese nombre");
            nuevoContacto.nombre = Console.ReadLine();
            Console.WriteLine("Ingrese apellido");
            nuevoContacto.apellido = Console.ReadLine();
            Console.WriteLine("Ingrese teléfono");
            nuevoContacto.telefono = int.Parse(Console.ReadLine());

            
        
lista.Add(nuevoContacto);
            Console.WriteLine("Contacto agregado correctamente.");
        }
        public static void BorrarContacto(List<Contacto> lista)
        {
            Console.WriteLine("Ingrese apellido");
            string apellido = Console.ReadLine();
            // Recorrido inverso para borrar de forma segura
            for (int i = lista.Count - 1; i >= 0; i--)
            {
                if (lista[i].apellido.ToLower() == apellido.ToLower())
                {
                    lista.RemoveAt(i);
                    Console.WriteLine("Elemento borrado");
                }
            }
        }
        public static void BuscarContacto(List<Contacto> lista)
        {
            Console.WriteLine("Ingrese apellido");
            string apellido = Console.ReadLine();
            bool bandera = false;
            foreach (var item in lista)
            {
                if (item.apellido.ToLower() == apellido.ToLower())
                {
                    Console.WriteLine("Nombre {0} Apellido {1} Telefono {2}", item.nombre,

                    item.apellido, item.telefono);
                    bandera = true;
                }
            }
            if (bandera == true)
            {
                Console.WriteLine("Contacto existe");
            }
            else
            {
                Console.WriteLine("No existe");
            }
        }
        public static void MostrarTodos(List<Contacto> lista)
        {
            Console.WriteLine("************");
            foreach (var item in lista)
            {
                Console.WriteLine("Nombre {0} Apellido {1} Telefono {2}", item.nombre,

                item.apellido, item.telefono);

            }
            Console.WriteLine("************");
        }
    }
}