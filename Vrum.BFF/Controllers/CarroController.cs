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
    }
}
