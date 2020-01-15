using System.Collections.Generic;

namespace ApiVendas.Repositories
{
    public interface IVeiculoRepository
    {
        IEnumerable<VeiculoVenda> GetVeiculos();

        VeiculoVenda GetVeiculoId(int id);

        IEnumerable<VeiculoVenda> PesquisarVeiculos(string q);

        bool InserirVeiculo(VeiculoVenda veiculo);

        bool  AtualizarVeiculo(VeiculoVenda veiculo);

        bool MarcarVeiculoVendido(VeiculoVenda veiculo);

        bool ExcluirVeiculo(VeiculoVenda veiculo);    

        bool Teste();
        
        bool Teste2();
    }
}
