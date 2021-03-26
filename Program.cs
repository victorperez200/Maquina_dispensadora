using System;
using System.Collections.Generic;


namespace Maquina_dispensadora
{
    public class Producto
    {
      public string Nombre {get; set;}
      public string Codigo {get; set;}
      public int Cantidad {get; set;}
      public double Valor {get; set;}
      public double Cambio {get; set;}
      
    
      public void sumarCantidad(int cantidad)

      {
         this.Cantidad += cantidad;
      }
 
      public bool validarCantidad()
      {
         if(this.Cantidad > 0)
         {
           return true;
         }
         
         return false; 
      }

      public bool validarValor(double valor)
      
      {
        if (this.Valor <= valor)
        {
           this.Cambio = valor - this.Valor;
           return true;
        }

        return false;
      }

      public void restarProducto()

      { 
         this.Cantidad--;
      }
      
    }

    public class Dispensadora
    {
      public List<Producto> Productos{get; set;}
      public string Pago {get; set;}
      
      public Dispensadora()
      {

       this.Productos = new List<Producto>();

       Producto cocacola = new Producto();
       cocacola.Codigo = "01";
       cocacola.Nombre = "Coca Cola";
       cocacola.Cantidad = 2;
       cocacola.Valor = 15;

       Producto papas = new Producto();
       papas.Nombre = "papas fritas";
       papas.Codigo = "02";
       papas.Cantidad = 5;
       papas.Valor = 20;
       
       Producto chocolate = new Producto();
       chocolate.Codigo = "03";
       chocolate.Nombre = "chocolate";
       chocolate.Cantidad = 10;
       chocolate.Valor = 35;
       
       this.Productos.Add(cocacola);
       this.Productos.Add(papas);
       this.Productos.Add(chocolate);
      }

       public int validarProducto(string codigo)
       {
          int encontro = -1;
          int v = 0;

          for (int v = 0; v < this.Productos.Count; v++)
          {
              if (this.Productos[v].Codigo == codigo)
              {
                encontro = v;
              }
          }

          
            return encontro;
       }
      
       public bool agregarProducto(Producto producto)
       {

           int enc = this.validarProducto(producto.Codigo);

           if( enc >= 0)
           {
              this.Productos[enc].sumarCantidad(producto.Cantidad);
           }
           else
           {
              this.Productos.Add(producto);
           }

           return true;
       }
       
        /*public bool eliminarProducto(string codigo)

       {

           int enc = this.validarProducto(codigo);

           if( enc >= 0)
           {
              this.Productos.RemoveAt;
              return true;
           }

           return false;*/
       }

        public double validarBilletes(string[] billetes)

        { 
           double total = 0;
           foreach (string item in billetes)
           {
              try
              { 

              total += double.Parse(item);

              }
              
              catch (Exception e) { }
           }

           return total;
        }
        
        public Producto vender(string codigo)
        {
           int enc = this.validarProducto(codigo);

           if (enc >= 0)

           {
             if (this.Productos[enc].validarCantidad())

             {
                string[] monedas = this.Pago.Split('-');

                double total = this.validarBilletes(billetes);

                if (this.Productos[enc].validarValor(total))
                { 
                   this.Productos[enc].restarProducto();

                   return this.Productos[enc]; 
                }
        
             }
           }
           

           return null;
        }
         
        public string listarProducto()
        {
           string lista = "";

           foreach (Producto item in this.Productos)
           {
             lista += item.Codigo + " " + item.Nombre + " " + item.Cantidad + " " + item.Valor + "\n";
           }
      
         return lista;
        }


    class Program
    {
        static void Main(string[] args)
        {
            Dispensadora dispensador = new Dispensadora();
        
         while(true)
        {  
         Console.WriteLine("Bienvenis a la dispensadora");
         Console.WriteLine(dispensador.listarProducto());

         Console.WriteLine("1. Agregar producto");
         Console.WriteLine("2. Eliminar producto");
         Console.WriteLine("3. Comprar producto"); 

         string option = Console.ReadLine();

         switch (option)

         {
           case "1":
                Producto producto = new Producto();
                Console.Write("Codigo: ");
                producto.Codigo = Console.ReadLine();

                Console.Write("Nombre: ");
                producto.Nombre = Console.ReadLine();

                Console.Write("Cantidad: ");
                producto.Cantidad = Convert.ToInt32(Console.ReadLine());

                Console.Write("Valor: ");
                producto.Valor = double.Parse(Console.ReadLine());

                dispensador.agregarProducto(producto);
                break;

          case "2":
                Console.Write("Codigo: ");
                string codigo = Console.ReadLine();

                dispensador.eliminarProducto(codigo);
                break;

          case "3":
                Console.Write("Codigo");
                string codigo_venta = Console.ReadLine();

                Console.Write("Solo Billetes de (50-100-200): ");
                dispensador.Pago = Console.ReadLine();

                Producto pcomprado = dispensador.vender(codigo_venta);

                if (pcomprado == null)
                {
                  Console.WriteLine("No se puedo sacar el producto");
                }
                else
                {
                   Console.WriteLine("Su producto es {0} y su cambio es {1}", pcomprado.Codigo, pcomprado.Cambio);
                }
                
                break;

                
         }

             Console.WriteLine("Desea continuar si/no: ");

             if (Console.ReadLine() == "no")
             {
               break;
             }
        }

    }

    }
}
