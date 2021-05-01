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

            if (!string.IsNullOrEmpty(requisicao.Situacao) && !Enum.TryParse<SituacaoAluguel>(requisicao.Situacao, out _))
            {
                return new AtualizarAluguelServicoResponseModel("O campo situação deve ser uma das: PENDENTE, EM_ANDAMENTO, REJEITADO, FINALIZADO.", 
                    AtualizarAluguelServicoResponseModel.FalhasPossiveis.SITUACAO_INVALIDA);
            }

            if (aluguel.Situacao > 0 && requisicao.DataInicioReserva.HasValue)
            {
                return new AtualizarAluguelServicoResponseModel("Não é possível alterar a data de início do aluguel depois do carro ja ter sido retirado",
                    AtualizarAluguelServicoResponseModel.FalhasPossiveis.DATAS_INVALIDAS);
            }

            if (aluguel.Situacao > 0 && requisicao.CodigoCarroQueSeraAlugado.HasValue)
            {
                return new AtualizarAluguelServicoResponseModel("Não é possível trocar o carro alugado depois do aluguel ja ter sido aceito pelo locador",
                    AtualizarAluguelServicoResponseModel.FalhasPossiveis.SITUACAO_INVALIDA);
            }


            var novaDataInicioReserva = requisicao.DataInicioReserva.HasValue ? requisicao.DataInicioReserva.Value : aluguel.DataInicioReserva;
            var novaDataFimReserva = requisicao.DataFimReserva.HasValue ? requisicao.DataFimReserva.Value : aluguel.DataFimReserva;
            var novoCodigoCarroAlugado = requisicao.CodigoCarroQueSeraAlugado.HasValue ? requisicao.CodigoCarroQueSeraAlugado.Value : aluguel.CodigoCarroAlugado;
            var novaDataDevolucao = requisicao.DataDeDevolucaoDoCarro.HasValue ? requisicao.DataDeDevolucaoDoCarro : aluguel.DataDevolucaoCarro;
            var novoCodigoSituacao = !string.IsNullOrEmpty(requisicao.Situacao) ? (int)Enum.Parse<AluguelEntidade.SituacaoAluguel>(requisicao.Situacao) : aluguel.Situacao;

            if (aluguel.Situacao == 1 && novoCodigoSituacao == 2)
            {
                return new AtualizarAluguelServicoResponseModel("Um aluguel que já está em andamento não pode ser rejeitado, apenas finalizado.",
                    AtualizarAluguelServicoResponseModel.FalhasPossiveis.SITUACAO_INVALIDA);
            }

            if (aluguel.Situacao != 1 && novoCodigoSituacao == 3)
            {
                return new AtualizarAluguelServicoResponseModel("Apenas alugueis em andamento podem ser finalizados.",
                    AtualizarAluguelServicoResponseModel.FalhasPossiveis.SITUACAO_INVALIDA);
            }

            if (novoCodigoSituacao < aluguel.Situacao)
                return new AtualizarAluguelServicoResponseModel("As situações não podem retroceder.", AtualizarAluguelServicoResponseModel.FalhasPossiveis.SITUACAO_INVALIDA);

            if (novoCodigoSituacao == 3 && !novaDataDevolucao.HasValue)
            {
                return new AtualizarAluguelServicoResponseModel("Para que o aluguel seja encerrado é preciso informar a data de devolução do carro.",
                    AtualizarAluguelServicoResponseModel.FalhasPossiveis.DATAS_INVALIDAS);
            }

            if (novaDataDevolucao.HasValue && novoCodigoSituacao != 3)
            {
                return new AtualizarAluguelServicoResponseModel("Se o carro foi devolvido, a situacao do aluguel deve ser FINALIZADO",
                    AtualizarAluguelServicoResponseModel.FalhasPossiveis.SITUACAO_INVALIDA);
            }

            var novoCarro = await _carroServico.ObterCarro(novoCodigoCarroAlugado);
            if (!novoCarro.Sucesso)
                return new AtualizarAluguelServicoResponseModel("Carro não encontrado.", AtualizarAluguelServicoResponseModel.FalhasPossiveis.CARRO_NAO_EXISTE);

            var novoPrecoAluguel = novoCarro.Carro.PrecoDaDiaria * 
                (novaDataDevolucao.HasValue ? novaDataDevolucao.Value - novaDataInicioReserva : novaDataFimReserva - novaDataInicioReserva).TotalDays;

            await _aluguelRepositorio.AtualizarAluguel(codigoAluguel, novaDataInicioReserva, novaDataFimReserva, novaDataDevolucao, novoCodigoSituacao, novoPrecoAluguel, novoCodigoCarroAlugado);
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

        public async Task<ObterResumoAlugueisUsuarioServicoResponseModel> ObterResumoAluguelUsuario(int codigoUsuario)
        {
            var usuario = await _usuarioServico.ObterUsuario(codigoUsuario);

            if (!usuario.Sucesso)
                return new ObterResumoAlugueisUsuarioServicoResponseModel("Usuario não encontrado");

            if (usuario.Usuario.Perfil == "LOCADOR")
            {
                var listaAlugueis = await _aluguelRepositorio.ListarAlugueis(codigoUsuario);

                var alugueisPendentes = listaAlugueis.Count(a => a.Situacao == 0);
                var alugueisEmAndamento = listaAlugueis.Count(a => a.Situacao == 1);
                var alugueisRejeitados = listaAlugueis.Count(a => a.Situacao == 2);
                var alugueisFinalizados = listaAlugueis.Count(a => a.Situacao == 3);

                return new ObterResumoAlugueisUsuarioServicoResponseModel(alugueisPendentes, alugueisEmAndamento, alugueisRejeitados, alugueisFinalizados);
            }
            else
            {
                var listaAlugueis = await _aluguelRepositorio.ListarAlugueis(null, codigoUsuario);

                var alugueisPendentes = listaAlugueis.Count(a => a.Situacao == 0);
                var alugueisEmAndamento = listaAlugueis.Count(a => a.Situacao == 1);
                var alugueisRejeitados = listaAlugueis.Count(a => a.Situacao == 2);
                var alugueisFinalizados = listaAlugueis.Count(a => a.Situacao == 3);

                return new ObterResumoAlugueisUsuarioServicoResponseModel(alugueisPendentes, alugueisEmAndamento, alugueisRejeitados, alugueisFinalizados);
            }
        }
    }

    public interface IAluguelServico
    {
        Task<CadastrarAluguelServicoResponseModel> CadastrarAluguel(AluguelEntidade aluguel);
        Task<List<AluguelEntidade>> ListarAlugueis(int? codigoUsuarioLocador = null, int? codigoUsuarioLocatario = null, string situacao = null);
        Task<AluguelEntidade> ObterAluguel(string chaveIdentificacaoReserva);
        Task<AluguelEntidade> ObterAluguel(int codigoAluguel);
        Task<AtualizarAluguelServicoResponseModel> AtualizarAluguel(int codigoAluguel, AlterarAluguelRequestModel requisicao);
        Task<ObterResumoAlugueisUsuarioServicoResponseModel> ObterResumoAluguelUsuario(int codigoUsuario);
    }
}
