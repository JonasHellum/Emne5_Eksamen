namespace Emne5_Eksamen;

public class MessierCatalogue
{
    private List<Messier> messiers = new List<Messier>();
    
    public List<Messier> GetMessiersFromCsv(string csvFile)
    {
        if (!File.Exists(csvFile))
        {
            Console.WriteLine($"CSV file {csvFile} not found");
            return null;
        }
        
        StreamReader sr = new StreamReader(csvFile);
        int lineNr = 0;

        using (sr)
        {
            // Skipping 18 lines in the csv.
            for (int i = 0; i < 18; i++)
            {
                lineNr++;
                sr.ReadLine();
            }
            
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(',');
                string[] columns = { "Name", "NGC", "Constellation", "Class", "Right ascension", 
                                     "Declination", "Magnitude", "Angular size", "Burnham", "Remarks" };

                string name = values[0];
                string NGC = values[1];
                string constellation = values[2];
                string classInCsv = values[3];
                string rightAscension = values[4];
                string declination = values[5];
                string magnitude = values[6];
                string angularSize = values[7];
                string burnham = values[8];
                string remarks = values[9];
                
                
                // So each object which has an error would not be saved but that it's still going
                // through everything to give an appropirate message back.
                // Was the plan until I noticed that the format and stuff was wrong in it
                // so I had everything be saved eitherway into the list.
                bool parseError = false;
                
                if (values.Length != 10) // Checks that the format is correct
                {
                    Console.WriteLine($"Error on line: \"{lineNr + 1}\": No valid format.");
                    // parseError = true;
                }
                
                for (int i = 0; i < values.Length; i++) // Checks that it's data in each column
                {
                    if (string.IsNullOrWhiteSpace(values[i]))
                    {
                        Console.WriteLine($"Error on line: \"{lineNr + 1}\": Column \"{columns[i]}\" missing data.");
                        // parseError = true;
                    }
                }
                
                // try
                // {
                //     NGC = int.Parse(values[1]);
                // }
                // catch (Exception e)
                // {
                //     Console.WriteLine($"Error in column: \"NGC\" on line: \"{lineNr + 1}\`": \"{e.Message}\"");
                //     parseError = true;
                // }
                
                // If no errors the objects get saved to a list.
                if (!parseError)
                {
                    Messier messier = new Messier(name, NGC, constellation, classInCsv, rightAscension, declination, magnitude, angularSize, burnham, remarks);
                    messiers.Add(messier);
                }

                lineNr++;
            }
        }

        return messiers;
    }
    
    
    
    public static string Display(Messier messier)
    {
        return $"Messier catalogue number: \"{messier.Name}\", NGC (or other) catalogue number: \"{messier.NGC}\", Constellation: \"{messier.Constellation}\", Object class: \"{messier.Class}\", " +
               $"Right ascension: \"{messier.RightAscension}\", Declination: \"{messier.Declination}\", " +
               $"Visual magnitude: \"{messier.Magnitude}\", Diameter: \"{messier.AngularSize}\", Burnham's code: \"{messier.Burnham}\", " +
               $"Remarks: \"{messier.Remarks}\"";
        
        return $"Name: {messier.Name}, NGC: {messier.NGC}, Constellation: {messier.Constellation}, Class: {messier.Class}, " +
               $"Right ascension: {messier.RightAscension}, Declination: {messier.Declination}, " +
               $"Magnitude: {messier.Magnitude}, Angular size {messier.AngularSize}, Burnham: {messier.Burnham}, " +
               $"Remarks: {messier.Remarks}";
    }
    
    public List<Messier> Search(string messierInfo, int maxResults = 10)
    {
        var results = messiers
            .Where(c => c != null &&
                        (c.Name.Contains(messierInfo, StringComparison.OrdinalIgnoreCase) ||
                         c.NGC.Contains(messierInfo, StringComparison.OrdinalIgnoreCase) ||
                         c.Constellation.Contains(messierInfo, StringComparison.OrdinalIgnoreCase) ||
                         c.Class.Contains(messierInfo, StringComparison.OrdinalIgnoreCase) ||
                         c.RightAscension.Contains(messierInfo, StringComparison.OrdinalIgnoreCase) ||
                         c.Declination.Contains(messierInfo, StringComparison.OrdinalIgnoreCase) ||
                         c.Magnitude.Contains(messierInfo, StringComparison.OrdinalIgnoreCase) ||
                         c.AngularSize.Contains(messierInfo, StringComparison.OrdinalIgnoreCase) ||
                         c.Burnham.Contains(messierInfo, StringComparison.OrdinalIgnoreCase) ||
                         c.Remarks.Contains(messierInfo, StringComparison.OrdinalIgnoreCase)))
            .Take(maxResults)
            .ToList();
    
        return results;
    }
    
    public static string DisplayAll(List<Messier> messiers)
    {
        if (messiers.Count == 0)
            return "No info found.";
    
        if (messiers.Count == 1)
            return Display(messiers[0]);
        
        return string.Join("\n", messiers.Select(c => Display(c)));
    }
    
    public List<Messier> Sort(string data, string order, int maxResults = 20)
    {
        var sorterContacts = messiers.Where(c => c != null).ToList();

        Func<string, int> extractNumericPart = name =>
        {
            if (string.IsNullOrEmpty(name))
            {
                return 0;
            }
            
            var match = System.Text.RegularExpressions.Regex.Match(name, @"\d+");
            return match.Success 
                ? int.Parse(match.Value) 
                : 0;
        };

        switch (data.ToLower())
        {
            case "messier catalogue number":
                    
                sorterContacts = order.ToLower() == "desc"
                    ? sorterContacts.OrderByDescending(c => extractNumericPart(c.Name)).ToList()
                    : sorterContacts.OrderBy(c => extractNumericPart(c.Name)).ToList();
                break;
                
            case "ngc (or other) catalogue number":
                    
                sorterContacts = order.ToLower() == "desc"
                    ? sorterContacts.OrderByDescending(c => c.NGC ?? string.Empty).ToList()
                    : sorterContacts.OrderBy(c => c.NGC ?? string.Empty).ToList();
                break;
                
            case "constellation":
                    
                sorterContacts = order.ToLower() == "desc"
                    ? sorterContacts.OrderByDescending(c => c.Constellation ?? string.Empty).ToList()
                    : sorterContacts.OrderBy(c => c.Constellation ?? string.Empty).ToList();
                break;
                
            case "object class":
                    
                sorterContacts = order.ToLower() == "desc"
                    ? sorterContacts.OrderByDescending(c => c.Class ?? string.Empty).ToList()
                    : sorterContacts.OrderBy(c => c.Class ?? string.Empty).ToList();
                break;
                    
            case "right ascension":
                    
                sorterContacts = order.ToLower() == "desc"
                    ? sorterContacts.OrderByDescending(c => c.RightAscension ?? string.Empty).ToList()
                    : sorterContacts.OrderBy(c => c.RightAscension ?? string.Empty).ToList();
                break;
                    
            case "declination":
                    
                sorterContacts = order.ToLower() == "desc"
                    ? sorterContacts.OrderByDescending(c => c.Declination ?? string.Empty).ToList()
                    : sorterContacts.OrderBy(c => c.Declination ?? string.Empty).ToList();
                break;
                    
            case "visual magnitude":
                    
                sorterContacts = order.ToLower() == "desc"
                    ? sorterContacts.OrderByDescending(c => c.Magnitude ?? string.Empty).ToList()
                    : sorterContacts.OrderBy(c => c.Magnitude ?? string.Empty).ToList();
                break;
                    
            case "diameter:":
                    
                sorterContacts = order.ToLower() == "desc"
                    ? sorterContacts.OrderByDescending(c => c.AngularSize ?? string.Empty).ToList()
                    : sorterContacts.OrderBy(c => c.AngularSize ?? string.Empty).ToList();
                break;
                    
            case "burnham's code":
                    
                sorterContacts = order.ToLower() == "desc"
                    ? sorterContacts.OrderByDescending(c => c.Burnham ?? string.Empty).ToList()
                    : sorterContacts.OrderBy(c => c.Burnham ?? string.Empty).ToList();
                break;
                    
            case "remarks":
                    
                sorterContacts = order.ToLower() == "desc"
                    ? sorterContacts.OrderByDescending(c => c.Remarks ?? string.Empty).ToList()
                    : sorterContacts.OrderBy(c => c.Remarks ?? string.Empty).ToList();
                break;
            
            default:
                throw new ArgumentException($"Invalid sorting field: {data}");
        }

        return sorterContacts.Take(maxResults).ToList();
    }
}