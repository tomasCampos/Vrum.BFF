using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vrum.BFF.Controllers.Models;
using Vrum.BFF.Controllers.Models.Carro;
using Vrum.BFF.Servicos.Carro;

namespace Vrum.BFF.Controllers
{
    [ApiController]
    [Route("carros")]
    public class CarroController : ControllerBase
    {
        private readonly ICarroServico _carroServico;

        public CarroController(ICarroServico carroServico)
        {
            _carroServico = carroServico;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarCarro([FromBody] CadastrarCarroRequestModel requisicao)
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

            var carro = requisicao.CriarEntidade();
            var resultado = await _carroServico.CadastrarCarro(carro);

            if (!resultado.Sucesso) 
            {
                if (resultado.EspecificacaoErro == Servicos.Carro.Models.CadastrarCarroServicoRespostaModel.FalhasPossiveis.PLACA_JA_CADASTRADA)
                {
                    return Conflict(new HttpResponseModel
                    {
                        Sucesso = false,
                        StatusCode = System.Net.HttpStatusCode.Conflict,
                        Mensagem = resultado.Mensagem
                    });
                }
                else
                {
                    return BadRequest(new HttpResponseModel
                    {
                        Sucesso = false,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Mensagem = resultado.Mensagem
                    });
                }
            }

            return Created("/Carros", new HttpResponseModel() 
            {
                Sucesso = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Mensagem = resultado.Mensagem,
                Corpo = new { CodigoCarroCadastrado = resultado.CodigoCarroCadastrado }
            });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarCarro([FromQuery] string modelo, string marca, string cidade, string estado, int? ano = null, 
            int? codigoUsuarioDonoDoCarro = null, bool? disponibilidade = null)
        {
            var resultado = await _carroServico.ListarCarros(modelo, ano, marca, cidade, estado, codigoUsuarioDonoDoCarro, disponibilidade);
            return Ok(new HttpResponseModel 
            {
                Sucesso = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Corpo = resultado
            });
        }

        [HttpGet("buscaGenerica")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarCarro([FromQuery] string termo, int? codigoUsuarioDonoDoCarro = null, bool? disponibilidade = null)
        {
            var resultado = await _carroServico.ListarCarros(termo, codigoUsuarioDonoDoCarro, disponibilidade);
            return Ok(new HttpResponseModel
            {
                Sucesso = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Corpo = resultado
            });
        }

        [HttpGet("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterCarro(int codigo)
        {
            var resultadoServico = await _carroServico.ObterCarro(codigo);

            if (!resultadoServico.Sucesso)
            {
                return NotFound(new HttpResponseModel
                {
                    Sucesso = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Mensagem = resultadoServico.Mensagem
                });
            }

            return Ok(new HttpResponseModel
            {
                Sucesso = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Corpo = resultadoServico.Carro
            });
        }

        [HttpPatch("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarCarro([FromRoute] int codigo, [FromBody] AlterarCarroRequestModel requisicao)
        {
            var resultadoAtualizacao = await _carroServico.Atualizarcarro(codigo, requisicao);

            if (!resultadoAtualizacao.Sucesso)
            {
                return BadRequest(new HttpResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Sucesso = false,
                    Mensagem = resultadoAtualizacao.Mensagem
                });
            }

            return Ok(new HttpResponseModel 
            {
                Sucesso = true,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        [HttpDelete("{codigoCarro}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletarCarro([FromRoute] int codigoCarro)
        {
            await _carroServico.DeletarCarro(codigoCarro);

            return Ok(new HttpResponseModel 
            {
                Sucesso = true,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }
    }
}
