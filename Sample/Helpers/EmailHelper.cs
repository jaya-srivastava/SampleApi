using System;
using System.Collections.Generic;
using System.Linq;
//using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using WebMatrix.WebData;
using iPractice.Helpers;
using iPractice.Filters;
using Sample.Models.DTO;
//using iPractice.Repository;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Text;
using MailChimp;
using MailChimp.Types;
using System.Threading.Tasks;

namespace iPractice.Helpers
{
    public static class EmailHelper
    {
        #region SEND EMAIL CUSTOMIZED METHODS

        public static async Task SendMail(MailMessage msg)
        {
            if (ConfigurationManager.AppSettings["EmailSettings"] == "true")
            {
                SmtpClient client = new SmtpClient();
                await client.SendMailAsync(msg);
            }
        }
        public static async Task SendVerifyAccountEmail(string email, string userName, string RoleType)
        {
            MailMessage mailMsg = new MailMessage();
            //mailMsg.From = new MailAddress("kewal2feb@gmail.com");
            mailMsg.To.Add(email);
            mailMsg.Subject = "Email verification for iPracticeMath.com";
            mailMsg.IsBodyHtml = true;
            string MailBody = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/") + "content/emailtemplates/accountverification.html");
            MailBody = MailBody.Replace("#Name#", userName);
            MailBody = MailBody.Replace("##Type##", RoleType);
            MailBody = MailBody.Replace("###ActivateEmail###", ConfigurationManager.AppSettings["cdn1"] + "/membership/verifyaccount?userName=" + userName);
            mailMsg.Body = MailBody;
            //SendMail(mailMsg);

            SmtpClient smtp = new SmtpClient();
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(mailMsg);
        }

        public static async Task SendNewUserEmail(string email, string userName)
        {

            MailMessage mailMsg = new MailMessage();
            mailMsg.To.Add(new MailAddress(email));
            mailMsg.Subject = "Welcome to iPracticeMath.com";
            mailMsg.IsBodyHtml = true;
            string MailBody = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/") + "content/emailtemplates/registrationcongrates.html");
            MailBody = MailBody.Replace("###Name###", userName);
            mailMsg.Body = MailBody;
            await SendMail(mailMsg);
        }

        public static async Task SendResetEmail(string email, string uName,string userName, string resetToken)
        {

            MailMessage mailMsg = new MailMessage();
            mailMsg.To.Add(new MailAddress(email));
            //mailMsg.Body = "Email form iPracticeMath";
            mailMsg.Subject = "Password Reset : iPracticeMath.com";
            mailMsg.IsBodyHtml = true;
            string MailBody = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/") + "content/emailtemplates/ForgotPassword.html");
            MailBody = MailBody.Replace("###Name###", userName);
            MailBody = MailBody.Replace("###UserName###", userName);
            MailBody = MailBody.Replace("###UName###", uName);
            MailBody = MailBody.Replace("###resetToken###", resetToken);
            MailBody = MailBody.Replace("###CDNURL###", ConfigurationManager.AppSettings["cdn1"]);
            mailMsg.Body = MailBody ?? "Email form iPracticeMath";
            await SendMail(mailMsg);
        }

        public static bool AddMailChimpSubscriber(string email, string username, bool updateExisting, bool sendWelcome)
        {
            try
            {
                var mc = new MCApi(ConfigurationManager.AppSettings["MailChimpAPIKey"], true);

                var subscribedId = ConfigurationManager.AppSettings["MailChimpSubscribedUsersListId"];
                var unsubscribedId = ConfigurationManager.AppSettings["MailChimpUnSubscribedUserListId"];
                if (updateExisting)
                {
                    var User = mc.ListMemberInfo(unsubscribedId, new List<string> { email });
                    if (User.Success.Value > 0)
                    {
                        MailChimpUnSubscribe(new List<string> { email }, unsubscribedId);
                    }
                }
                else
                {
                    var User = mc.ListMemberInfo(unsubscribedId, new List<string> { email });
                    if (User.Success.Value == 0)
                    {
                        var SubUser = mc.ListMemberInfo(subscribedId, new List<string> { email });
                        if (SubUser.Success.Value > 0)
                        {
                            return false;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }

                var subscribeOptions =
                    new Opt<List.SubscribeOptions>(
                        new List.SubscribeOptions
                        {
                            DoubleOptIn = false,
                            SendWelcome = sendWelcome,
                            UpdateExisting = updateExisting,
                        });
                var merges = new Opt<List.Merges>(
                     new List.Merges
                    {
                        {"FNAME", username},
                    });
                return mc.ListSubscribe(updateExisting ? subscribedId : unsubscribedId, email, merges, subscribeOptions);
            }
            catch
            {
                return false;
            }
        }

        public static bool MailChimpUnSubscribe(List<string> emails, string ListId)
        {
            try
            {
                var mc = new MCApi(ConfigurationManager.AppSettings["MailChimpAPIKey"], true);

                var UnsubscribeOptions =
                    new Opt<List.UnsubscribeOptions>(
                        new List.UnsubscribeOptions
                        {
                            DeleteMember = true,
                            SendGoodby = false,
                            SendNotify = false
                        });
                mc.ListBatchUnsubscribe(ListId, emails, UnsubscribeOptions);
                return true;
            }
            catch
            {
                return false;
            }
        }

       
        #endregion
    }
}