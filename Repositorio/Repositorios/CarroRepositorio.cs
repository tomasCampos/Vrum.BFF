using Repositorio.Dtos;
using Repositorio.Infraestrutura;
using Repositorio.Constantes;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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
                numero_assentos_carro = carro.NumeroDeAssentos,
                disponibilidade_carro = carro.Disponibilidade,
                descricao_carro = carro.Descricao,
                imagem_carro = carro.Imagem,
                ano_carro = carro.Ano,
                preco_diaria_carro = carro.PrecoDaDiaria,
                id_usuario = carro.CodigoDoUsuarioDoDono
            });
        }

        public async Task<List<CarroDto>> ListarCarros(string modelo, int? ano, string marca, string placa, int? codigo, string cidade, string estado, int? codigoUsuarioDonoDoCarro)
        {
            var filtro = string.Empty;

            if (!string.IsNullOrEmpty(modelo))
                filtro += $" AND c.modelo_carro LIKE '%{modelo}%'";
            if (ano.HasValue)
                filtro += $" AND c.ano_carro = {ano.Value}";
            if (!string.IsNullOrEmpty(marca))
                filtro += $" AND c.marca_carro LIKE '%{marca}%'";
            if (!string.IsNullOrEmpty(placa))
                filtro += $" AND c.placa_carro = '{placa}'";
            if(codigo.HasValue)
                filtro = $" AND c.carro_id = {codigo.Value}";
            if (!string.IsNullOrEmpty(cidade))
                filtro += $" AND e.logradouro_endereco LIKE '%{cidade}%'";
            if (!string.IsNullOrEmpty(estado))
                filtro += $" AND e.uf_endereco = '{estado}'";
            if (codigoUsuarioDonoDoCarro.HasValue)
                filtro += $" AND u.id_usuario = {codigoUsuarioDonoDoCarro}";

            var query = string.Format(AppConstants.SQL_LISTAR_CARRO, filtro);
            var carros = await _dataBase.SelecionarAsync<CarroDto>(query);

            return carros.ToList();
        }
    }

    public interface ICarroRepositorio
    {
        Task CadastrarCarro(CarroDto carro);

        Task<List<CarroDto>> ListarCarros(string modelo, int? ano, string marca, string placa, int? codigo, string cidade, string estado, int? codigoUsuarioDonoDoCarro);
    }
}
