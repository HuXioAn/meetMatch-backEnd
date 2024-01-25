
namespace mmApi.Model;

public class Selection{
    public string color { get; set; }
    public Slot[] slots { get; set; }
}

public class Slot
{
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }
    
}

