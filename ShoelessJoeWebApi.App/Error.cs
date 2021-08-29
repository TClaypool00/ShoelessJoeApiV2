using ShoelessJoeWebApi.DataAccess.DataModels;
using System;
using System.Net;
using System.Net.Mail;

namespace ShoelessJoeWebApi.App
{
    public class Error
    {
        public enum ControllerNames
        {
            Users,
            Shoes,
            Post,
            Images,
            Comments,
            Replies,
            States,
            Address,
            Models,
            Friends
        }

        public static void SendErrorMessage(Exception exception, ControllerNames controller)
        {
            var fromAddress = new MailAddress("unhandlederror.shoelessJoe.com");
            var toAddress = new MailAddress(SecretConfig.DevEmail);

            using var client = new SmtpClient(SecretConfig.SMTPHost, SecretConfig.Port);
            var networkCred = new NetworkCredential(SecretConfig.Login, SecretConfig.Password);
            client.Credentials = networkCred;
            client.UseDefaultCredentials = false;
            using var message = new MailMessage(fromAddress, toAddress);
            message.Subject = $"Unhandled Handled Error from {ConvertControllerToString(controller)}";
            message.IsBodyHtml = true;
            message.Body = exception.ToString();

            client.Send(message);
        }

        private static string ConvertControllerToString(ControllerNames controller)
        {
            string controllerName;

            controllerName = controller switch
            {
                ControllerNames.Users => "Users",
                ControllerNames.Shoes => "Shoes",
                ControllerNames.Post => "Post",
                ControllerNames.Images => "Shoe Images",
                ControllerNames.Comments => "Comments",
                ControllerNames.Replies => "Replies",
                ControllerNames.States => "States",
                ControllerNames.Address => "Address",
                ControllerNames.Models => "Models",
                ControllerNames.Friends => "Friends",
                _ => throw new ArgumentException("Not a valid controller name"),
            };
            return controllerName + " Controller";
        }
    }
}
