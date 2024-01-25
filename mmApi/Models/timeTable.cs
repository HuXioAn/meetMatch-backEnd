
namespace mmApi.Model;

public class timeTable{
    public string meetingName { get; set; }
    public DateTime[] dateSelection { get; set; }
    public int[] timeRange { get; set; } 
    public int maxCollaborator { get; set; }
    public string? email { get; set; }

    public tableState state { get; set; }
    public string tableVisitToken { get; set; }
    public string tableManageToken { get; set; }

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