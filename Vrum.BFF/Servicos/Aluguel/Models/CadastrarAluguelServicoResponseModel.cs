using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Servicos.Aluguel.Models
{
    public class CadastrarAluguelServicoResponseModel : RespostaServicoBase
    {
        public CadastrarAluguelServicoResponseModel(int codigoAluguelCadastrado) : base()
        {
            CodigoAluguelCadastrado = codigoAluguelCadastrado;
            EspecificacaoErro = null;
        }

        public CadastrarAluguelServicoResponseModel(string mensagemErro, FalhasPossiveis tipoErro) : base(mensagemErro)
        {
            EspecificacaoErro = tipoErro;
        }

        public int CodigoAluguelCadastrado { get; }
        public FalhasPossiveis? EspecificacaoErro { get; }

        public enum FalhasPossiveis
        {
            LOCATARIO_NAO_EXISTENTE,
            CARRO_NAO_EXISTENTE,
            CARRO_INDISPONÍVEL
        }
    }
}
