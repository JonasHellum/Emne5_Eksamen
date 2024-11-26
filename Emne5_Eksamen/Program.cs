﻿// See https://aka.ms/new-console-template for more information

using Emne5_Eksamen;

string fileName = "Messier.csv";
var messiers = new MessierCatalogue();
messiers.GetMessiersFromCsv(fileName);

if (messiers == null || !messiers.Search("").Any())
{
    Console.WriteLine($"Error: Can't find any data inside \"{fileName}\".");
    return;
}

Console.WriteLine("");
Console.WriteLine(MessierCatalogue.DisplayAll(messiers.Search("", 10000)));

Console.WriteLine("\nSearch query: Nebula");
Console.WriteLine(MessierCatalogue.DisplayAll(messiers.Search("Nebula")));

Console.WriteLine("\nSearch query: Spiral Galaxy");
Console.WriteLine(MessierCatalogue.DisplayAll(messiers.Search("Spiral Galaxy")));

Console.WriteLine("\nSearch query: Ursa Major");
Console.WriteLine(MessierCatalogue.DisplayAll(messiers.Search("Ursa Major")));

Console.WriteLine("\nSearch query: Sunflower");
Console.WriteLine(MessierCatalogue.DisplayAll(messiers.Search("Sunflower")));

Console.WriteLine("\nSearch query: M31");
Console.WriteLine(MessierCatalogue.DisplayAll(messiers.Search("M31")));


Console.WriteLine("\nSorting query: Messier catalogue number");
Console.WriteLine(MessierCatalogue.DisplayAll(messiers.Sort("messier catalogue number", "desc")));
