using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Controllers.Models.Carro
{
    public class AlterarCarroRequestModel
    {
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Cor { get; set; }
        public int? NumeroDeAssentos { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public int? Ano { get; set; }
        public double? PrecoDaDiaria { get; set; }
        public bool? Disponibilidade { get; set; }
    }
}
