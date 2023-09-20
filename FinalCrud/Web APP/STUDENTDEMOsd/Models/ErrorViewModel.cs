using System.Security.Policy;

namespace StudentCrudApp.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }



        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    }
}
