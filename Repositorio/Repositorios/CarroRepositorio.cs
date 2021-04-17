using Repositorio.Dtos;
using Repositorio.Infraestrutura;
using Repositorio.Constantes;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class CarroRepositorio : ICarroRepositorio
    {
        private readonly DataBaseConnector _dataBase;

        public CarroRepositorio()
        {
            _dataBase = new DataBaseConnector();
        }

        public async Task CadastrarCarro(CarroDto carro)
        {
            await _dataBase.ExecutarAsync(AppConstants.SQL_CADASTRAR_CARRO, new 
            { 
                placa_carro =  carro.Placa,
                modelo_carro = carro.Modelo,
                marca_carro = carro.Marca,
                cor_carro = carro.Cor,
                numero_aassentos_carro = carro.NumeroDeAssentos,
                disponibilidade_carro = carro.Disponibilidade,
                descricao_carro = carro.Descricao,
                imagem_carro = carro.Imagem,
                ano_carro = carro.Ano,
                preco_diaria_carro = carro.PrecoDaDiaria,
                id_usuario = carro.CodigoDoUsuarioDoDono
            });
        }
    }

    public interface ICarroRepositorio
    {
        Task CadastrarCarro(CarroDto carro);
    }
}
