using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class SmtpHelper
    {
        //Inserire delle credenziali per l'utilizzo dell'SMTP
        public static void SendEmail(string receiverAddress, string receiverName, string subject, string body, List<string> ccAddresses, List<Attachment> attachments)
        {
            /*
            string senderAddress = "";
            string senderName = "Esg reporting assistant";
            //CREDENZIALI ED IMPOSTAZIONI SMTP
            SmtpClient client = new SmtpClient("smtp.office365.com", 587)
            {
                Credentials = new NetworkCredential("", ""),
                EnableSsl = true,
                //EnableSsl = false,
                Timeout = 100000
            };

            //COSTRUZIONE EMAIL
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(senderAddress, senderName); //mittente
            msg.To.Add(new MailAddress(receiverAddress, receiverName)); //destinatario

            foreach (string ccAddress in ccAddresses) //Aggiungi indirizzi in copia conoscenza
                msg.CC.Add(ccAddress);

            msg.Subject = subject;  //oggetto
            msg.Body = body;  //corpo

            //Aggiungi allegati
            foreach (Attachment attachment in attachments)
            {
                msg.Attachments.Add(attachment);
            }

            client.Send(msg);  //invio del messaggio costruito
            */
        }

        public static void SendEmail(string receiverAddress, string receiverName, string subject, string body)
        {
            /*
            string senderAddress = "";
            string senderName = "Esg reporting assistant";
            //CREDENZIALI ED IMPOSTAZIONI SMTP
            SmtpClient client = new SmtpClient("smtp.office365.com", 587)
            {
                Credentials = new NetworkCredential("", ""),
                EnableSsl = true,
                //EnableSsl = false,
                Timeout = 100000
            };

            //COSTRUZIONE EMAIL
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(senderAddress, senderName); //mittente
            msg.To.Add(new MailAddress(receiverAddress, receiverName)); //destinatario

            msg.Subject = subject;  //oggetto
            msg.Body = body;  //corpo

            client.Send(msg);  //invio del messaggio costruito
            */
        }

        public static void SendEmail(string receiverAddress, string receiverName, string subject, string body, bool isHtml)
        {
            /*
            string senderAddress = "";
            string senderName = "Esg reporting assistant";
            //CREDENZIALI ED IMPOSTAZIONI SMTP
            SmtpClient client = new SmtpClient("smtp.office365.com", 587)
            {
                Credentials = new NetworkCredential("", ""),
                EnableSsl = true,
                //EnableSsl = false,
                Timeout = 100000
            };

            //COSTRUZIONE EMAIL
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(senderAddress, senderName); //mittente
            msg.To.Add(new MailAddress(receiverAddress, receiverName)); //destinatario

            msg.Subject = subject;  //oggetto

            if (isHtml)
                msg.IsBodyHtml = true; //Trasformare il formato del body in html
            else
                msg.IsBodyHtml = false;

            msg.Body = body;  //corpo


            client.Send(msg);  //invio del messaggio costruito
            */
        }
    }
}
