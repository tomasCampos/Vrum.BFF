using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vrum.BFF.Controllers.Models;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterUsuario([FromRoute] string emailUsuario)
        {
            if (!emailUsuario.Contains('@'))
            {
                return BadRequest(new HttpResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucesso= false,
                    Mensagem = "Email inválido",
                    Corpo = null
                });
            }

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
    }
}
