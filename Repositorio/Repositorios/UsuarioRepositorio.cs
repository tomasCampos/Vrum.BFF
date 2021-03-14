using Repositorio.Constantes;
using Repositorio.Dtos;
using Repositorio.Infraestrutura;
using System.Threading.Tasks;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DataBaseConnector _dataBase;

        public UsuarioRepositorio()
        {
            _dataBase = new DataBaseConnector();
        }

        public async Task CadastrarUsuario(UsuarioDto usuario)
        {
            await _dataBase.ExecutarAsync(AppConstants.SQL_CADASTRAR_USUARIO, new { email_usuario = usuario.Email, senha_usuario = usuario.Senha, nome_usuario = usuario.Nome, cpf_usuario = usuario.Cpf, 
                telefone_usuario = usuario.NumeroTelefone, perfil_usuario = usuario.Perfil});
        }

        public async Task<UsuarioDto> ObterUsuario(string emailUsuario)
        {
            var result = await _dataBase.SelecionarAsync<UsuarioDto>(AppConstants.SQL_OBTER_USUARIO_POR_EMAIL, new { email_usuario = emailUsuario });
            return result.FirstOrDefault();
        }
    }

    public interface IUsuarioRepositorio
    {
        Task CadastrarUsuario(UsuarioDto usuario);
        Task<UsuarioDto> ObterUsuario(string emailUsuario);
    }
}
