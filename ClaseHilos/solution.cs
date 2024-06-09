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

      static List<Producto> productos = new List<Producto>
        {
            new Producto("Camisa", 10, 50),
            new Producto("Pantalón", 8, 30),
            new Producto("Zapatilla/Champión", 7, 20),
            new Producto("Campera", 25, 100),
            new Producto("Gorra", 16, 10)
        };

      static int precio_dolar = 500;

        static void Tarea1()
        {
            Console.WriteLine("Tarea 1: Actualizar stock.");
            foreach (var producto in productos)
            {
                producto.CantidadEnStock += 10;
            }
        }

        static void Tarea2()
        {
            Console.WriteLine("Tarea 2: Actualizar precio del dólar.");
            precio_dolar = 510; 
        }

        static void Tarea3()
        {
            Console.WriteLine("Tarea 3: Generar informe.");
            foreach (var producto in productos)
            {
                decimal valorInventario = producto.PrecioUnitarioDolares * producto.CantidadEnStock;
                Console.WriteLine($"Producto: {producto.Nombre}, Cantidad: {producto.CantidadEnStock}, Valor en USD: {valorInventario}");
            }
            Console.WriteLine($"Precio del dólar actualizado: {precio_dolar}");
        }

        internal static void Excecute()
        {
            Thread task1 = new Thread(Tarea1);
            Thread task2 = new Thread(Tarea2);
            Thread task3 = new Thread(Tarea3);

            task1.Start();
            task2.Start();
            task3.Start();

            task1.Join();
            task2.Join();
            task3.Join();

            Console.WriteLine("Todas las tareas han finalizado.");
            Console.ReadLine();
        }
    }
}