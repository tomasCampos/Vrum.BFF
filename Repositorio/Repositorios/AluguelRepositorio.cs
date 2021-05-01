using Repositorio.Constantes;
using Repositorio.Dtos;
using Repositorio.Infraestrutura;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class AluguelRepositorio : IAluguelRepositorio
    {
        private readonly DataBaseConnector _dataBase;
        public AluguelRepositorio()
        {
            _dataBase = new DataBaseConnector();
        }

        public async Task CadastrarAluguel(AluguelDto aluguel)
        {
            await _dataBase.ExecutarAsync(AppConstants.SQL_CADASTRAR_ALUGUEL, new 
            {
                chave_reserva_aluguel = aluguel.ChaveIdentificacaoReserva,
                data_ini_reserva_aluguel = aluguel.DataInicioReserva,
                data_fim_reserva_aluguel = aluguel.DataFimReserva,
                preco_total_aluguel = aluguel.PrecoTotal,
                id_usuario = aluguel.CodigoUsuarioLocatario,
                id_carro = aluguel.CodigoCarroAlugado
            });               
        }

        public async Task<List<AluguelDto>> ListarAlugueis(int? codigoUsuarioLocador = null, int? codigoUsuarioLocatario = null, int? codigoSituacao = null)
        {
            var filtro = string.Empty;

            if (codigoUsuarioLocador.HasValue)
                filtro += $" AND c.id_usuario = {codigoUsuarioLocador.Value}";
            if (codigoUsuarioLocatario.HasValue)
                filtro += $" AND u.id_usuario = {codigoUsuarioLocatario.Value}";
            if (codigoSituacao.HasValue)
                filtro += $" AND a.situacao_aluguel = {codigoSituacao}";

            var query = string.Format(AppConstants.SQL_LISTAR_ALUGUEL, filtro);
            var alugueis = await _dataBase.SelecionarAsync<AluguelDto>(query);

            return alugueis.ToList();
        }

        public async Task<AluguelDto> ObterAluguel(string chaveIdentificacaoReserva)
        {
            var filtro = $" AND a.chave_reserva_aluguel = '{chaveIdentificacaoReserva}'";
            var query = string.Format(AppConstants.SQL_LISTAR_ALUGUEL, filtro);
            var aluguel = await _dataBase.SelecionarAsync<AluguelDto>(query);

            return aluguel.FirstOrDefault();
        }

        public async Task<AluguelDto> ObterAluguel(int codigoAluguel)
        {
            var filtro = $" AND a.id_aluguel = {codigoAluguel}";
            var query = string.Format(AppConstants.SQL_LISTAR_ALUGUEL, filtro);
            var aluguel = await _dataBase.SelecionarAsync<AluguelDto>(query);

            return aluguel.FirstOrDefault();
        }
    }

    public interface IAluguelRepositorio
    {
        Task CadastrarAluguel(AluguelDto aluguel);
        Task<List<AluguelDto>> ListarAlugueis(int? codigoUsuarioLocador = null, int? codigoUsuarioLocatario = null, int? codigoSituacao = null);
        Task<AluguelDto> ObterAluguel(string chaveIdentificacaoReserva);
        Task<AluguelDto> ObterAluguel(int codigoAluguel);
    }
}
