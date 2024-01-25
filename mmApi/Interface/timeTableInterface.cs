using mmApi.Model;
namespace mmApi.Interface;


public class createTimeTableRequest{
    public string meetingName { get; set; }
    public DateTime[] dateSelection { get; set; }
    public int[] timeRange { get; set; } 
    public int maxCollaborator { get; set; }
    public string? email { get; set; }
}

public class createTimeTableReply{
    public int state { get; set; }
    public string tableVisitToken { get; set; }
    public string tableManageToken { get; set; }
}




public class visitTimeTableReply{
    public int state { get; set; }
    public string meetingName { get; set; }
    public DateTime[] dateSelection { get; set; }
    public int[] timeRange { get; set; } 

    public Selection[] existingSelection { get; set; }
}


public class updateTimeTableRequest{
    public Selection selection { get; set; }
}


public class manageTimeTableRequest{
    public string meetingName { get; set; }
    public DateTime[] dateSelection { get; set; }
    public int[] timeRange { get; set; } 
    public int maxCollaborator { get; set; }
    public string? email { get; set; }
}

public class manageTimeTableReply{
    public int state { get; set; }
    public string tableVisitToken { get; set; }
    public string tableManageToken { get; set; }
}


public class resultTimeTableReply{
    public int state { get; set; }

    public Selection[] existingSelection { get; set; }
    public Selection recommendedResult { get; set; }
}






