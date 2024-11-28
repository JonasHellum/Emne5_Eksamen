namespace Emne5_Eksamen;

public class MessierCatalogue
{
    private List<Messier> messiers = new List<Messier>();

    #region Question 1
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
                    Console.WriteLine($"Error on line: \"{lineNr + 1}\": Not a valid format.");
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
    
    public static string DisplayAll(List<Messier> messiers)
    {
        if (messiers.Count == 0)
            return "No info found.";
    
        if (messiers.Count == 1)
            return Display(messiers[0]);
        
        return string.Join("\n", messiers.Select(c => Display(c)));
    }
    
    #endregion
    
    #region Question 1.1
    
    // Linear Search
    private bool Find(string field, string value)
    {
        field = field.ToLower();
        value = value.ToLower();

        // Looping through the fields in (Messier) to check if value exists until the
        // remaining characters is the same as value (searching for) then ends
        for (int i = 0; i <= field.Length - value.Length; i++)
        {
            bool found = true;
            
            // Checking each character in field that starts at current i index against value[j]
            for (int j = 0; j < value.Length; j++)
            {
                if (field[i + j] != value[j])
                {
                    found = false;
                    break;
                }
            }

            if (found)
            {
                return true;
            }
        }

        return false;
    }
    
    public List<Messier> Search(string value, int maxResults = 10)
    {
        var results = new List<Messier>();

        // Loops through list of messiers and performs a linear search in them.
        foreach (var m in messiers)
        {
            if (m != null &&
                Find(m.Name, value) ||
                Find(m.NGC, value) ||
                Find(m.Constellation, value) ||
                Find(m.Class, value) ||
                Find(m.RightAscension, value) ||
                Find(m.Declination, value) ||
                Find(m.Magnitude, value) ||
                Find(m.AngularSize, value) ||
                Find(m.Burnham, value) ||
                Find(m.Remarks, value))
            {
                results.Add(m);
                
                if (results.Count >= maxResults)
                {
                    break;
                }
            }
        }
        
        return results.ToList();
    }
    #endregion
    
    #region Question 1.2
    
    private bool IsValidField(string field)
    {
        var validFields = new HashSet<string>
        {
            "messier catalogue number",
            "ngc (or other) catalogue number",
            "constellation",
            "object class",
            "right ascension",
            "declination",
            "visual magnitude",
            "diameter",
            "burnham's code",
            "remarks"
        };
    
        return validFields.Contains(field.ToLower());
    }
    
    private string GetFieldValue(Messier messier, string field)
    {
        switch (field.ToLower())
        {
            case "messier catalogue number":
                return messier.Name;
            case "ngc (or other) catalogue number":
                return messier.NGC;
            case "constellation":
                return messier.Constellation;
            case "object class":
                return messier.Class;
            case "right ascension":
                return messier.RightAscension;
            case "declination":
                return messier.Declination;
            case "visual magnitude":
                return messier.Magnitude;
            case "diameter":
                return messier.AngularSize;
            case "burnham's code":
                return messier.Burnham;
            case "remarks":
                return messier.Remarks;
            default:
                Console.WriteLine($"Invalid sorting field: {field}");
                return null;
        }
    }
    
    
    // Compares the values and returns less than zero, zero or greater than zero.
    private int CompareValues(string candidateValue, string currentValue)
    {
        var alphabetFirst = ExtractAlphabeticalPart(candidateValue);
        var alphabetSecond = ExtractAlphabeticalPart(currentValue);

        int alphabeticalComparison = string.Compare(alphabetFirst, alphabetSecond, StringComparison.OrdinalIgnoreCase);

        if (alphabeticalComparison == 0)
        {
            decimal numericFirst = ExtractNumericPart(candidateValue);
            decimal numericSecond = ExtractNumericPart(currentValue);
            
            return numericFirst.CompareTo(numericSecond);
        }

        return alphabeticalComparison;
    }
    
    
    private decimal ExtractNumericPart(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return 0;
        }
        
        var match = System.Text.RegularExpressions.Regex.Match(value, @"\d+(\.\d+)?");
        return match.Success
            ? decimal.Parse(match.Value)
            : 0;
    }
    
    private string ExtractAlphabeticalPart(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return string.Empty;
        }

        var match = System.Text.RegularExpressions.Regex.Match(name, @"^[A-Za-z]+");
        return match.Success 
            ? match.Value 
            : string.Empty;
    }
    
    // Checks and swaps Messier objects.
    private bool ShouldSwap(Messier candidateElement, Messier currentElement, string field, string order)
    {
        bool isDescending = order.ToLower() == "descending";

        
        // Value in the candidate element from the inner loop (list[j])
        string candidateValue = GetFieldValue(candidateElement, field);
        // Value in the current minimum or maximum element (list[minOrMaxIndex])
        string currentValue = GetFieldValue(currentElement, field);
        
        int comparison = CompareValues(candidateValue, currentValue);

        return isDescending 
            ? comparison > 0  // Swap if descending and candidateElement is > currentElement
            : comparison < 0; // Swap if ascending and candidateElement is < currentElement
                            // If neither of these conditions are met the values are in correct order and doesn't swap.
    }
    
    private void SelectionSort(List<Messier> list, string field, string order)
    {
        // Loops over the list of messier objects.
        for (int i = 0; i < list.Count - 1; i++)
        {
            int minOrMaxIndex = i;

            // Loops over values to find the correct value to place at index i
            for (int j = i + 1; j < list.Count; j++)
            {
                // Checks if the values should be swapped
                bool condition = ShouldSwap(list[j], list[minOrMaxIndex], field, order);

                // update minOrMaxIndex if current value should be before current MinOrMaxIndex value
                if (condition)
                    minOrMaxIndex = j;
            }
            
            // Swap values if value at index i is not the same as minOrMaxIndex
            if (i != minOrMaxIndex)
            {
                (list[i], list[minOrMaxIndex]) = (list[minOrMaxIndex], list[i]);
            }
        }
    }
    
    public List<Messier> Sort(string field, string order = "ascending", int maxResults = 20)
    {
        if (!IsValidField(field))
        {
            Console.WriteLine($"Invalid sorting field: {field}");
            return new List<Messier>();
        }
        
        var sortedMessiers = messiers.Where(c => c != null).ToList();
        SelectionSort(sortedMessiers, field, order);
        return sortedMessiers.Take(maxResults).ToList();
    }
    #endregion

    #region Question 3
    public MessierCatalogue Filter(string field, string value)
    {
        var filteredMessiers = messiers.Where(m => m != null).ToList();
        
        switch (field.ToLower())
        {
            case "messier catalogue number":
                filteredMessiers =
                    filteredMessiers.Where(m => m.Name.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            case "ngc (or other) catalogue number":
                filteredMessiers =
                    filteredMessiers.Where(m => m.NGC.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            case "constellation":
                filteredMessiers = 
                    filteredMessiers.Where(m => m.Constellation.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            case "object class":
                filteredMessiers =
                    filteredMessiers.Where(m => m.Class.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            case "right ascension":
                filteredMessiers = 
                    filteredMessiers.Where(m => m.RightAscension.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            case "declination":
                filteredMessiers =
                    filteredMessiers.Where(m => m.Declination.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            case "visual magnitude":
                filteredMessiers =
                    filteredMessiers.Where(m => m.Magnitude.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            case "diameter":
                filteredMessiers =
                    filteredMessiers.Where(m => m.AngularSize.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            case "burnham's code":
                filteredMessiers = 
                    filteredMessiers.Where(m => m.Burnham.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            case "remarks":
                filteredMessiers = 
                    filteredMessiers.Where(m => m.Remarks.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            default:
                Console.WriteLine($"Invalid sorting field: {field}");
                return new MessierCatalogue { messiers = new List<Messier>() };
        }
        
        return new MessierCatalogue { messiers = filteredMessiers };
    }
    #endregion
    
}