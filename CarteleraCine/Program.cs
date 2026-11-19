using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace CarteleraCine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FuncionRepositorio repo = new FuncionRepositorio();
            string opcion;

            do
            {
                opcion = LeerOpcionMenu();

                switch (opcion)
                {
                    case "1":
                        RegistrarFuncion(repo);
                        break;
                    case "2":
                        ListarFunciones(repo);
                        break;
                    case "3":
                        BuscarFuncionPorId(repo);
                        break;
                    case "4":
                        ActualizarPrecio(repo);
                        break;
                    case "5":
                        EliminarFuncion(repo);
                        break;
                    case "6":
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            } while (opcion != "6");
        }

        private static string LeerOpcionMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== MENÚ CARTELERA ===");
            Console.WriteLine("1. Registrar función");
            Console.WriteLine("2. Listar funciones");
            Console.WriteLine("3. Buscar función por Id");
            Console.WriteLine("4. Actualizar precio");
            Console.WriteLine("5. Eliminar función");
            Console.WriteLine("6. Salir");
            Console.Write("Elige una opción: ");
            return Console.ReadLine()?.Trim() ?? "";
        }

        private static void RegistrarFuncion(FuncionRepositorio repo)
        {
            Console.Write("Película: ");
            string pelicula = (Console.ReadLine() ?? "").Trim();

            Console.Write("Sala: ");
            string sala = (Console.ReadLine() ?? "").Trim();

            Console.Write("Precio: ");
            bool precioOk = decimal.TryParse(Console.ReadLine(), out decimal precio);

            if (string.IsNullOrWhiteSpace(pelicula) || string.IsNullOrWhiteSpace(sala) || !precioOk || precio <= 0)
            {
                Console.WriteLine("Datos inválidos.");
                return;
            }

            try
            {
                repo.Insertar(new Funcion
                {
                    Pelicula = pelicula,
                    Sala = sala,
                    Precio = precio
                });
                Console.WriteLine("Función registrada.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de base de datos: {ex.Message}");
            }
        }

        private static void ListarFunciones(FuncionRepositorio repo)
        {
            try
            {
                List<Funcion> lista = repo.ListarTodos();

                if (lista.Count == 0)
                {
                    Console.WriteLine("No hay funciones registradas.");
                    return;
                }

                decimal totalPrecios = 0;
                foreach (Funcion f in lista)
                {
                    Console.WriteLine(f.ToString());
                    totalPrecios += f.Precio;
                }

                Console.WriteLine($"TOTAL DE PRECIOS: Q {totalPrecios:N2}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de base de datos: {ex.Message}");
            }
        }

        private static void BuscarFuncionPorId(FuncionRepositorio repo)
        {
            Console.Write("Id de la función: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            try
            {
                Funcion? funcion = repo.BuscarPorId(id);
                if (funcion == null)
                {
                    Console.WriteLine("No existe ese Id.");
                }
                else
                {
                    Console.WriteLine(funcion.ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de base de datos: {ex.Message}");
            }
        }

        private static void ActualizarPrecio(FuncionRepositorio repo)
        {
            Console.Write("Id de la función: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            try
            {
                Funcion? funcion = repo.BuscarPorId(id);
                if (funcion == null)
                {
                    Console.WriteLine("No existe ese Id.");
                    return;
                }

                Console.WriteLine($"Función actual: {funcion}");
                Console.Write("Nuevo precio: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal nuevoPrecio) || nuevoPrecio <= 0)
                {
                    Console.WriteLine("Precio inválido.");
                    return;
                }

                funcion.Precio = nuevoPrecio;
                bool actualizado = repo.Actualizar(funcion);
                Console.WriteLine(actualizado ? "Función actualizada." : "No existe ese Id.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de base de datos: {ex.Message}");
            }
        }

        private static void EliminarFuncion(FuncionRepositorio repo)
        {
            Console.Write("Id de la función: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            try
            {
                bool eliminado = repo.Eliminar(id);
                Console.WriteLine(eliminado ? "Función eliminada." : "No existe ese Id.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de base de datos: {ex.Message}");
            }
        }
    }
}