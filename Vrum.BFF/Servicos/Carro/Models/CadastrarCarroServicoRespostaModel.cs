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
        }

        public CadastrarCarroServicoRespostaModel(string mensagemErro) : base(mensagemErro) {}

        public int CodigoCarroCadastrado { get; }
    }
}
