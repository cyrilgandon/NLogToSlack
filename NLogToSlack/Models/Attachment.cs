using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NLogToSlack.Models
{
    /// <summary>
    /// https://api.slack.com/docs/attachments
    /// It is possible to create more richly-formatted messages using Attachments.
    /// Attachments can be added to messages in different ways:
    /// For Incoming Webhooks, send a regular payload, but include an attachments array, where each element is a hash containing an attachment.
    /// For the Web API, include an attachments property, containing a JSON-encoded array of attachment hashes.
    /// For Hammock, pass an array of attachment hashes.
    /// </summary>
    [DataContract]
    public class Attachment
    {

        #region Title parameters
        /// <summary>
        /// The title is displayed as larger, bold text near the top of a message attachment
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// By passing a valid URL (optional), the title will be hyperlinked.
        /// </summary>
        [DataMember(Name = "title_link")]
        public string TitleLink { get; set; }
        #endregion

        #region Author parameters
        /// <summary>
        /// Small text used to display the author's name.
        /// </summary>
        [DataMember(Name = "author_name")]
        public string AuthorName { get; set; }
        /// <summary>
        /// A valid URL that will hyperlink the AuthorName. Will only work if AuthorName is present.
        /// </summary>
        [DataMember(Name = "author_link")]
        public string AuthorLink { get; set; }
        /// <summary>
        /// A valid URL that displays a small 16x16px image to the left of the AuthorName. Will only work if AuthorName is present.
        /// </summary>
        [DataMember(Name = "author_icon")]
        public string AuthorIcon { get; set; }

        #endregion

        /// <summary>
        /// A plain-text summary of the attachment. This text will be used in clients that don't show formatted text (eg. IRC, mobile notifications) and should not contain any markup.
        /// </summary>
        [DataMember(Name = "fallback")]
        public string Fallback { get; set; }

        /// <summary>
        /// An optional value that can either be one of [good, warning, danger], or any hex color code (eg. #439FE0). This value is used to color the border along the left side of the message attachment.
        /// </summary>
        [DataMember(Name = "color")]
        public string Color { get; set; }

        /// <summary>
        /// This is optional text that appears above the message attachment block.
        /// </summary>
        [DataMember(Name = "pretext")]
        public string PreText { get; set; }

        /// <summary>
        /// This is the main text in a message attachment, and can contain standard message markup. The content will automatically collapse if it contains 700+ characters or 5+ linebreaks, and will display a "Show more..." link to expand the content.
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Fields are defined as an array, and hashes contained within it will be displayed in a table inside the message attachment.
        /// </summary>
        [DataMember(Name = "fields")]
        public readonly List<Field> Fields = new List<Field>();

        /// <summary>
        /// A valid URL to an image file that will be displayed inside a message attachment. We currently support the following formats: GIF, JPEG, PNG, and BMP.
        /// Large images will be resized to a maximum width of 400px or a maximum height of 500px, while still maintaining the original aspect ratio.
        /// </summary>
        [DataMember(Name = "image_url")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// A valid URL to an image file that will be displayed as a thumbnail on the right side of a message attachment. We currently support the following formats: GIF, JPEG, PNG, and BMP.
        /// The thumbnail's longest dimension will be scaled down to 75px while maintaining the aspect ratio of the image. The filesize of the image must also be less than 500 KB.
        /// </summary>
        [DataMember(Name = "thumb_url")]
        public string ThumbUrl { get; set; }
    }
}