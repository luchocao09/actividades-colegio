using System;

class Program
{
    static void Main()
    {
        Alumno alumno1 = new Alumno("Perez", "Juan", 20, 12345678);
        Alumno alumno2 = new Alumno("Gomez", "Maria", 22, 87654321);
        Alumno alumno3 = new Alumno("Lopez", "Carlos", 19, 11223344);
        Alumno alumno4 = new Alumno("Rodriguez", "Ana", 21, 55667788);
        Alumno alumno5 = new Alumno("Garcia", "Lucia", 23, 99887766);
        Alumno alumno6 = new Alumno();
        Alumno alumno7 = new Alumno();
        Alumno alumno8 = new Alumno();
        Alumno alumno9 = new Alumno();
        Alumno alumno10 = new Alumno();
        //6 Sanchez Lucia 20 390001111
        //7 Diaz Pedro 23 320002222
        //8 Torres Sofia 19 410003333
        //9 Ruiz Diego 21 360004444
        //10 Flores Elena 20 380005555
        alumno6.Apellido="Sanchez";
        alumno6.Nombre="Lucia";
        alumno6.Edad=20;
        alumno6.Dni=390001111;
        alumno7.Apellido="Ruiz";
        alumno7.Nombre="Diego";
        alumno7.Edad=21;
        alumno7.Dni=360004444;
        alumno8.Apellido="Flores";
        alumno8.Nombre="Elena";
        alumno8.Edad=20;
        alumno8.Dni=380005555;
        alumno9.Apellido="Sanchez";
        alumno9.Nombre="Lucia";
        alumno9.Edad=20;
        alumno9.Dni=390001111;
        alumno10.Apellido="Diaz";
        alumno10.Nombre="Pedro";
        alumno10.Edad=23;
        alumno10.Dni=320002222;

        alumno1.Saludar();
        alumno2.Saludar();
        alumno3.Saludar();
        alumno4.Saludar();
        alumno5.Saludar();
        alumno6.Saludar();
        alumno7.Saludar(); 
        alumno8.Saludar();
        alumno9.Saludar();
        alumno10.Saludar();

        Console.WriteLine($"El Dni: 12345678 correspponde a {alumno1.DevolverApellido(12345678)}");
        Console.WriteLine($"El Dni: 87654321 correspponde a {alumno2.DevolverApellido(87654321)}");
        Console.WriteLine($"El Dni: 11223344 correspponde a {alumno3.DevolverApellido(11223344)}");
        Console.WriteLine($"El Dni: 55667788 correspponde a {alumno4.DevolverApellido(55667788)}");
        Console.WriteLine($"El Dni: 99887766 correspponde a {alumno5.DevolverApellido(99887766)}");
        Console.WriteLine($"El Dni: 390001111 correspponde a {alumno6.DevolverApellido(390001111)}");
        Console.WriteLine($"El Dni: 320002222 correspponde a {alumno7.DevolverApellido(320002222)}");
        Console.WriteLine($"El Dni: 410003333 correspponde a {alumno8.DevolverApellido(410003333)}");
        Console.WriteLine($"El Dni: 360004444 correspponde a {alumno9.DevolverApellido(360004444)}");
        Console.WriteLine($"El Dni: 380005555 correspponde a {alumno10.DevolverApellido(380005555)}");
    }
}

public class Alumno
{
    public string Apellido{get;set;}
    public string Nombre{get;set;}
    public int Edad
    {
        get;
        set
        {
         if (value < 0)
         {
            Console.WriteLine("La edad no puede ser negativa. Se asignará 0 por defecto.");
            field = 0;
         }
         else
         {
            field = value;
         }
        }
    }
    public long Dni{get;set;}

    public int Id {get;set;}
    public static int UltimoId=0;

    public Alumno(string apellido, string nombre, int edad, long dni)
    {
        this.Apellido = apellido;
        this.Nombre = nombre;
        this.Edad = edad;
        this.Dni = dni;
        this.Id = ++UltimoId;
    }
    public Alumno()
    {
        this.Apellido="";
        this.Nombre="";
        this.Edad=0;
        this.Dni=0;
        this.Id = ++UltimoId;
    }
    public void Saludar()
    {
        Console.WriteLine($"Hola, soy {Nombre} {Apellido}, tengo {Edad} años y mi DNI es {Dni}");
    }
    public string DevolverApellido(long dni)
    {
        if (Dni == this.Dni) 
        return this.Apellido;
        return "";
    }

}