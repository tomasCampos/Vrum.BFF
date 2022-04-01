using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vrum.BFF.Controllers.Models;
using Vrum.BFF.Controllers.Models.Aluguel;
using Vrum.BFF.Entidades;
using Vrum.BFF.Servicos.Aluguel;

namespace Vrum.BFF.Controllers
{
    [ApiController]
    [Route("alugueis")]
    public class AluguelController : ControllerBase
    {
        private readonly IAluguelServico _aluguelServico;

        public AluguelController(IAluguelServico aluguelServico)
        {
            _aluguelServico = aluguelServico;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarAluguel([FromBody] CadastrarAluguelRequestModel requisicao)
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

            var aluguel = requisicao.CriarEntidade();
            var resultado = await _aluguelServico.CadastrarAluguel(aluguel);

            if (!resultado.Sucesso)
            {
                return BadRequest(new HttpResponseModel
                {
                    Sucesso = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Mensagem = resultado.Mensagem
                });
            }

            return Created("/Alugueis", new HttpResponseModel()
            {
                Sucesso = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Corpo = new { CodigoAluguelCadastrado = resultado.CodigoAluguelCadastrado }
            });
        }

        [HttpGet("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterAluguel([FromRoute] int codigo)
        {
            var aluguel = await _aluguelServico.ObterAluguel(codigo);

            if (aluguel == null)
            {
                return NotFound(new HttpResponseModel
                {
                    Sucesso = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Mensagem = "Aluguel não encontrado"
                });
            }

            return Ok(new HttpResponseModel
            {
                Sucesso = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Corpo = aluguel
            });
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListarAluguel(int? codigoUsuarioLocador = null, int? codigoUsuarioLocatario = null, string situacao = null)
        {
            if (situacao != null)
            {
                if (!Enum.TryParse<AluguelEntidade.SituacaoAluguel>(situacao, out _))
                {
                    return BadRequest(new HttpResponseModel
                    {
                        Sucesso = false,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Mensagem = "O campo situação deve ser uma das: PENDENTE, EM_ANDAMENTO, REJEITADO, FINALIZADO"
                    });
                }
            }

            var alugueis = await _aluguelServico.ListarAlugueis(codigoUsuarioLocador, codigoUsuarioLocatario, situacao);

            return Ok(new HttpResponseModel
            {
                Sucesso = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Corpo = alugueis
            });
        }

        [HttpGet("resumo/{codigoUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterResumoAlugueisUsuario([FromRoute] int codigoUsuario)
        {
            if(codigoUsuario <= 0)
            {
                return BadRequest(new HttpResponseModel 
                {
                    Sucesso = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Mensagem = "Código de usuário inválido"
                });
            }

            var resultado = await _aluguelServico.ObterResumoAluguelUsuario(codigoUsuario);

            if (!resultado.Sucesso)
            {
                return BadRequest(new HttpResponseModel 
                {
                    Sucesso = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Mensagem = resultado.Mensagem
                });
            }

            return Ok(new HttpResponseModel 
            {
                Sucesso = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Corpo = new
                {
                    resultado.QuantidadeDeAlugueisPendentes,
                    resultado.QuantidadeDeAlugueisEmAndamento,
                    resultado.QuantidadeDeAlugueisRejeitados,
                    resultado.QuantidadeDeAlugueisFinalizados
                }
            });
        }

        [HttpPatch("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarAluguel([FromRoute] int codigo, [FromBody] AlterarAluguelRequestModel requisicao)
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

            var resultadoAlteracao = await _aluguelServico.AtualizarAluguel(codigo, requisicao);

            if (!resultadoAlteracao.Sucesso)
            {
                return BadRequest(new HttpResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucesso = false,
                    Mensagem = resultadoAlteracao.Mensagem
                });
            }

            return Ok(new HttpResponseModel
            {
                Sucesso = true,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

    }
}
