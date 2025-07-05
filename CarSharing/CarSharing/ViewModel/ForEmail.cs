using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Windows;
using CarSharing.Model;

namespace CarSharing.ViewModel
{
    internal class ForEmail
    {

        public static void SendEmail(string email, string subject, string body)
        {
            try
            {
                // Настройки SMTP-сервера (для Gmail)
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587; // Порт для Gmail
                bool enableSsl = true;

                // Ваши учетные данные
                string senderEmail = "carsharingwa@gmail.com";
                string senderPassword = "lplz ozzs dfyw yhlg";

                // Получатель письма
                string recipientEmail = GlobalData.Email;

                // Создание письма
                MailMessage mail = new MailMessage(senderEmail, recipientEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false // Можно установить true, если тело письма в HTML
                };

                // Настройка SMTP-клиента
                SmtpClient smtpClient = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = enableSsl,
                };

                // Отправка письма
                smtpClient.Send(mail);

                //MessageBox.Show("Письмо успешно отправлено!");
            }
            catch (Exception ex)
            {
                //MessBox.ShowCustomMessageBox("Error");
            }
        }
    }
}
