using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Servicos.Aluguel.Models
{
    public class AtualizarAluguelServicoResponseModel : RespostaServicoBase
    {
        public AtualizarAluguelServicoResponseModel() : base()
        {
            EspecificacaoErro = null;
        }

        public AtualizarAluguelServicoResponseModel(string mensagemErro, FalhasPossiveis tipoErro) : base(mensagemErro)
        {
            EspecificacaoErro = tipoErro;
        }

        public FalhasPossiveis? EspecificacaoErro { get; }

        public enum FalhasPossiveis
        {
            ALUGUEL_NAO_EXISTE,
            CARRO_NAO_EXISTE,
            DATAS_INVALIDAS,
            SITUACAO_INVALIDA
        }
    }
}
