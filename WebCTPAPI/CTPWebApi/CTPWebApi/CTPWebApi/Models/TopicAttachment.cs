using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTPWebApi.Models
{
    public class TopicAttachment
    {
        [Key]
        public Guid TopicAttachmentId { get; set; }

        [ForeignKey("TopicId")]
        public virtual Topic Topic { get; set; }
        public Guid TopicId { get; set; }
        [Required]
        public string TopicAttachmentFileName { get; set; }
        [Required]
        public byte[] TopicAttachmentFile { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
    }
}