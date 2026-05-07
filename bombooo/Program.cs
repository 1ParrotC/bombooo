using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using bombooo;

const string DbFile = "biblioteca.json";

var libros = LoadLibrary();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("BiblioTech - Menú");
    Console.WriteLine("1) Listar libros");
    Console.WriteLine("2) Añadir libro");
    Console.WriteLine("3) Cambiar disponibilidad");
    Console.WriteLine("4) Guardar");
    Console.WriteLine("5) Salir (guarda automáticamente)");
    Console.WriteLine("6) Ejecutar Ejercicio 2 (separado)");
    Console.Write("Elige una opción: ");
    var opt = Console.ReadLine();

    switch (opt)
    {
        case "1":
            ListBooks(libros);
            break;
        case "2":
            AddBook(libros);
            break;
        case "3":
            ToggleAvailability(libros);
            break;
        case "4":
            SaveLibrary(libros);
            break;
        case "6":
            // Ejecuta la versión del ejercicio 2 implementada en Ejercicio2.cs
            Ejercicio2.Ejecutar();
            break;
        case "5":
            SaveLibrary(libros);
            return;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
}

void ListBooks(List<Libro> lista)
{
    if (lista.Count == 0)
    {
        Console.WriteLine("No hay libros en la biblioteca.");
        return;
    }

    Console.WriteLine("Listado de libros:");
    for (int i = 0; i < lista.Count; i++)
    {
        var libro = lista[i];
        Console.WriteLine($"[{i}] {libro} {(libro.Disponible ? "- Disponible" : "- No disponible")} ");
    }
}

void AddBook(List<Libro> lista)
{
    Console.Write("Título: ");
    var titulo = Console.ReadLine() ?? string.Empty;
    Console.Write("Autor: ");
    var autor = Console.ReadLine() ?? string.Empty;
    Console.Write("Año: ");
    if (!int.TryParse(Console.ReadLine(), out var anyo))
    {
        Console.WriteLine("Año no válido. Operación cancelada.");
        return;
    }
    Console.Write("Disponible (s/n): ");
    var d = (Console.ReadLine() ?? string.Empty).ToLower();
    var disponible = d == "s" || d == "y" || d == "yes" || d == "si";

    lista.Add(new Libro(titulo, autor, anyo, disponible));
    Console.WriteLine("Libro añadido.");
}

void ToggleAvailability(List<Libro> lista)
{
    ListBooks(lista);
    Console.Write("Índice del libro a cambiar: ");
    if (!int.TryParse(Console.ReadLine(), out var idx) || idx < 0 || idx >= lista.Count)
    {
        Console.WriteLine("Índice no válido.");
        return;
    }

    // Como las propiedades son de solo lectura, recreamos el objeto con el valor contrario
    var old = lista[idx];
    var nuevo = new Libro(old.Titulo, old.Autor, old.Anyo, !old.Disponible);
    lista[idx] = nuevo;
    Console.WriteLine("Disponibilidad cambiada.");
}

void SaveLibrary(List<Libro> lista)
{
    var serial = new List<object>();
    foreach (var l in lista)
    {
        serial.Add(new { Titulo = l.Titulo, Autor = l.Autor, Anyo = l.Anyo, Disponible = l.Disponible });
    }

    var opts = new JsonSerializerOptions { WriteIndented = true };
    File.WriteAllText(DbFile, JsonSerializer.Serialize(serial, opts));
    Console.WriteLine($"Biblioteca guardada en {DbFile}.");
}

List<Libro> LoadLibrary()
{
    if (!File.Exists(DbFile))
    {
        // datos iniciales
        return new List<Libro>
        {
            new Libro("Cien años de soledad", "Gabriel García Márquez", 1967, true),
            new Libro("Don Quijote de la Mancha", "Miguel de Cervantes", 1605, false),
            new Libro("El Principito", "Antoine de Saint-Exupéry", 1943, true)
        };
    }

    try
    {
        var json = File.ReadAllText(DbFile);
        var items = JsonSerializer.Deserialize<List<SerializableLibro>>(json);
        var list = new List<Libro>();
        if (items != null)
        {
            foreach (var s in items)
            {
                list.Add(new Libro(s.Titulo, s.Autor, s.Anyo, s.Disponible));
            }
        }

        return list;
    }
    catch
    {
        Console.WriteLine("Error leyendo la biblioteca. Se cargan valores por defecto.");
        return new List<Libro>();
    }
}

// DTO para serialización
internal record SerializableLibro(string Titulo, string Autor, int Anyo, bool Disponible);
