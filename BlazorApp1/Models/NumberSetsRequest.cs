namespace BlazorApp1.Models;

public class NumberGroup
{
    public int min { get; set; }
    public int max { get; set; }
    public int numbersPerGroup { get; set; }
    public int divergence { get; set; }
    public bool sumCheck { get; set; }
    public bool oeCheck { get; set; }
}

public class Root
{
    public List<NumberGroup> numberGroups { get; set; } = new List<NumberGroup>();
    public int sets { get; set; }
}
