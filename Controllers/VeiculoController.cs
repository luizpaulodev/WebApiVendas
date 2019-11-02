using ApiVendas.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ApiVendas.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly AppDbContext context;

        public VeiculoController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("v1/veiculos")]
        public IActionResult GetVeiculos()
        {
            return Ok(
                context.Veiculos               
            );
        }

        [HttpGet("v1/veiculos/{id}")]
        public IActionResult GetVeiculoId(int id)
        {
            return Ok(
                context.Veiculos.FirstOrDefault(x => x.Id == id)
            );
        }

        [HttpGet("v1/veiculos/find/{q}")]
        public IActionResult PesquisarVeiculos(string q)
        {
            int ano = 0;
            string param = q.ToUpper();
            Int32.TryParse(q, out ano);

            return Ok(
               context.Veiculos.Where(
                   x => x.Veiculo.ToUpper().Contains(param) 
                        || x.Marca.ToUpper().Contains(param)
                        || x.Descricao.ToUpper().Contains(param)
                        || x.Ano.Equals(ano))
                    .ToList()
            );
        }

        [HttpPost("v1/veiculos")]
        public IActionResult InserirVeiculo([FromBody]VeiculoVenda veiculo)
        {
            if(veiculo != null){
                veiculo.Created = DateTime.Now;
                veiculo.Updated = DateTime.Now;

                context.Veiculos.Add(veiculo);
                int x = context.SaveChanges();

                return Ok(x > 0);
            } else {
                return NotFound(false);
            }            
        }

        [HttpPut("v1/veiculos/{id}")]
        public IActionResult AtualizarVeiculo(int id, [FromBody]VeiculoVenda veiculo)
        {
            VeiculoVenda veiculoAux = context.Veiculos.FirstOrDefault(x => x.Id == id);

            if(veiculoAux == null){
                return NotFound("Veiculo não localizado!");
            } else {
                veiculoAux.Veiculo = veiculo.Veiculo;
                veiculoAux.Marca = veiculo.Marca;
                veiculoAux.Ano = veiculo.Ano;
                veiculoAux.Descricao = veiculo.Descricao;
                veiculoAux.Vendido = veiculo.Vendido;
                veiculoAux.Updated = DateTime.Now;

                context.Veiculos.Update(veiculoAux);
                int x = context.SaveChanges();

                return Ok(x > 0);
            }
        }

        [HttpPatch("v1/veiculos/{id}")]
        public IActionResult MarcarVeiculoVendido(int id)
        {
            VeiculoVenda veiculoAux = context.Veiculos.FirstOrDefault(x => x.Id == id);

            if(veiculoAux == null){
                return NotFound("Veiculo não localizado!");
            } else {
                veiculoAux.Vendido = true;
                veiculoAux.Updated = DateTime.Now;

                context.Veiculos.Update(veiculoAux);
                int x = context.SaveChanges();

                return Ok(x > 0);
            }
        }

        [HttpDelete("v1/veiculos/{id}")]
        public IActionResult ExcluirVeiculo(int id)
        {
            VeiculoVenda veiculo = context.Veiculos.FirstOrDefault(x => x.Id == id);
            
            if(veiculo == null){
                return NotFound("Veiculo não localizado!");                
            } else {              
                context.Veiculos.Remove(veiculo);
                int x = context.SaveChanges();

                return Ok(x > 0);
            }
        }       
    }
}
