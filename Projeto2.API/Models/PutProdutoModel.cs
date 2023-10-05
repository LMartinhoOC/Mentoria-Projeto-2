using System.ComponentModel.DataAnnotations;

namespace Projeto2.API.Models
{
    public class PutProdutoModel
    {
        [Required (ErrorMessage = "O nome do produto atual é obrigatório")]
        public string NomeAtual { get; set; }
        [Required(ErrorMessage = "O nome do produto para alterar é obrigatório")]
        public string NomeNovo  { get; set; }
    }
}
