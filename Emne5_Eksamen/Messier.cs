using CsvHelper.Configuration.Attributes;

namespace Emne5_Eksamen;

public class Messier
{
    [Name("Name")]
    public string? Name { get; set; }
    
    [Name("NGC")]
    public string? NGC { get; set; }
    
    [Name("Constellation")]
    public string? Constellation { get; set; }
    
    [Name("Class")]
    public string? Class { get; set; }
    
    [Name("Right Ascension")]
    public string? RightAscension { get; set; }
    
    [Name("Declination")]
    public string? Declination { get; set; }
    
    [Name("Magnitude")]
    public string? Magnitude { get; set; }
    
    [Name("Angular size")]
    public string? AngularSize { get; set; }
    
    [Name("Burnham")]
    public string? Burnham { get; set; }
    
    [Name("Remarks")]
    public string? Remarks { get; set; }

    public Messier(string name, string ngc, string constellation, string classInMessier, string rightAscension,
        string declination, string magnitude, string angularSize, string burnham, string remarks)
    {
        Name = name;
        NGC = ngc;
        Constellation = constellation;
        Class = classInMessier;
        RightAscension = rightAscension;
        Declination = declination;
        Magnitude = magnitude;
        AngularSize = angularSize;
        Burnham = burnham;
        Remarks = remarks;
    }
    
    public override string ToString()
    {
        return $"Name: {Name}, NGC: {NGC}, Constellation: {Constellation}, Class: {Class}, Right ascension: {RightAscension}, Declination: {Declination}, " +
               $"Magnitude: {Magnitude}, Angular size {AngularSize}, Burnham: {Burnham}, Remarks: {Remarks}";
    }
}