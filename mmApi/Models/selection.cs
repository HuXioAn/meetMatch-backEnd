
namespace mmApi.Model;

public class Selection{
    public string color { get; set; }
    public Slot[] slots { get; set; }
}

public struct Slot
{
    public DateTime startTime;
    public DateTime endTime;
    
}

