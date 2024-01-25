using Microsoft.AspNetCore.Mvc;
using mmApi.Interface;
using static System.Console;

namespace mmApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class timeTableController : ControllerBase {

    [HttpPost]
    public createTimeTableReply createTimeTable(createTimeTableRequest request){

        var reply = new createTimeTableReply{
            state = 1,
            tableVisitToken = "visit!",
            tableManageToken = "mmmmmanage!"
        };

        //create new table
        Console.WriteLine(request.meetingName);
        WriteLine(request.email);
        WriteLine(request.dateSelection[0].ToLongDateString());

        

        return reply;
    }

    [HttpGet("{visitToken}")]
    public visitTimeTableReply visitTimeTable(string visitToken){
        var reply = new visitTimeTableReply();
        //visit new table

        if(visitToken.Equals("12345")){
            reply.state = 0;
            reply.meetingName = "g8";
            reply.dateSelection = new DateTime[]{DateTime.Today, DateTime.Today.AddDays(2)};
            reply.timeRange = new int[]{8, 15};
            reply.existingSelection = new Model.Selection[]{
                new Model.Selection{
                    color = "red",
                    slots = new Model.Slot[]{
                        new Model.Slot{
                            startTime = DateTime.Today,
                            endTime = DateTime.Now.AddMinutes(60)
                        },
                        new Model.Slot{
                            startTime = DateTime.Now.AddMinutes(80),
                            endTime = DateTime.Now.AddMinutes(100)
                        }
                    }
                }
            };
        }

        return reply;
    }

    [HttpPost("update/{visitToken}")]
    public stateReply updateTimeTable(string visitToken, updateTimeTableRequest request){
        var reply = new stateReply();

        WriteLine(visitToken);
        WriteLine(request.selection.color);
        WriteLine(request.selection.slots[0].startTime.ToLongDateString());

        return reply;
    }

    [HttpPut("manage/{manageToken}")]
    public manageTimeTableReply manageTimeTable(string manageToken, manageTimeTableRequest request){
        var reply = new manageTimeTableReply();

        return reply;
    }


    [HttpGet("result/{visitToken}")]
    public resultTimeTableReply getResult(string visitToken){
        var reply = new resultTimeTableReply();

        return reply;
    }


    [HttpDelete("{manageToken}")]
    public stateReply deleteTimeTable(string manageToken){
        var reply = new stateReply();


        return reply;
    }


}