using System;
using System.Collections.Generic;
using System.IO;

namespace bombooo
{
    // Ejercicio 2 implementado en una clase separada para no modificar el programa original.
    public static class Ejercicio2
    {
        public static void Ejecutar()
        {
            // Apartado 2.1 - Crear lista y añadir tres libros
            var lista = new List<Libro>
            {
                new Libro("1984", "George Orwell", 1949, true),
                new Libro("El Quijote", "Miguel de Cervantes", 1605, false),
                new Libro("Rebelión en la granja", "George Orwell", 1945, true)
            };

            // b) Mostrar todos los libros con foreach y ToString()
            Console.WriteLine("--- Apartado 2.1 b) - Todos los libros ---");
            foreach (var libro in lista)
            {
                Console.WriteLine(libro.ToString());
            }

            // c) Mostrar solo los libros cuyo autor contenga "Orwell"
            Console.WriteLine();
            Console.WriteLine("--- Apartado 2.1 c) - Libros cuyo autor contiene 'Orwell' ---");
            foreach (var libro in lista)
            {
                if (libro.Autor != null && libro.Autor.Contains("Orwell", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(libro.ToString());
                }
            }

            // Apartado 2.2 - Fecha de registro
            Console.WriteLine();
            Console.WriteLine("--- Apartado 2.2 - Fecha actual (formato corto) ---");
            Console.WriteLine(DateTime.Now.ToShortDateString());

            // Apartado 2.3 - Guardar en fichero
            Console.WriteLine();
            Console.WriteLine("--- Apartado 2.3 - Guardar en fichero 'libros_ej2.txt' ---");
            var ruta = "libros_ej2.txt";
            GuardarLibros(lista, ruta);
            Console.WriteLine($"Fichero creado: {Path.GetFullPath(ruta)}");
        }

        private static void GuardarLibros(List<Libro> lista, string ruta)
        {
            using var sw = File.CreateText(ruta);
            foreach (var l in lista)
            {
                // Formato: Titulo;Autor;Anyo;Disponible
                sw.WriteLine($"{l.Titulo};{l.Autor};{l.Anyo};{l.Disponible}");
            }
        }
    }
}
