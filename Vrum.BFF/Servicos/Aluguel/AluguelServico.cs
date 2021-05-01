using Repositorio.Dtos;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vrum.BFF.Controllers.Models.Aluguel;
using Vrum.BFF.Entidades;
using Vrum.BFF.Servicos.Aluguel.Models;
using Vrum.BFF.Servicos.Carro;
using Vrum.BFF.Servicos.Usuario;
using static Vrum.BFF.Entidades.AluguelEntidade;

namespace Vrum.BFF.Servicos.Aluguel
{
    public class AluguelServico : IAluguelServico
    {
        private readonly IAluguelRepositorio _aluguelRepositorio;
        private readonly IUsuarioServico _usuarioServico;
        private readonly ICarroServico _carroServico;

        public AluguelServico(IAluguelRepositorio aluguelRepositorio, IUsuarioServico usuarioServico, ICarroServico carroServico)
        {
            _aluguelRepositorio = aluguelRepositorio;
            _usuarioServico = usuarioServico;
            _carroServico = carroServico;
        }

        public async Task<AtualizarAluguelServicoResponseModel> AtualizarAluguel(int codigoAluguel, AlterarAluguelRequestModel requisicao)
        {
            var aluguel = await _aluguelRepositorio.ObterAluguel(codigoAluguel);
            if (aluguel == null)
                return new AtualizarAluguelServicoResponseModel("Aluguel informado não existe.", AtualizarAluguelServicoResponseModel.FalhasPossiveis.ALUGUEL_NAO_EXISTE);

            double novoPrecoDiaria;
            if (requisicao.CodigoCarroQueSeraAlugado.HasValue)
            {
                var resultado = await _carroServico.ObterCarro(requisicao.CodigoCarroQueSeraAlugado.Value);

                if (!resultado.Sucesso)
                    return new AtualizarAluguelServicoResponseModel("Carro informado não existe.", AtualizarAluguelServicoResponseModel.FalhasPossiveis.CARRO_NAO_EXISTE);

                novoPrecoDiaria = resultado.Carro.PrecoDaDiaria;
            }
            else
            {
                novoPrecoDiaria = aluguel.PrecoDaDiariaCarroAlugado;
            }

            if (requisicao.DataDeDevolucaoDoCarro != null && requisicao.DataDeDevolucaoDoCarro < aluguel.DataInicioReserva)
            {
                return new AtualizarAluguelServicoResponseModel("A data de devolução do carro não pode ser menor que a data de início da reserva.",
                    AtualizarAluguelServicoResponseModel.FalhasPossiveis.DATAS_INVALIDAS);
            }
            if (requisicao.DataFimReserva != null && requisicao.DataFimReserva < aluguel.DataInicioReserva)
            {
                return new AtualizarAluguelServicoResponseModel("A data de fim da reserva não pode ser menor que a data de início da reserva.",
                    AtualizarAluguelServicoResponseModel.FalhasPossiveis.DATAS_INVALIDAS);
            }

            int codigoSituacao = 0;
            if (!string.IsNullOrEmpty(requisicao.Situacao))
            {
                SituacaoAluguel situacaoAlteracaoAluguel;
                if (!Enum.TryParse<SituacaoAluguel>(requisicao.Situacao, out situacaoAlteracaoAluguel))
                {
                    return new AtualizarAluguelServicoResponseModel("O campo situação deve ser uma das: PENDENTE, EM_ANDAMENTO, REJEITADO, FINALIZADO", 
                        AtualizarAluguelServicoResponseModel.FalhasPossiveis.SITUACAO_INVALIDA);
                }
                codigoSituacao = (int)situacaoAlteracaoAluguel;
                if (codigoSituacao < aluguel.Situacao)
                {
                    return new AtualizarAluguelServicoResponseModel("A situação de um aluguel não pode retroceder",
                        AtualizarAluguelServicoResponseModel.FalhasPossiveis.RETROCEDENDO_SITUACOES);
                }
            }

            var novaDataInicioReserva = requisicao.DataInicioReserva == null ? aluguel.DataInicioReserva : requisicao.DataInicioReserva;
            var novaDataFimReserva = requisicao.DataFimReserva == null ? aluguel.DataFimReserva : requisicao.DataFimReserva;
            var novoCodigoDoCarro = !requisicao.CodigoCarroQueSeraAlugado.HasValue ? aluguel.CodigoCarroAlugado : requisicao.CodigoCarroQueSeraAlugado;
            var novoCodigoSituacao = string.IsNullOrEmpty(requisicao.Situacao) ? aluguel.Situacao : codigoSituacao;

            if (novoCodigoSituacao == 4 && requisicao.DataDeDevolucaoDoCarro == null)
            {
                return new AtualizarAluguelServicoResponseModel("Para encerrar o aluguel é necessário informar a data de devolução do carro", AtualizarAluguelServicoResponseModel.FalhasPossiveis.DATAS_INVALIDAS);
            }

            var novaDataDevolucao = requisicao.DataDeDevolucaoDoCarro == null ? aluguel.DataDevolucaoCarro : requisicao.DataDeDevolucaoDoCarro;
            var novoPrecoTotal = novoPrecoDiaria * (novaDataInicioReserva - novaDataFimReserva).TotalDays;

            await _aluguelRepositorio.AtualizarAluguel(codigoAluguel, novaDataInicioReserva, novaDataFimReserva, novaDataDevolucao, novoCodigoSituacao, novoPrecoTotal, codigoAluguel);
            return new AtualizarAluguelServicoResponseModel();
        }

        public async Task<CadastrarAluguelServicoResponseModel> CadastrarAluguel(AluguelEntidade aluguel)
        {
            var usuarioLocatario = await _usuarioServico.ObterUsuario(aluguel.UsuarioLocatario.Codigo);
            if (!usuarioLocatario.Sucesso)
                return new CadastrarAluguelServicoResponseModel("O usuário com código de locatário informado não existe.", 
                    CadastrarAluguelServicoResponseModel.FalhasPossiveis.LOCATARIO_NAO_EXISTENTE);
            if (!string.Equals(usuarioLocatario.Usuario.Perfil, "LOCATARIO"))
                return new CadastrarAluguelServicoResponseModel("Usuários que não são do perfil LOCATARIO não podem alugar carros.", 
                    CadastrarAluguelServicoResponseModel.FalhasPossiveis.LOCATARIO_NAO_EXISTENTE);

            var carroQueSeraAlugado = await _carroServico.ObterCarro(aluguel.CarroAlugado.Codigo);
            if(!carroQueSeraAlugado.Sucesso)
                return new CadastrarAluguelServicoResponseModel("O carro com código informado não existe", CadastrarAluguelServicoResponseModel.FalhasPossiveis.CARRO_NAO_EXISTENTE);
            if(!carroQueSeraAlugado.Carro.Disponibilidade)
                return new CadastrarAluguelServicoResponseModel("O carro que você deseja alugar não está disponível", 
                    CadastrarAluguelServicoResponseModel.FalhasPossiveis.CARRO_INDISPONÍVEL);

            var precoTotalAluguel = carroQueSeraAlugado.Carro.PrecoDaDiaria * (aluguel.DataFimReserva - aluguel.DataInicioReserva).TotalDays;
            var chaveIdentificacaoreserva = Guid.NewGuid().ToString();
            var aluguelDto = new AluguelDto 
            {
                ChaveIdentificacaoReserva = chaveIdentificacaoreserva,
                DataInicioReserva = aluguel.DataInicioReserva,
                DataFimReserva = aluguel.DataFimReserva,
                PrecoTotal = precoTotalAluguel,
                CodigoUsuarioLocatario = aluguel.UsuarioLocatario.Codigo,
                CodigoCarroAlugado = carroQueSeraAlugado.Carro.Codigo
            };

            await _aluguelRepositorio.CadastrarAluguel(aluguelDto);
            var codigoAluguelCadastrado = await _aluguelRepositorio.ObterAluguel(chaveIdentificacaoreserva);
            return new CadastrarAluguelServicoResponseModel(codigoAluguelCadastrado.Codigo);
        }

        public async Task<List<AluguelEntidade>> ListarAlugueis(int? codigoUsuarioLocador = null, int? codigoUsuarioLocatario = null, string situacao = null)
        {
            List<AluguelDto> retornoRepositorio;
            if (situacao == null)
                retornoRepositorio = await _aluguelRepositorio.ListarAlugueis(codigoUsuarioLocador, codigoUsuarioLocatario);
            else
            {
                var codigoSituacao = (int)Enum.Parse<AluguelEntidade.SituacaoAluguel>(situacao);
                retornoRepositorio = await _aluguelRepositorio.ListarAlugueis(codigoUsuarioLocador, codigoUsuarioLocatario, codigoSituacao);
            }

            var alugueis = new List<AluguelEntidade>();
            foreach (var dto in retornoRepositorio)
            {
                alugueis.Add(new AluguelEntidade(dto));
            }

            return alugueis;
        }

        public async Task<AluguelEntidade> ObterAluguel(string chaveIdentificacaoReserva)
        {
            var dto = await _aluguelRepositorio.ObterAluguel(chaveIdentificacaoReserva);

            if (dto == null)
                return null;

            var aluguel = new AluguelEntidade(dto);
            return aluguel;
        }

        public async Task<AluguelEntidade> ObterAluguel(int codigoAluguel)
        {
            var dto = await _aluguelRepositorio.ObterAluguel(codigoAluguel);

            if (dto == null)
                return null;

            var aluguel = new AluguelEntidade(dto);
            return aluguel;
        }
    }

    public interface IAluguelServico
    {
        Task<CadastrarAluguelServicoResponseModel> CadastrarAluguel(AluguelEntidade aluguel);
        Task<List<AluguelEntidade>> ListarAlugueis(int? codigoUsuarioLocador = null, int? codigoUsuarioLocatario = null, string situacao = null);
        Task<AluguelEntidade> ObterAluguel(string chaveIdentificacaoReserva);
        Task<AluguelEntidade> ObterAluguel(int codigoAluguel);
        Task<AtualizarAluguelServicoResponseModel> AtualizarAluguel(int codigoAluguel, AlterarAluguelRequestModel requisicao);
    }
}
