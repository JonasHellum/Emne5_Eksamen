// See https://aka.ms/new-console-template for more information

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
Console.WriteLine(MessierCatalogue.DisplayAll(messiers.Search("", maxResults: 1000)));

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



Console.WriteLine("\nSorting query: Messier catalogue number, descending");
Console.WriteLine(MessierCatalogue.DisplayAll(messiers.Sort("messier catalogue number", "descending")));

Console.WriteLine("\nSorting query: NGC (or other) catalogue number, ascending");
Console.WriteLine(MessierCatalogue.DisplayAll(messiers.Sort("NGC (or other) catalogue number")));

Console.WriteLine("\nSorting query: Visual magnitude, descending");
Console.WriteLine(MessierCatalogue.DisplayAll(messiers.Sort("Visual magnitude", "descending")));



Console.WriteLine("\nSearching for \"Open\" in a filtered list in field \"Constellation\" inside \"Sagittarius\"");
var filteredList = messiers.Filter("Constellation", "Sagittarius");
Console.WriteLine(MessierCatalogue.DisplayAll(filteredList.Search("Open"))); // There is no "Galaxy" within "Sagittarius"

Console.WriteLine("\nSearching for \"Galaxy\" in a filtered list in field \"Constellation\" inside \"Virgo\"");
Console.WriteLine(MessierCatalogue.DisplayAll(messiers.Filter("Constellation", "Virgo").Search("Galaxy")));

Console.WriteLine("\nSorting for \"Visual magnitude\" in a filtered list in field \"Constellation\" inside \"Virgo\"");
Console.WriteLine(MessierCatalogue.DisplayAll(messiers.Filter("Constellation", "Virgo").Sort("Visual magnitude")));














