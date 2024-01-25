using Microsoft.AspNetCore.Mvc;
using mmApi.Interface;

namespace mmApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class timeTableController : ControllerBase {

    [HttpPost]
    public createTimeTableReply createTimeTable(createTimeTableRequest request){

        var reply = new createTimeTableReply();

        //create new table

        return reply;
    }

    [HttpGet("{visitToken}")]
    public visitTimeTableReply visitTimeTable(string visitToken){
        var reply = new visitTimeTableReply();
        //visit new table

        return reply;
    }

    [HttpPost("update/{visitToken}")]
    public int updateTimeTable(string visitToken, updateTimeTableRequest request){
        int state = 0;


        return state;
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


    [HttpDelete("manageToken")]
    public int deleteTimeTable(string manageToken){
        int state = 0;


        return state;
    }


}