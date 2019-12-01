using System.ComponentModel.DataAnnotations;

namespace Sindicato_v1.Models.ViewModels
{
    public class RecoveryViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}

