using Repositorio.Dtos;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vrum.BFF.Entidades;
using Vrum.BFF.Servicos.Carro.Models;

namespace Vrum.BFF.Servicos.Carro
{
    public class CarroServico : ICarroServico
    {
        private readonly ICarroRepositorio _carroRepositorio;

        public CarroServico(ICarroRepositorio carroRepositorio)
        {
            _carroRepositorio = carroRepositorio;
        }

        public async Task<CadastrarCarroServicoRespostaModel> CadastrarUsuario(CarroEntidade carro)
        {
            var carroJaExiste = await ObterCarro(carro.Placa) != null;
            if (carroJaExiste)
                return new CadastrarCarroServicoRespostaModel("Um carro com essa placa já foi cadastrado. Verifique a placa e tente novamente.");

            var carroDto = new CarroDto()
            {
                Codigo = carro.Codigo,
                Placa = carro.Placa,
                Marca = carro.Marca,
                Modelo = carro.Modelo,
                Ano = carro.Ano,
                NumeroDeAssentos = carro.NumeroDeAssentos,
                PrecoDaDiaria = carro.PrecoDaDiaria,
                Cor = carro.Cor,
                Descricao = carro.Descricao,
                Disponibilidade = carro.Disponibilidade,
                Imagem = carro.Imagem,
                CodigoDoUsuarioDoDono = carro.Dono.CodigoUsuario,
                NomeDoDono = carro.Dono.Nome,
                TelefoneDoDono = carro.Dono.Telefone,
                EmailDoDono = carro.Dono.Email,
                EstadoLocalizacao = carro.Dono.Estado,
                Localizacao = carro.Dono.Endereco,
            };
            await _carroRepositorio.CadastrarCarro(carroDto);

            var respostaObterCarro = await ObterCarro(carro.Placa);
            return new CadastrarCarroServicoRespostaModel(respostaObterCarro.Carro.Codigo);
        }

        public async Task<List<CarroEntidade>> ListarCarros(string modelo, int? ano, string marca)
        {
            var carrosDto = await _carroRepositorio.ListarCarros(modelo, ano, marca, null, null);

            var resposta = new List<CarroEntidade>();
            foreach (var dto in carrosDto)
            {
                resposta.Add(new CarroEntidade(dto));
            }

            return resposta;
        }

        public async Task<ObterCarroServicoRespostaModel> ObterCarro(int codigo)
        {
            var carrosDto = await _carroRepositorio.ListarCarros(null, null, null, null, codigo);
            var carroDto = carrosDto.FirstOrDefault();

            if (carroDto == null)
                return new ObterCarroServicoRespostaModel("Não existe um carro com o código especificado.");

            var carro = new CarroEntidade(carroDto);
            var resposta = new ObterCarroServicoRespostaModel(carro);
            return resposta;
        }

        public async Task<ObterCarroServicoRespostaModel> ObterCarro(string placa)
        {
            var carrosDto = await _carroRepositorio.ListarCarros(null, null, null, placa, null);
            var carroDto = carrosDto.FirstOrDefault();

            if (carroDto == null)
                return new ObterCarroServicoRespostaModel("Não existe um carro com a placa especificada.");

            var carro = new CarroEntidade(carroDto);
            var resposta = new ObterCarroServicoRespostaModel(carro);
            return resposta;
        }
    }

    public interface ICarroServico
    {
        Task<CadastrarCarroServicoRespostaModel> CadastrarUsuario(CarroEntidade usuario);

        Task<List<CarroEntidade>> ListarCarros(string modelo, int? ano, string marca);

        Task<ObterCarroServicoRespostaModel> ObterCarro(int codigo);

        Task<ObterCarroServicoRespostaModel> ObterCarro(string placa);
    }
}
