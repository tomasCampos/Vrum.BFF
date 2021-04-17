using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Servicos.Carro.Models
{
    public class AtualizarCarroServicoRespostaModel : RespostaServicoBase
    {
        public AtualizarCarroServicoRespostaModel() : base() {}

        public AtualizarCarroServicoRespostaModel(string mensagemErro) : base(mensagemErro) {}
    }
}
