using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vrum.BFF.Servicos.Usuario.Models;
using Vrum.BFF.Entidades;
using Repositorio.Dtos;
using Vrum.BFF.Util;
using static Repositorio.Constantes.AppConstants;
using static Vrum.BFF.Entidades.UsuarioEntidade;

namespace Vrum.BFF.Servicos.Usuario
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<AutenticarUsuarioRespostaModel> AutenticarUsuario(string emailLogin, string senhaLogin)
        {
            var respostaObterUsuario = await ObterUsuario(emailLogin);

            if (!respostaObterUsuario.Sucesso)
                return new AutenticarUsuarioRespostaModel("Não existe usuário cadastrado para este E-mail.");

            var usuario = respostaObterUsuario.Usuario;
            var senhaCadastrada = usuario.Senha;
            var senhaLogincifrada = CifrarSenhaUsuario(senhaLogin);

            if (!string.Equals(senhaCadastrada, senhaLogincifrada))
                return new AutenticarUsuarioRespostaModel("Senha Inválida");

            return new AutenticarUsuarioRespostaModel(usuario);
        }

        public async Task<CadastrarUsuarioServicoRespostaModel> CadastrarUsuario(UsuarioEntidade usuario)
        {
            var respostaObterUsuario = await ObterUsuario(usuario.Email);

            if (respostaObterUsuario.Sucesso)
                return new CadastrarUsuarioServicoRespostaModel("Nome de usuário já utilizado.");

            var senhaCifrada = CifrarSenhaUsuario(usuario.Senha);
            var usuarioDto = new UsuarioDto 
            {
                Cpf = usuario.Cpf,
                Email = usuario.Email,
                Nome = usuario.Nome,
                NumeroTelefone = usuario.NumeroTelefone,
                Perfil = usuario.ObterCodigoPerfil(),
                Senha = senhaCifrada
            };
            
            await _usuarioRepositorio.CadastrarUsuario(usuarioDto);

            respostaObterUsuario = await ObterUsuario(usuario.Email);
            return new CadastrarUsuarioServicoRespostaModel(respostaObterUsuario.Usuario.Codigo);       
        }

        public async Task<ObterUsuarioServicoRespostaModel> ObterUsuario(string emailUsuario)
        {
            var usuarioDto = await _usuarioRepositorio.ObterUsuario(emailUsuario);

            if (usuarioDto == null)
                return new ObterUsuarioServicoRespostaModel("Não existe usuário com email informado");

            var usuario = new UsuarioEntidade(usuarioDto);
            var resposta = new ObterUsuarioServicoRespostaModel(usuario);
            return resposta;
        }

        private string CifrarSenhaUsuario(string senha)
        {
            var senhaCifrada = AES.Encrypt(senha, CHAVE_CIFRA);
            return senhaCifrada;
        }
    }

    public interface IUsuarioServico
    {
        Task<CadastrarUsuarioServicoRespostaModel> CadastrarUsuario(UsuarioEntidade usuario);
        Task<ObterUsuarioServicoRespostaModel> ObterUsuario(string emailUsuario);
        Task<AutenticarUsuarioRespostaModel> AutenticarUsuario(string emailUsuario, string senha);
    }
}
