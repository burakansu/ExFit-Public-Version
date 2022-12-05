using ObjectLayer;
using System.Net.Mail;

namespace BussinesLayer
{
    public class MailManager
    {
        public string SendMail(string CompanyName, string MailText, List<ObjMember> objMembers = null, List<ObjUser> objUsers = null)
        {
            MailMessage Message = new MailMessage();
            Message.From = new MailAddress("exfitgymmanager@gmail.com");
            if (objMembers != null)
            {
                foreach (var item in objMembers)
                {
                    Message.To.Add(item.Mail);
                }
            }
            if (objUsers != null)
            {
                foreach (var item in objUsers)
                {
                    Message.To.Add(item.Mail);
                }
            }
            Message.Subject = CompanyName;
            Message.Body = MailText;

            SmtpClient SMTP = new SmtpClient();
            SMTP.Credentials = new System.Net.NetworkCredential("exfitgymmanager@gmail.com", "Private");
            SMTP.Port = 587;
            SMTP.Host = "Private";
            SMTP.EnableSsl = true;
            object userState = Message;

            try
            {
                SMTP.SendAsync(Message, (object)Message);
                return "Mail Gönderildi";
            }
            catch (SmtpException ex)
            {
                return "Mail Gönderilemedi";
            }
        }
    }
}
