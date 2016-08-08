using Google.Apis.Gmail.v1.Data;
using kt.GmailWeb.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kt.GmailWeb.Core.Util
{
    public class GmailParser
    {
        public static Email FromGmailApi(Message message)
        {
            return new Email
            {
                MailId = message.Id,
                Subject = GetHeaderValue(message.Payload.Headers, "Subject"),
                From = GetHeaderValue(message.Payload.Headers, "From"),
                To = GetHeaderValue(message.Payload.Headers, "To"),
                Snippet = message.Snippet,
                SendDate = message.InternalDate == null ? null : new DateTime?(new DateTime(DateTime.Now.Ticks - message.InternalDate.Value)),
                Body = message.Payload != null && message.Payload.Parts != null ? GetContent(message.Payload.Parts[0].Body.Data) : string.Empty,
                BodyHtml = message.Payload != null && message.Payload.Parts != null ? GetContent(message.Payload.Parts[1].Body.Data) : string.Empty
            };
        }

        private static string GetContent(string data)
        {
            if (data != null)
            {
                var temp = data.Replace('-', '+').Replace('_', '/');
                return Encoding.UTF8.GetString(Convert.FromBase64String(temp));
            }
            return string.Empty;
        }

        private static string GetHeaderValue(IList<MessagePartHeader> headers, string name)
        {
            if (headers != null && headers.Any(i => i.Name == name))
            {
                return headers.FirstOrDefault(i => i.Name == name).Value;
            }
            return string.Empty;
        }
    }
}
