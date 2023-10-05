using System.ComponentModel.DataAnnotations;

namespace Projeto2.API.Models
{
    public class PostAuthLoginModel
    {
        [Required (ErrorMessage = "O login é obrigatório")]
        public string login    { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string password { get; set; }
    }
}
