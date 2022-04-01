using Vrum.BFF.Entidades;

namespace Vrum.BFF.Servicos.Usuario.Models
{
    public class ObterUsuarioServicoRespostaModel : RespostaServicoBase
    {
        public ObterUsuarioServicoRespostaModel(UsuarioEntidade usuario) : base()
        {
            Usuario = usuario;
        }

        public ObterUsuarioServicoRespostaModel(string mensagemFalha) : base(mensagemFalha) 
        {
            Usuario = null;
        }

        public UsuarioEntidade Usuario { get; }
    }
}
