using System;
using System.Formats.Tar;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
class N5
{
    static void Main()
    {
        string opcion;
        List<string> ListaTareas = new List<string>();
        do
        {
            Console.Clear();
            Console.WriteLine("############################");
            Console.WriteLine("  SISTEMA GESTOR DE TAREAS");
            Console.WriteLine("############################");
            Console.Write("1. Agregar tarea\n2. Mostrar tareas pendientes\n3. Marcar tarea como completa\n4. Eliminar tarea\nElija una opcion: ");
            opcion = (Console.ReadLine() ?? "").Trim();
            switch (opcion)
            {
                case "1":
                    Console.Clear();
                    string tarea;
                    Console.Write("Ingrese una tarea para agregar: ");
                    tarea = (Console.ReadLine() ?? "").ToUpper().Trim();
                    AgregarTarea(tarea, ListaTareas);
                    Console.ReadKey();
                    break;

                case "2":
                    Console.Clear();
                    MostrarTareas(ListaTareas);
                    Console.ReadKey();
                    break;

                case "3":
                    Console.Clear();
                    Console.Write("Ingrese la tarea que completo : ");
                    tarea = (Console.ReadLine() ?? "").ToUpper().Trim();
                    CompletarTarea(tarea,ListaTareas);
                    Console.ReadKey();
                    break;

                case "4":
                    Console.Clear();
                    Console.Write("Ingrese la tarea que quiera quitar: ");
                    tarea=(Console.ReadLine() ?? "").ToUpper().Trim();
                    EliminarTarea(tarea,ListaTareas);
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

    static List<string> AgregarTarea(string Tarea, List<string> ListaTareas)
    {
        if (Tarea=="")
        {
            Console.WriteLine("No ingreso una tarea");
        }
        else
        {
        if (ListaTareas.Remove($"- {Tarea}"))
            Console.WriteLine("Tarea duplicada");
        else
            ListaTareas.Add($"- {Tarea}");
        }
        return ListaTareas;
    }
    static void MostrarTareas(List<string> ListaTareas) 
    {
        if (ListaTareas.Count == 0)
            Console.WriteLine("No tiene tareas pendientes.");
        else
        {
            Console.WriteLine($"#######################");
            Console.WriteLine($"Sus tareas pendientes son:");
            foreach (var Tarea in ListaTareas)
                Console.WriteLine(Tarea);
            Console.WriteLine($"#######################");
        }
        return;
    }
    static List<string> CompletarTarea(string TareaCompletada, List<string> ListaTareas)
    {
        if (TareaCompletada=="")
            {
            Console.WriteLine("No ingreso una tarea");
            return ListaTareas;
            }
        int contador = 0;
        foreach (var Tarea in ListaTareas)
        {
            if (Tarea == $"- {TareaCompletada}")
            {
            ListaTareas[contador] = $"- X - {TareaCompletada}";
            return ListaTareas;
            }
            contador++;
        }
        return ListaTareas;
    }
    static List<string> EliminarTarea(string Tarea, List<string> ListaTareas)
    {
        if (Tarea=="")
            {
            Console.WriteLine("No ingreso una tarea");
            return ListaTareas;
            }
        if (ListaTareas.Remove($"- X - {Tarea}"))
            ListaTareas.Remove($"- X - {Tarea}");
        if (ListaTareas.Remove($"- {Tarea}"))
            ListaTareas.Remove($"- {Tarea}");
        return ListaTareas;
    }

}