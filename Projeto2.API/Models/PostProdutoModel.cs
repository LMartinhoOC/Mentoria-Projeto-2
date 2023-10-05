using System.ComponentModel.DataAnnotations;

namespace Projeto2.API.Models
{
    public class PostProdutoModel
    {
        [Required (ErrorMessage = "O nome do produto é obrigatório")]
        public string Nome { get; set; }
    }
}
