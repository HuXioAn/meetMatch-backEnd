using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mmApi.Interface;
using mmApi.Model;
using mmApi.Mail;
using SQLitePCL;

using System.Threading.Tasks;

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
    public async Task<createTimeTableReply> createTimeTable(createTimeTableRequest request){

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

        
        

        try
        {   
            await _context.timeTables.AddAsync(newTable);
            await _context.SaveChangesAsync();

            //success
            reply.state = 0;
            reply.tableVisitToken = newTable.tableVisitToken;
            reply.tableManageToken = newTable.tableManageToken;

            var t = Task.Run(() => {
                try{
                    if(!string.IsNullOrWhiteSpace(newTable.email)){
                    var email = mail.composeMail(newTable.email, 
                    "Meeting Name: " + newTable.meetingName +
                    "\nVisit Link: https://meetmatch.us/table/?vToken="+newTable.tableVisitToken + 
                    "\nManage Link: https://meetmatch.us/manage/?mToken="+newTable.tableManageToken);
                    if(email != null)
                    mail.sendMail(email);
                }
                }catch(Exception e){
                    
                }
            });
            

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
    public async Task<visitTimeTableReply> visitTimeTable(string visitToken){
        var reply = new visitTimeTableReply();
        //visit new table
        try
        {
            var table = await _context.timeTables.SingleAsync<timeTable>(t => t.tableVisitToken == visitToken);
            if(table.state == tableState.Deleted){
                throw new Exception();
            }
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
    public async Task<stateReply> updateTimeTable(string visitToken, updateTimeTableRequest request){
        var reply = new stateReply();
        timeTable table;

        try
        {
            table = await _context.timeTables.SingleAsync<timeTable>(t => t.tableVisitToken == visitToken);
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
            else table.state = tableState.Filling;
            await _context.SaveChangesAsync();

            reply.state = 0;

        }
        catch (System.Exception)
        {
            reply.state = 1;
        }
        
        return reply;
    }

    [HttpPut("manage/{manageToken}")]
    public async Task<manageTimeTableReply> manageTimeTable(string manageToken, manageTimeTableRequest request){
        var reply = new manageTimeTableReply();
        timeTable table;
        
        //query the table
        try
        {
            table = await _context.timeTables.SingleAsync<timeTable>(t => t.tableManageToken == manageToken);
            if(table.state >= tableState.Done) throw new Exception();
        }
        catch (System.Exception)
        {
            
            reply.state = 1;
            return reply;
        }

        //check the request
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

        try
        {
            table.meetingName = request.meetingName;
            table.dateSelection = request.dateSelection;
            table.timeRange = request.timeRange;
            table.maxCollaborator = request.maxCollaborator;
            table.email = request.email;
            table.state = table.state;
            table.existingSelection = table.existingSelection;

            await _context.SaveChangesAsync();
            reply.state = 0;
            reply.tableManageToken = table.tableManageToken;
            reply.tableVisitToken = table.tableVisitToken;

            // try{
            //     if(!string.IsNullOrWhiteSpace(table.email)){
            //     var email = mail.composeMail(table.email, 
            //     "Visit Link: https://meetmatch.us/table/?vToken="+table.tableVisitToken + 
            //     "\nManage Link: https://meetmatch.us/manage/?mToken="+table.tableManageToken);
            //     if(email != null)
            //     mail.sendMail(email);
            // }
            // }catch(Exception e){
                
            // }
        }
        catch (System.Exception)
        {
            reply.state = 1;
        }
        
        return reply;
    }


    [HttpGet("result/{visitToken}")]
    public resultTimeTableReply getResult(string visitToken){
        var reply = new resultTimeTableReply(); reply.state = 1;

        return reply;
    }


    [HttpDelete("{manageToken}")]
    public async Task<stateReply> deleteTimeTable(string manageToken){
        var reply = new stateReply();
        timeTable table;
        
        //query the table
        try
        {
            table = await _context.timeTables.SingleAsync<timeTable>(t => t.tableManageToken == manageToken);
            if(table.state >= tableState.Done) throw new Exception();
        }
        catch (System.Exception)
        {
            
            reply.state = 1;
            return reply;
        }

        try
        {
            table.state = tableState.Deleted;
            await _context.SaveChangesAsync();
            reply.state = 0;
        }
        catch (System.Exception)
        {
            reply.state = 1;
        }

        return reply;
    }


}