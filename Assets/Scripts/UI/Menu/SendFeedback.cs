using System.Net;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public class SendFeedback : MonoBehaviour
{
    [SerializeField] private InputField text;

    public void Send()
    {
        string smtpServer = "smtp-mail.outlook.com";
        int smtpPort = 587;
        string fromEmail = "peopleplayedavmgame@outlook.com";
        string fromPassword = "DustyStudio10";
        string toEmail = "dustystudio10@gmail.com";
        string subject = "����� � ����";
        string body = text.text;
        var Sender = new MailAddress(fromEmail);
        var smtpClient = new SmtpClient(smtpServer)
        {
            Port = smtpPort,
            Credentials = new NetworkCredential(fromEmail, fromPassword),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage(fromEmail, toEmail)
        {
            Sender = Sender,

            Subject = subject,
            Body = body,
            IsBodyHtml = false,
        };

        smtpClient.Send(mailMessage);
    }
}