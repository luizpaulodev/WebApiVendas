using System;
using System.Collections.Generic;
using System.Linq;
using ApiVendas.Models;

namespace ApiVendas.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly AppDbContext context;

        public VeiculoRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<VeiculoVenda> GetVeiculos()
        {
            return context.Veiculos;
        }

        public VeiculoVenda GetVeiculoId(int id)
        {
            return context.Veiculos.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<VeiculoVenda> PesquisarVeiculos(string q)
        {
            int ano = 0;
            string param = q.ToUpper();
            Int32.TryParse(q, out ano);

            return 
               context.Veiculos.Where(
                   x => x.Veiculo.ToUpper().Contains(param) 
                        || x.Marca.ToUpper().Contains(param)
                        || x.Descricao.ToUpper().Contains(param)
                        || x.Ano.Equals(ano))
                    .ToList();
        }

        public bool InserirVeiculo(VeiculoVenda veiculo)
        {
            context.Veiculos.Add(veiculo);
            return context.SaveChanges() > 0;
        }

        public bool AtualizarVeiculo(VeiculoVenda veiculo)
        {
            context.Veiculos.Update(veiculo);
            return context.SaveChanges() > 0;
        }

        public bool MarcarVeiculoVendido(VeiculoVenda veiculo)
        {
            context.Veiculos.Update(veiculo);
            return context.SaveChanges() > 0;
        }

        public bool ExcluirVeiculo(VeiculoVenda veiculo)
        {
            context.Veiculos.Remove(veiculo);
            return context.SaveChanges() > 0;
        }
    }
}