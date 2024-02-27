using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace mmApi.Mail;

class mailConfig{
    public string name{set; get;}
    public string address{set; get;}

    public string smtpServerHost{set; get;}
    public int smtpServerPort{set; get;}

    public string password{set; get;}

}

public static class mail{

    static private string configPath = "./mail/mailConfig.json";
    static private string templatePath = "./mail/mailTemplate.html";

    static private mailConfig? config;

    static private string mailTamplate;

    
    static mail(){
        //initiating the resources, only once
        try
        {
            var configText = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<mailConfig>(configText);
            if(config == null){
                throw new Exception("Unable to find config info: " + configPath);
            }

            mailTamplate = File.ReadAllText(templatePath);
        }
        catch (System.Exception e)
        {  
            Console.WriteLine("[!]Error parsing JSON file: {0}, {1}",configPath,e.Message);   
        }

    }

    static public MimeMessage? composeMail(string receiverAddress, string addition){
        if(config == null)return null;

        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(config.name, config.address));
        email.To.Add(new MailboxAddress("User", receiverAddress));

        email.Subject = "MeetMatch - Time Table registered";
        email.Body = new TextPart(MimeKit.Text.TextFormat.Text) { 
            Text = mailTamplate + addition
        };

        return email;
        
    }

    static public int sendMail(MimeMessage mail){
        if(config == null)return -1;
        using (var smtp = new SmtpClient()){
            smtp.Connect(config.smtpServerHost, config.smtpServerPort, false);

            // Note: only needed if the SMTP server requires authentication
            smtp.Authenticate(config.address, config.password);

            smtp.Send(mail);
            smtp.Disconnect(true);
        }

        return 0;
    }



}