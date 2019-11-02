using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVendas
{    
    public class VeiculoVenda
    {
        public int Id { get; set; }
        public String Veiculo { get; set; }
        public String Marca { get; set; }
        public int Ano { get; set; }
        public String Descricao { get; set; }
        public bool Vendido { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}