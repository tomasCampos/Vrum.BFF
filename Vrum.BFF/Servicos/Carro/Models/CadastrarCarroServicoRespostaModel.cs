using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Servicos.Carro.Models
{
    public class CadastrarCarroServicoRespostaModel : RespostaServicoBase
    {
        public CadastrarCarroServicoRespostaModel(int codigoCarroCadastrado) : base()
        {
            CodigoCarroCadastrado = codigoCarroCadastrado;
            EspecificacaoErro = null;
        }

        public CadastrarCarroServicoRespostaModel(string mensagemErro, FalhasPossiveis tipoErro) : base(mensagemErro) 
        {
            EspecificacaoErro = tipoErro;
        }

        public int CodigoCarroCadastrado { get; }
        public FalhasPossiveis? EspecificacaoErro { get; }

        public enum FalhasPossiveis
        {
            PLACA_JA_CADASTRADA,
            USUARIO_DONO_DO_CARRO_INVALIDO
        }
    }
}
