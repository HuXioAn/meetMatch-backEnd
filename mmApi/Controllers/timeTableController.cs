using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mmApi.Interface;
using mmApi.Model;
using SQLitePCL;

namespace mmApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class timeTableController : ControllerBase {

    private readonly timeTableDb _context;

    public timeTableController(timeTableDb context){
        _context = context;
    } 

    [HttpPost]
    public createTimeTableReply createTimeTable(createTimeTableRequest request){

        var reply = new createTimeTableReply();

        //create new table

        var newTable = new timeTable{
            meetingName = request.meetingName,
            dateSelection = request.dateSelection,
            timeRange = request.timeRange,
            maxCollaborator = request.maxCollaborator,
            email = request.email,
            state = tableState.Initiated,
            tableManageToken = "mtoken123445566777kjhxcvjkfgvjhxcgvjx",
            tableVisitToken = "vToken1234sdifghjskdfgvjskdgvjkd",
            existingSelection = new Selection[0]
        };

        

        
            _context.timeTables.Add(newTable);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                //update fail
                
                
            }
            
        


        return reply;
    }

    [HttpGet("{visitToken}")]
    public visitTimeTableReply visitTimeTable(string visitToken){
        var reply = new visitTimeTableReply();
        //visit new table
        try
        {
            var table = _context.timeTables.Single(t => t.tableVisitToken == visitToken);
            reply.state = 0;
            reply.meetingName = table.meetingName;
            reply.dateSelection = table.dateSelection;
            reply.timeRange = table.timeRange;
            reply.existingSelection = table.existingSelection;
        }
        catch (System.Exception)
        {
            
            reply.state = 1;
        }

        return reply;
    }

    [HttpPost("update/{visitToken}")]
    public stateReply updateTimeTable(string visitToken, updateTimeTableRequest request){
        var reply = new stateReply();


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