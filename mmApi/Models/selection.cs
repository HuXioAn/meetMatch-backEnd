
using Microsoft.EntityFrameworkCore;

namespace mmApi.Model;

// [Keyless]
public class Selection{
    public string color { get; set; }
    public Slot[] slots { get; set; }
}

// [Keyless]
public class Slot
{
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }
    
}

