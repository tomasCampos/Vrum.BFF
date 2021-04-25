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

        public async Task AtualizarUsuario(UsuarioDto usuario)
        {
            await _dataBase.ExecutarAsync(AppConstants.SQL_ATUALIZAR_USUARIO, new 
            {
                email_usuario = usuario.Email,
                senha_usuario = usuario.Senha,
                nome_usuario = usuario.Nome,
                cpf_usuario = usuario.Cpf,
                telefone_usuario = usuario.NumeroTelefone,
                bairro_endereco = usuario.BairroEndereco,
                cep_endereco = usuario.CepEndereco,
                complemento_endereco = usuario.ComplementoEndereco,
                logradouro_endereco = usuario.LogradouroEndereco,
                numero_endereco = usuario.NumeroEndereco,
                uf_endereco = usuario.UfEndereco
            });
        }

        public async Task CadastrarUsuario(UsuarioDto usuario)
        {
            await CadastrarEnderecoDoUsuario(usuario.ChaveIdentificacaoEndereco, usuario.CepEndereco, usuario.BairroEndereco, usuario.ComplementoEndereco,
                usuario.LogradouroEndereco, usuario.NumeroEndereco, usuario.UfEndereco);

            var codigoEndereco = await ObterCodigoDoEndereco(usuario.ChaveIdentificacaoEndereco);

            await _dataBase.ExecutarAsync(AppConstants.SQL_CADASTRAR_USUARIO, new { email_usuario = usuario.Email, senha_usuario = usuario.Senha, 
                nome_usuario = usuario.Nome, cpf_usuario = usuario.Cpf, telefone_usuario = usuario.NumeroTelefone, perfil_usuario = usuario.Perfil,
                id_endereco = codigoEndereco});
        }

        public async Task<UsuarioDto> ObterUsuario(string emailUsuario)
        {
            var result = await _dataBase.SelecionarAsync<UsuarioDto>(AppConstants.SQL_OBTER_USUARIO_POR_EMAIL, new { email_usuario = emailUsuario });
            return result.FirstOrDefault();
        }

        public async Task<UsuarioDto> ObterUsuario(int codigoUsuario)
        {
            var result = await _dataBase.SelecionarAsync<UsuarioDto>(AppConstants.SQL_OBTER_USUARIO_POR_CODIGO, new { id_usuario = codigoUsuario });
            return result.FirstOrDefault();
        }

        #region METODOS PRIVADOS

        private async Task CadastrarEnderecoDoUsuario(string chave, string cep, string bairro, string complemento, string logradouro, string numero, string uf)
        {
            await _dataBase.ExecutarAsync(AppConstants.SQL_CADASTRAR_ENDERECO, new 
            { 
                chave_endereco = chave,
                cep_endereco = cep,
                logradouro_endereco = logradouro,
                numero_endereco = numero,
                complemento_endereco = complemento,
                bairro_endereco = bairro,
                uf_endereco = uf
            });
        }

        private async Task<int> ObterCodigoDoEndereco(string chaveIdentificacao)
        {
            var codigoEndereco = await _dataBase.SelecionarAsync<int>(AppConstants.SQL_OBTER_CODIGO_ENDERECO_POR_CHAVE, new { chave_endereco = chaveIdentificacao });
            return codigoEndereco.First();
        }

        #endregion
    }

    public interface IUsuarioRepositorio
    {
        Task CadastrarUsuario(UsuarioDto usuario);
        Task<UsuarioDto> ObterUsuario(string emailUsuario);
        Task<UsuarioDto> ObterUsuario(int codigoUsuario);
        Task AtualizarUsuario(UsuarioDto usuario);
    }
}
