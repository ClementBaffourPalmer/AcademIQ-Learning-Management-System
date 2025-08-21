using System.ComponentModel.DataAnnotations;

namespace AcademIQ_LMS.Models
{
    public enum ContentType
    {
        PDF,
        Text,
        Video,
        Image,
        Document,
        Other
    }

    public class Content
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        public ContentType Type { get; set; }

        [StringLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [StringLength(100)]
        public string FileName { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;

        public int UploadedById { get; set; }
        public virtual User UploadedBy { get; set; } = null!;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
} 