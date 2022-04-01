using Vrum.BFF.Entidades;

namespace Vrum.BFF.Servicos.Usuario.Models
{
    public class AutenticarUsuarioRespostaModel : RespostaServicoBase
    {
        public AutenticarUsuarioRespostaModel(UsuarioEntidade usuarioLogado) : base() 
        {
            UsuarioLogado = usuarioLogado;
        }

        public AutenticarUsuarioRespostaModel(string mensagemFalha) : base(mensagemFalha) { }
        public UsuarioEntidade UsuarioLogado { get; }
    }
}
