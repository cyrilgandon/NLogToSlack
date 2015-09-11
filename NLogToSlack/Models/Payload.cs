using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace NLogToSlack.Models
{
    /// <summary>
    /// Payload parameter transformed to a JSON string in the POST request to Slack
    /// </summary>
    [DataContract]
    public class Payload
    {
        /// <summary>
        /// Override incoming webhooks default channel. 
        /// A public channel can be specified with "#other-channel", and a Direct Message with "@username".
        /// </summary>
        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        /// <summary>
        /// Override incoming webhook's configured name.
        /// </summary>
        [DataMember(Name = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Override the bot icon
        /// </summary>
        [DataMember(Name = "icon_url")]
        public string IconUrl { get; set; }

        /// <summary>
        /// Override the bot icon
        /// </summary>
        [DataMember(Name = "icon_emoji")]
        public string IconEmoji { get; set; }

        /// <summary>
        /// Simple message that will be posted to the channel.
        /// To create a link in your text, enclose the URL in <> angle brackets.For example: <https://slack.com>
        /// will post a clickable link to https://slack.com.
        /// To display hyperlinked text instead of the actual URL, use the pipe character, as shown in this example: '<https://alert-system.com/alerts/1234|Click here> for details!'
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Display richly-formatted message blocks.
        /// </summary>
        [DataMember(Name = "attachments")]
        public readonly ICollection<Attachment> Attachments = new List<Attachment>();

        /// <summary>
        /// Set the IconUrl attribute if icon look like a Uri, else set the IconEmoji attribute
        /// </summary>
        /// <param name="icon"></param>
        public void SetIcon(string icon)
        {
            Uri uriResult;
            if (Uri.TryCreate(icon, UriKind.Absolute, out uriResult)
                && uriResult.Scheme == Uri.UriSchemeHttp)
            {
                this.IconUrl = icon;
            }
            else
            {
                this.IconEmoji = icon;
            }
        }

        /// <summary>
        /// Create JSON that will be send to Slack
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            var serializer = new DataContractJsonSerializer(typeof(Payload));
            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, this);
                memoryStream.Position = 0;
                using (var reader = new StreamReader(memoryStream))
                {
                    string json = reader.ReadToEnd();
                    return json;
                }
            }
        }

        /// <summary>
        /// Send this payload via a POST request to the given slack Webhook
        /// </summary>
        /// <param name="webHookUrl">The WebhookUrl where Payload will be POST</param>
        public void SendTo(string webHookUrl)
        {
            var json = this.ToJson();
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;
                client.UploadString(webHookUrl, "POST", json);
            }
        }
    }
}

