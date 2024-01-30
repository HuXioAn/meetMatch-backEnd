using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mmApi.Interface;
using mmApi.Model;
using SQLitePCL;

namespace mmApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class timeTableController : ControllerBase {

    const int MAX_MEETINGNAME_LENGTH = 128;
    const int MAX_DATE_SELECTION = 10;
    const int MAX_COLLABORATOR = 30;
    const int MAX_EMAIL_LENGTH = 50;
    const int MAX_COLOR_LENGTH = 15;
    const int MAX_SLOT_NUM = 50;

    private readonly timeTableDb _context;

    public timeTableController(timeTableDb context){
        _context = context;
    } 

    [HttpPost]
    public createTimeTableReply createTimeTable(createTimeTableRequest request){

        var reply = new createTimeTableReply();
        //request validate
        int validate = 0;
        do{
            if(request.meetingName.Length < 1 || 
                request.meetingName.Length > MAX_MEETINGNAME_LENGTH ||
                string.IsNullOrWhiteSpace(request.meetingName)){
                break;
            }

            if(request.dateSelection.Length < 1 || 
                request.dateSelection.Length > MAX_DATE_SELECTION){
                    break;
            }

            if(request.timeRange.Length != 2 || 
                request.timeRange[0] > request.timeRange[1] ||
                request.timeRange[0] < 0 ||
                request.timeRange[1] > 24 ){
                    break;
            }

            if(request.maxCollaborator < 2 ||
                request.maxCollaborator > MAX_COLLABORATOR){
                    break;
            }

            if(request.email != null){
                if(request.email.Length > MAX_EMAIL_LENGTH)break;
            }

            validate = 1;
        }while(false);

        if(validate == 0){
            //didn't pass
            reply.state = -1;
            return reply;
        }

        //create new table
        var guid = Guid.NewGuid().ToString("N");

        var newTable = new timeTable{
            meetingName = request.meetingName,
            dateSelection = request.dateSelection,
            timeRange = request.timeRange,
            maxCollaborator = request.maxCollaborator,
            email = request.email,
            state = tableState.Initiated,
            //TODO: token generate
            tableVisitToken = "v" + guid[0..16],
            tableManageToken = "m" + guid,
            
            existingSelection = new Selection[0]
        };

        
        _context.timeTables.Add(newTable);

        try
        {
            _context.SaveChanges();

            //success
            reply.state = 0;
            reply.tableVisitToken = newTable.tableVisitToken;
            reply.tableManageToken = newTable.tableManageToken;
        }
        catch (DbUpdateException e)
        {
            //update fail
            reply.state = 1;
            reply.tableVisitToken = "[!] Saving Fail.";
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
            reply.meetingName = "[!] Unable to find the time table.";
        }

        return reply;
    }

    [HttpPost("update/{visitToken}")]
    public stateReply updateTimeTable(string visitToken, updateTimeTableRequest request){
        var reply = new stateReply();
        timeTable table;

        try
        {
            table = _context.timeTables.Single(t => t.tableVisitToken == visitToken);
            if(table.state > tableState.Filling) throw new Exception();
        }
        catch (System.Exception)
        {
            
            reply.state = 1;
            return reply;
        }


        //request validate
        int validate = 0;
        do{

            if(string.IsNullOrWhiteSpace(request.selection.color) ||
                request.selection.color.Length > MAX_COLOR_LENGTH){
                    break;
                }

            if(request.selection.slots.Length < 1 || 
                request.selection.slots.Length > MAX_SLOT_NUM){
                    break;
                }

            // foreach (var slot in request.selection.slots)
            // {
            //     if()
            // }

            validate = 1;
        }while(false);

        if(validate == 0){
            //didn't pass
            reply.state = -1;
            return reply;
        }

        //update the table
        try
        {
            //table.existingSelection.Append(request.selection);
            var selection = new Selection[table.existingSelection.Length + 1];
            table.existingSelection.CopyTo(selection,0);
            selection[table.existingSelection.Length] = request.selection;
            table.existingSelection = selection;
            if(table.existingSelection.Length == table.maxCollaborator)table.state = tableState.Full;
            _context.SaveChanges();

            reply.state = 0;

        }
        catch (System.Exception)
        {
            reply.state = 1;
        }
        
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