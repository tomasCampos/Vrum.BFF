using System.Collections.Generic;
using System.Linq;
using Vrum.BFF.Entidades;

namespace Vrum.BFF.Controllers.Models.Carro
{
    public class CadastrarCarroRequestModel : IRequestModel
    {
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Cor { get; set; }
        public int NumeroDeAssentos { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public int? Ano { get; set; }
        public double PrecoDaDiaria { get; set; }
        public int CodigoUsuarioDonoDoCarro { get; set; }

        public ValidacaoRequisicaoModel Validar()
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(Placa))
                erros.Add("O campo placa do carro deve ser preenchido");
            if (string.IsNullOrWhiteSpace(Modelo))
                erros.Add("O campo modelo do carro deve ser preenchido");
            if (string.IsNullOrWhiteSpace(Marca))
                erros.Add("O marca do carro deve ser preenchido");
            if (string.IsNullOrWhiteSpace(Cor))
                erros.Add("O campo cor do carro deve ser preenchido");
            if (NumeroDeAssentos <= 0)
                erros.Add("O campo numero de assentos do carro está inválido");
            if (PrecoDaDiaria <= 0)
                erros.Add("O campo preço da diária está inválido");
            if (CodigoUsuarioDonoDoCarro <= 0)
                erros.Add("O campo código do usuário dono do carro está inválido");
            if (Ano.HasValue && Ano.Value <= 0)
                erros.Add("O campo ano do carro está inválido");

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }

        public CarroEntidade CriarEntidade()
        {
            var donoDoCarro = new DonoDocarro { CodigoUsuario = CodigoUsuarioDonoDoCarro };
            var entidade = new CarroEntidade(Placa, Modelo, Marca, Cor, NumeroDeAssentos, true, Descricao, Imagem, Ano, PrecoDaDiaria, donoDoCarro);
            return entidade;
        }
    }
}
