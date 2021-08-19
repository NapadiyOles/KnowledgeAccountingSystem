using System.ComponentModel.DataAnnotations;

namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class LoginModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
