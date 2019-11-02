using ApiVendas.Models;
using ApiVendas.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ApiVendas.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly IVeiculoRepository veiculoDb;

        public VeiculoController(IVeiculoRepository veiculoDb)
        {
            this.veiculoDb = veiculoDb;
        }

        [HttpGet("v1/veiculos")]
        public IActionResult GetVeiculos()
        {
            return Ok(
                veiculoDb.GetVeiculos()               
            );
        }

        [HttpGet("v1/veiculos/{id}")]
        public IActionResult GetVeiculoId(int id)
        {
            return Ok(
                veiculoDb.GetVeiculoId(id)
            );
        }

        [HttpGet("v1/veiculos/find/{q}")]
        public IActionResult PesquisarVeiculos(string q)
        {
            return Ok(
               veiculoDb.PesquisarVeiculos(q)
            );
        }

        [HttpPost("v1/veiculos")]
        public IActionResult InserirVeiculo([FromBody]VeiculoVenda veiculo)
        {
            if(veiculo != null){
                veiculo.Created = DateTime.Now;
                veiculo.Updated = DateTime.Now;

                return Ok(veiculoDb.InserirVeiculo(veiculo));
            } else {
                return NotFound(false);
            }            
        }

        [HttpPut("v1/veiculos/{id}")]
        public IActionResult AtualizarVeiculo(int id, [FromBody]VeiculoVenda veiculo)
        {
            VeiculoVenda veiculoAux = veiculoDb.GetVeiculoId(id);

            if(veiculoAux == null){
                return NotFound("Veiculo não localizado!");
            } else {
                veiculoAux.Veiculo = veiculo.Veiculo;
                veiculoAux.Marca = veiculo.Marca;
                veiculoAux.Ano = veiculo.Ano;
                veiculoAux.Descricao = veiculo.Descricao;
                veiculoAux.Vendido = veiculo.Vendido;
                veiculoAux.Updated = DateTime.Now;

                return Ok(veiculoDb.AtualizarVeiculo(veiculoAux));
            }
        }

        [HttpPatch("v1/veiculos/{id}")]
        public IActionResult MarcarVeiculoVendido(int id)
        {
            VeiculoVenda veiculoAux = veiculoDb.GetVeiculoId(id);

            if(veiculoAux == null){
                return NotFound("Veiculo não localizado!");
            } else {
                veiculoAux.Vendido = true;
                veiculoAux.Updated = DateTime.Now;

                return Ok(veiculoDb.MarcarVeiculoVendido(veiculoAux));
            }
        }

        [HttpDelete("v1/veiculos/{id}")]
        public IActionResult ExcluirVeiculo(int id)
        {
            VeiculoVenda veiculoAux = veiculoDb.GetVeiculoId(id);
            
            if(veiculoAux == null){
                return NotFound("Veiculo não localizado!");                
            } else {              
                return Ok(veiculoDb.ExcluirVeiculo(veiculoAux));
            }
        }       
    }
}
