
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mmApi.Model;

public class timeTable{
    public string meetingName { get; set; }

    // [NotMapped]
    public DateTime[] dateSelection { get; set; }
    [NotMapped]
    public int[] timeRange { get; set; } 
    public int maxCollaborator { get; set; }
    public string? email { get; set; }

    public tableState state { get; set; }
    [Key]
    public string tableVisitToken { get; set; }
    public string tableManageToken { get; set; }

    [NotMapped]
    public Selection[] existingSelection { get; set; }
}


public enum tableState
{
    Initiated = 0,
    Filling = 1,
    Full = 2,
    Done = 3,
    Deleted = 4
}