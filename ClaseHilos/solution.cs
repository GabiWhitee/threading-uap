using System.Threading;

namespace ClaseHilos
{
    internal class Producto
   {
      public string Nombre { get; set; }
      public decimal PrecioUnitarioDolares { get; set; }
      public int CantidadEnStock { get; set; }

      public Producto(string nombre, decimal precioUnitario, int cantidadEnStock)
      {
         Nombre = nombre;
         PrecioUnitarioDolares = precioUnitario;
         CantidadEnStock = cantidadEnStock;
      }
   }
   internal class Solution //reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock
   {
        static Barrier barrera = new Barrier(2, (b) =>
        {
            Console.WriteLine($"Post-Phase action: {b.CurrentPhaseNumber}");
        });
        public static Barrier barrera_
        {
            get { return barrera; }
        }

        static List<Producto> productos = new List<Producto>
        {
            new Producto("Camisa", 10, 50),
            new Producto("Pantalón", 8, 30),
            new Producto("Zapatilla/Champión", 7, 20),
            new Producto("Campera", 25, 100),
            new Producto("Gorra", 16, 10)
        };

      static double precio_dolar = 500;

      static void Tarea1()
      {
            for (int i = 0; i < productos.Count; i++)
            {
                productos[i].CantidadEnStock += 10;
            }
            barrera.SignalAndWait();
      }
      static void Tarea2()
      {
            precio_dolar = precio_dolar * (1.1);
            barrera.SignalAndWait();
      }
      static void Tarea3()
      {
            decimal diezmo = 1.1m;
            for (int j = 0; j < productos.Count; j++)
            {
                productos[j].PrecioUnitarioDolares = productos[j].PrecioUnitarioDolares * diezmo;
            }
            decimal precioTotal = 0;
            for (int i = 0; i < productos.Count; i++)
            {
                precioTotal = precioTotal + productos[i].PrecioUnitarioDolares;
            }
            foreach (Producto producto in productos)
            {
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Producto:\t" + producto.Nombre);
                Console.WriteLine("Stock:\t\t" + producto.CantidadEnStock);
                Console.WriteLine("Precio (USD):\t" + producto.PrecioUnitarioDolares);
            }
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("Precio total: " + precioTotal);
    }

      internal static void Excecute()
      {
            Thread hilo = new Thread(new ThreadStart(Tarea1));
            Thread hilo2 = new Thread(new ThreadStart(Tarea2));
            Thread hilo3 = new Thread(new ThreadStart(Tarea3));

            hilo.Start();
            hilo2.Start();
            hilo3.Start(); //para demostrar que funciona lo ponemos al principio
    }
  }
}