using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vrum.BFF.Servicos.Usuario.Models;
using Vrum.BFF.Entidades;

namespace Vrum.BFF.Servicos.Usuario
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public Task<CadastrarUsuarioServicoRespostaModel> CadastrarUsuario(UsuarioEntidade usuario)
        {
            throw new NotImplementedException();
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
    }

    public interface IUsuarioServico
    {
        Task<CadastrarUsuarioServicoRespostaModel> CadastrarUsuario(UsuarioEntidade usuario);
        Task<ObterUsuarioServicoRespostaModel> ObterUsuario(string emailUsuario);
    }
}
