using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace LightFramework.Util
{
    public static class SmtpMailHelper
    {
        /// <summary>
        /// �����ʼ��ɴ�������System.Net.Mail�����ռ�
        /// </summary>
        /// <param name="host">�ʼ���������ַ</param>
        /// <param name="from">�����������ַ</param>
        /// <param name="fromPassword">��������������</param>
        /// <param name="fromDisplayName">����������</param>         
        /// <param name="to">�ռ��������ַ</param>
        /// <param name="toDisplayName">�ռ�������</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="body">�ʼ�����</param> 
        /// <param name="attachmentFiles">�����ļ�������</param>
        /// <returns>�ɹ�����true�����򷵻�false</returns>
        public static bool Send(string host, string from, string fromPassword, string fromDisplayName,
            List<ToMailAddress> toMailAddress, string subject, string body,out string desc, params string[] attachmentFileNames)
        {
            if (toMailAddress == null ||
                toMailAddress.Count == 0)
            {
                desc = "�ռ��������ַΪ��";
                return false;
            }
            try
            {
                SmtpClient smtpClient = new SmtpClient(host);

                smtpClient.Timeout = 60000;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential(from, fromPassword);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.Port = 25;
                //smtpClient.EnableSsl = true;

                MailMessage message = new MailMessage();

                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                //message.From = new MailAddress(from, fromDisplayName, Encoding.UTF8); //.net4��ǰ�汾ʹ��
                message.From = new MailAddress(from);
                //message.Sender = new MailAddress(from, fromDisplayName);
                message.IsBodyHtml = true;
                message.Subject = subject;
                message.Body = body;
                message.Priority = MailPriority.Normal;

                SetToMailAddress(toMailAddress, message);
                SetAttachments(attachmentFileNames, message);

                smtpClient.Send(message);
                desc = "���ͳɹ�";

                return true;
            }
            catch (Exception ex)
            {
                desc = ex.Message;

                return false;
            }
        }

        /// <summary>
        /// �����ʼ��ɴ�������System.Net.Mail�����ռ�
        /// </summary>
        /// <param name="host">�ʼ���������ַ</param>
        /// <param name="from">�����������ַ</param>
        /// <param name="fromPassword">��������������</param>
        /// <param name="fromDisplayName">����������</param>         
        /// <param name="to">�ռ��������ַ</param>
        /// <param name="toDisplayName">�ռ�������</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="body">�ʼ�����</param> 
        /// <param name="attachmentFiles">�����ļ�������</param>
        /// <returns>�ɹ�����true�����򷵻�false</returns>
        public static bool Send(string host, string from, string fromPassword, string fromDisplayName,
            string to, string toDisplayName, string subject, string body, out string desc, params string[] attachmentFileNames)
        {
            List<ToMailAddress> toMailAddress =
                new List<ToMailAddress> { new ToMailAddress { Address = to, DisplayName = toDisplayName } };

            return Send(host, from, fromPassword, fromDisplayName, toMailAddress, subject, body, out desc,attachmentFileNames);
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