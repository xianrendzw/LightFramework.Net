using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.IO;

namespace LightFramework.Util
{
    public static class SmtpMailHelper
    {
        /// <summary>
        /// 发送邮件可带附件，System.Net.Mail命名空间
        /// </summary>
        /// <param name="host">邮件服务器地址</param>
        /// <param name="port">邮件服务器端口</param>
        /// <param name="from">发件人邮箱地址</param>
        /// <param name="fromPassword">发件人邮箱密码</param>
        /// <param name="fromDisplayName">发件人名字</param>         
        /// <param name="to">收件人邮箱地址</param>
        /// <param name="toDisplayName">收件人名字</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param> 
        /// <param name="attachmentFiles">附件文件名集合</param>
        /// <returns>成功返回true，否则返回false</returns>
        public static bool Send(string host, int port, string from, string fromPassword, string fromDisplayName,
            List<ToMailAddress> toMailAddress, string subject, string body, out string desc, params string[] attachmentFileNames)
        {
            if (toMailAddress == null ||
                toMailAddress.Count == 0)
            {
                desc = "收件人邮箱地址为空";
                return false;
            }
            try
            {
                SmtpClient smtpClient = new SmtpClient(host);
                smtpClient.Timeout = 60000;//获取或设置一个值，该值指定同步 Send 调用的超时时间。
                smtpClient.UseDefaultCredentials = true;//获取或设置 Boolean 值，该值控制 DefaultCredentials 是否随请求一起发送。
                smtpClient.Credentials = new NetworkCredential(from, fromPassword);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Port = port;
                //smtpClient.EnableSsl = true;

                MailMessage message = new MailMessage();
                //message.From = new MailAddress(from, fromDisplayName, Encoding.UTF8); //.net4以前版本使用
                message.From = new MailAddress(from, fromDisplayName);
                //message.Sender = new MailAddress(from, fromDisplayName);
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;

                message.SubjectEncoding = Encoding.UTF8;
                message.Subject = subject;

                message.BodyEncoding = Encoding.UTF8;
                message.Body = body;

                SetToMailAddress(toMailAddress, message);
                SetAttachments(attachmentFileNames, message);

                smtpClient.Send(message);
                desc = "发送成功";

                return true;
            }
            catch (Exception ex)
            {
                desc = ex.Message;

                return false;
            }
        }

        /// <summary>
        /// 发送邮件可带附件，System.Net.Mail命名空间
        /// </summary>
        /// <param name="host">邮件服务器地址</param>
        /// <param name="port">邮件服务器端口</param>
        /// <param name="from">发件人邮箱地址</param>
        /// <param name="fromPassword">发件人邮箱密码</param>
        /// <param name="fromDisplayName">发件人名字</param>         
        /// <param name="to">收件人邮箱地址</param>
        /// <param name="toDisplayName">收件人名字</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param> 
        /// <param name="attachmentFiles">附件文件名集合</param>
        /// <returns>成功返回true，否则返回false</returns>
        public static bool Send(string host, int port, string from, string fromPassword, string fromDisplayName,
            string to, string toDisplayName, string subject, string body, out string desc, params string[] attachmentFileNames)
        {
            List<ToMailAddress> toMailAddress =
                new List<ToMailAddress> { new ToMailAddress { Address = to, DisplayName = toDisplayName } };

            return Send(host, port, from, fromPassword, fromDisplayName, toMailAddress, subject, body, out desc, attachmentFileNames);
        }

        private static void SetAttachments(string[] attachmentFileNames, MailMessage message)
        {
            if (attachmentFileNames == null ||
                attachmentFileNames.Length == 0)
            {
                return;
            }

            foreach (string attachmentFileName in attachmentFileNames)
            {
                if (string.IsNullOrEmpty(attachmentFileName) ||
                    attachmentFileName.Trim().Length == 0)
                {
                    continue;
                }

                Attachment attachment = new Attachment(attachmentFileName);
                message.Attachments.Add(attachment);
            }
        }

        private static void SetToMailAddress(List<ToMailAddress> toMailAddress, MailMessage message)
        {
            toMailAddress.ForEach(mailAddr =>
                message.To.Add(new MailAddress(mailAddr.Address, mailAddr.DisplayName, Encoding.UTF8)));
        }
    }

    public class ToMailAddress
    {
        public string Address { get; set; }

        public string DisplayName { get; set; }
    }
}