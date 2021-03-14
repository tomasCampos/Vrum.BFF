using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("{emailUsuario}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterUsuario([FromRoute] string emailUsuario)
        {
            var respostaServico = await _usuarioServico.ObterUsuario(emailUsuario);

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
        public async Task<IActionResult> CadastrarUsuario([FromBody] CadastrarUsuarioRequestModel requisicao)
        {
            var validacao = requisicao.Validar();
            if (!validacao.Valido)
            {
                return BadRequest(new HttpResponseModel
                {
                    StatusCode= System.Net.HttpStatusCode.BadRequest,
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
    }
}
