using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vrum.BFF.Entidades;

namespace Vrum.BFF.Controllers.Models.Aluguel
{
    public class CadastrarAluguelRequestModel : IRequestModel
    {
        public DateTime DataInicioReserva { get; set; }
        public DateTime DataFimReserva { get; set; }
        public int CodigoCarroQueSeraAlugado { get; set; }
        public int CodigoUsuarioLocatario { get; set; }

        public ValidacaoRequisicaoModel Validar()
        {
            var erros = new List<string>();

            if (DataInicioReserva == null)
                erros.Add("O campo data inicial da reserva deve ser preenchido");
            if (DataFimReserva == null)
                erros.Add("O campo data final da reserva deve ser preenchido");
            if (DataFimReserva != null && DataInicioReserva != null && DataFimReserva <= DataInicioReserva)
                erros.Add("A data final da reserva deve ser superior à data inicial da reserva");            
            if (DataInicioReserva != null && DataInicioReserva < DateTime.Now)
                erros.Add("A data inicial da reserva não pode ser anterior à data atual");
            if (DataFimReserva != null && DataFimReserva < DateTime.Now)
                erros.Add("A data final da reserva não pode ser anterior à data atual");
            if (CodigoCarroQueSeraAlugado <= 0)
                erros.Add("O código do carro informado está inválido");
            if (CodigoUsuarioLocatario <= 0)
                erros.Add("O código do usuário informado está inválido");

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }

        public AluguelEntidade CriarEntidade()
        {
            var entidade = new AluguelEntidade();
            entidade.DataInicioReserva = DataInicioReserva;
            entidade.DataFimReserva = DataFimReserva;
            entidade.CarroAlugado = new AluguelEntidade.CarroAluguel { Codigo = CodigoCarroQueSeraAlugado };
            entidade.UsuarioLocatario = new AluguelEntidade.Locatario { Codigo = CodigoUsuarioLocatario };

            return entidade;
        }
    }
}
