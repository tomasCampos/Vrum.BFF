using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Controllers.Models.Aluguel
{
    public class AlterarAluguelRequestModel : IRequestModel
    {
        public DateTime? DataInicioReserva { get; set; }
        public DateTime? DataFimReserva { get; set; }
        public DateTime? DataDeDevolucaoDoCarro { get; set; }
        public int? CodigoCarroQueSeraAlugado { get; set; }
        public string Situacao { get; set; }

        public ValidacaoRequisicaoModel Validar()
        {
            var erros = new List<string>();

            if (DataFimReserva.HasValue && DataInicioReserva.HasValue && DataFimReserva.Value <= DataInicioReserva.Value)
                erros.Add("A data final da reserva deve ser superior à data inicial da reserva.");
            if (DataInicioReserva.HasValue && DataInicioReserva.Value.Date < DateTime.Now.Date)
                erros.Add("A data inicial da reserva não pode ser anterior à data atual.");
            if (DataFimReserva.HasValue && DataFimReserva.Value.Date < DateTime.Now.Date)
                erros.Add("A data final da reserva não pode ser anterior à data atual.");
            if (DataDeDevolucaoDoCarro.HasValue && DataDeDevolucaoDoCarro.Value.Date > DateTime.Now.Date)
                erros.Add("A data que o carro foi devolvido não pode estar no futuro.");
            if (CodigoCarroQueSeraAlugado.HasValue && CodigoCarroQueSeraAlugado <= 0)
                erros.Add("O código do carro informado está inválido.");

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }
    }
}
