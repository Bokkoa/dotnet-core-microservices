using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using ShopServices.Messenger.Email.SendGridLibrary.Interface;
using ShopServices.Messenger.Email.SendGridLibrary.Model;

namespace ShopServices.Messenger.Email.SendGridLibrary.Implement
{
    public class SendGridSend : ISendGridSend
    {
        public async  Task<(bool result, string errorMessage)> SendMail(SendGridData data)
        {
            try{

                var sendGridClient = new SendGridClient(data.SendGridApiKey);
                var receiver = new EmailAddress(data.EmailReceiver, data.NameReceiver);
                var emailTitle = data.Title;
                var sender = new EmailAddress("don.konejo@example.com", "Don Conejo");
                var messageContent =  data.Content;

                var objMessage = MailHelper.CreateSingleEmail(sender, receiver, emailTitle, messageContent, "<h1> THIS IS AN HTML </h1>");

                await sendGridClient.SendEmailAsync(objMessage);

                return ( true, null);
            }catch( Exception err ){
                return ( false, err.Message );
            }

        }
    }
}