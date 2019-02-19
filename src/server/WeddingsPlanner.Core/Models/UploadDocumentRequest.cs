using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WeddingsPlanner.Core.Models
{
    public class UploadDocumentRequest
    {
        [Required]
        public IFormFile File { get; set; }
    }
}