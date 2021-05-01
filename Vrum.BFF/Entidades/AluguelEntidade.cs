using Repositorio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Entidades
{
    public class AluguelEntidade
    {
        public AluguelEntidade()
        {

        }

        public AluguelEntidade(AluguelDto aluguelDto)
        {
            Codigo = aluguelDto.Codigo;
            ChaveIdentificacaoReserva = aluguelDto.ChaveIdentificacaoReserva;
            DataInicioReserva = aluguelDto.DataInicioReserva;
            DataFimReserva = aluguelDto.DataFimReserva;
            DataDevolucaoCarro = aluguelDto.DataDevolucaoCarro;
            CodigoSituacao = aluguelDto.Situacao;
            CodigoUsuarioLocador = aluguelDto.CodigoUsuarioLocador;

            UsuarioLocatario = new Locatario
            {
                Codigo = aluguelDto.CodigoUsuarioLocatario,
                Cpf = aluguelDto.CpfUsuarioLocatario,
                Email = aluguelDto.EmailUsuarioLocatario,
                Nome = aluguelDto.NomeUsuarioLocatario,
                NumeroTelefone = aluguelDto.NumeroTelefoneUsuarioLocatario
            };

            CarroAlugado = new CarroAluguel 
            {
                Codigo = aluguelDto.CodigoCarroAlugado,
                NumeroAssentos = aluguelDto.NumeroDeAssentosCarroAlugado,
                Marca = aluguelDto.MarcaCarroAlugado,
                Modelo = aluguelDto.ModeloCarroAlugado,
                PrecoDiaria = aluguelDto.PrecoDaDiariaCarroAlugado
            };
        }

        public int Codigo { get; set; }
        public string ChaveIdentificacaoReserva { get; set; }
        public DateTime DataInicioReserva { get; set; }
        public DateTime DataFimReserva { get; set; }
        public DateTime DataDevolucaoCarro { get; set; }

        public string Situacao
        {
            get
            {
                var a = (SituacaoAluguel)this.CodigoSituacao;
                return a.ToString();
            }

            private set { }
        }
        private int CodigoSituacao { get; set; }
        public double PrecoTotal { get; set; }
        public Locatario UsuarioLocatario { get; set; }
        public CarroAluguel CarroAlugado { get; set; }
        public int CodigoUsuarioLocador { get; set; }

        public enum SituacaoAluguel
        {
            PENDENTE,
            EM_ANDAMENTO,
            REJEITADO,
            FINALIZADO
        }

        public int ObterCodigoSituacao()
        {
            return (int)Enum.Parse<SituacaoAluguel>(Situacao);
        }

        public class Locatario
        {
            public int Codigo { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Cpf { get; set; }
            public string NumeroTelefone { get; set; }
        }

        public class Locador
        {
            public int Codigo { get; set; }
        }

        public class CarroAluguel
        {
            public int Codigo { get; set; }
            public int NumeroAssentos { get; set; }
            public double PrecoDiaria { get; set; }
            public string Marca { get; set; }
            public string Modelo { get; set; }
        }
    }
}
