using System;
using System.Net.Mail;
using System.Net;
using Microsoft.Office.Interop.Outlook;

namespace Email
{
    public class Email
    {
        //jak poslat email
        public void SendEmail(string[] LinkyEmail, string Odkaz = "")
        {
            try
            {
                //Vytvoří instanci aplikace Outlook
                var outlookApp = new Microsoft.Office.Interop.Outlook.Application();

                foreach (var email in LinkyEmail)
                {

                    // Vytvoří nový e-mail
                    MailItem mailItem = (MailItem)outlookApp.CreateItem(OlItemType.olMailItem);

                    //nastaven9 odesílatele
                    mailItem.To = email;

                    //// Nastaví vlastnosti e-mailu
                    mailItem.Subject = "Změny souboru";
                    //mailItem.Body = "This is a test email sent from a C# application using Outlook.";
                    if(LinkyEmail.Length < 1) return;

                    if (string.IsNullOrWhiteSpace(Odkaz))
                    {
                        //Testovací email
                        mailItem.HTMLBody = "Testovací email <br/> Martin";
                    }
                    else
                        mailItem.HTMLBody = Odkaz;

                    //// Odeslání e-mailu
                    mailItem.Send();
                    Console.WriteLine("Email na "+ email + " byl odeslan");
                }


                //Console.WriteLine("Email sent successfully.");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadKey();
            }
        }

        public void SendEmailClient(string Body)
        {
            //nefunguje nelze ověřit
            try
            {
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587)
                {
                    Credentials = new NetworkCredential("martin.csato@tractebel.engie.com", "m400c.60m400c.60"),
                    EnableSsl = true
                };

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("martin.csato@tractebel.engie.com");
                mailMessage.To.Add("milan.simko@tractebel.engie.com");
                mailMessage.Subject = "Test Email from C#";
                mailMessage.Body = "This is a test email sent from a C# application using SMTP.";
                if(Body != null) 
                    mailMessage.Body = Body;
                mailMessage.IsBodyHtml = true;

                client.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
