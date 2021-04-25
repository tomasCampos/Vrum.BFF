using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Servicos.Usuario.Models
{
    public class AtualizarUsuarioServicoRespostaModel : RespostaServicoBase
    {
        public AtualizarUsuarioServicoRespostaModel() : base() { }

        public AtualizarUsuarioServicoRespostaModel(string mensagemErro) : base(mensagemErro) { }
    }
}
