using System.Net.Mail;

namespace OceanicWorldAirService.Services
{
    public static class MailService
    {
        public static void SendMail(string costumerMail)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("email"),
                Subject = "subject",
                Body = "<h1>Hello</h1>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(costumerMail);

            SmtpClient smtpClient = new();
            smtpClient.Send(mailMessage);
        }
    }
}
