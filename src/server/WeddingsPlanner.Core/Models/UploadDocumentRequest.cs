using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.Core.Models
{
    public class UploadDocumentRequest
    {
        [Required]
        public IFormFile File { get; set; }
    }
}