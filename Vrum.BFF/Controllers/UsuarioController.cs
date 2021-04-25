using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vrum.BFF.Controllers.Models;
using Vrum.BFF.Controllers.Models.Usuario;
using Vrum.BFF.Servicos.Usuario;

namespace Vrum.BFF.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;

        public UsuarioController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        [HttpGet("{codigo}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterUsuario([FromRoute] int codigo)
        {
            var respostaServico = await _usuarioServico.ObterUsuario(codigo);

            if (!respostaServico.Sucesso)
            {
                return NotFound(new HttpResponseModel 
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Sucesso = false,
                    Mensagem = respostaServico.Mensagem,
                    Corpo = null
                });
            }

            return Ok(new HttpResponseModel 
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Sucesso = true,
                Mensagem = respostaServico.Mensagem,
                Corpo = respostaServico.Usuario
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarUsuario([FromBody] CadastrarUsuarioRequestModel requisicao)
        {
            var validacao = requisicao.Validar();
            if (!validacao.Valido)
            {
                return BadRequest(new HttpResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucesso = false,
                    Mensagem = validacao.MensagemDeErro
                });
            }

            var usuario = requisicao.CriarEntidade();
            var resultado = await _usuarioServico.CadastrarUsuario(usuario);

            if (!resultado.Sucesso)
            {
                return Conflict(new HttpResponseModel
                {
                    Sucesso = false,
                    StatusCode = System.Net.HttpStatusCode.Conflict,
                    Mensagem = resultado.Mensagem,
                    Corpo = null
                });
            }

            return Created("/Usuarios", new HttpResponseModel
            {
                Sucesso = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Mensagem = resultado.Mensagem,
                Corpo = new { CodigoUsuarioCadastrado = resultado.CodigoUsuarioCadastrado }
            });
        }

        [HttpPost("autenticacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AutenticarUsuario([FromBody] AutenticarUsuarioRequestModel requisicao)
        {
            var validacao = requisicao.Validar();
            if (!validacao.Valido)
            {
                return BadRequest(new HttpResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucesso = false,
                    Mensagem = validacao.MensagemDeErro
                });
            }

            var resultadoAutenticacao = await _usuarioServico.AutenticarUsuario(requisicao.EmailUsuario, requisicao.SenhaUsuario, requisicao.PerfilUsuario);

            if (!resultadoAutenticacao.Sucesso)
            {
                return Unauthorized(new HttpResponseModel 
                {
                    Sucesso = false,
                    StatusCode = System.Net.HttpStatusCode.Unauthorized,
                    Mensagem = resultadoAutenticacao.Mensagem,
                    Corpo = null
                });
            }

            return Ok(new HttpResponseModel
            {
                Sucesso = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Mensagem = resultadoAutenticacao.Mensagem,
                Corpo = resultadoAutenticacao.UsuarioLogado
            });
        }
    }
}
