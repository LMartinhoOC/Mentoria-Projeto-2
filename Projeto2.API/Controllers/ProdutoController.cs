using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto2.API.Models;

namespace Projeto2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        public List<string> Produtos { get; set; }

        public ProdutoController()
        {
            Produtos = new List<string>()
            {
                "Produto1",
                "Produto2",
                "Produto3",
                "Produto4",
                "Produto5"
            };
        }
        
        [HttpGet]
        [Authorize(Roles = "student,admin")]
        [Route("produto")]
        public ActionResult GetProduto()
        {
            return Ok(Produtos);
        }

        [HttpPost]
        [Authorize]
        [Route("produto")]
        public ActionResult PostProduto([FromBody] PostProdutoModel model)
        {
            if (!ModelState.IsValid) 
            {
               return BadRequest(ModelState);
            }
            else
            {
                Produtos.Add(model.Nome);

                return Ok(Produtos);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("produto")]
        public ActionResult PutProduto([FromBody] PutProdutoModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            else
            {
                if (!Produtos.Contains(model.NomeAtual))
                {
                    return BadRequest("O nome do produto atual não encontrado");
                }
                else
                {
                    int indice = Produtos.IndexOf(model.NomeAtual);

                    Produtos.Remove(model.NomeAtual);
                    Produtos.Insert(indice, model.NomeNovo);

                    return Ok(Produtos);
                }
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("produto")]
        public ActionResult DeleteProduto(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return BadRequest("O nome do produto é obrigatório");
            }
            else if (!Produtos.Contains(nome))
            {
                return BadRequest("O nome do produto não encontrado");
            }
            else
            {
                Produtos.Remove(nome);

                return Ok(Produtos);
            }
        }
    }
}
