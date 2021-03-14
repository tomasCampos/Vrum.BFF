using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Servicos.Usuario.Models
{
    public class CadastrarUsuarioServicoRespostaModel : RespostaServicoBase
    {
        public CadastrarUsuarioServicoRespostaModel(int codigoUsuarioCadastrado) : base() 
        {
            CodigoUsuarioCadastrado = codigoUsuarioCadastrado;            
        }

        public CadastrarUsuarioServicoRespostaModel(string mensagemErro) : base(mensagemErro) {}

        public int CodigoUsuarioCadastrado { get; }
    }
}
