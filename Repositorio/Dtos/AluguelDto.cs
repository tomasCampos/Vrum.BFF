using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Dtos
{
    public class AluguelDto
    {
        public int Codigo { get; set; }
        public string ChaveIdentificacaoReserva { get; set; }
        public DateTime DataInicioReserva { get; set; }
        public DateTime DataFimReserva { get; set; }
        public DateTime DataDevolucaoCarro { get; set; }
        public int Situacao { get; set; }
        public double PrecoTotal { get; set; }
        public int CodigoUsuarioLocatario { get; set; }
        public string NomeUsuarioLocatario { get; set; }
        public string CpfUsuarioLocatario { get; set; }
        public string EmailUsuarioLocatario { get; set; }
        public string NumeroTelefoneUsuarioLocatario { get; set; }
        public int CodigoCarroAlugado { get; set; }
        public string PlacaCarroAlugado { get; set; }
        public string MarcaCarroAlugado { get; set; }
        public string ModeloCarroAlugado { get; set; }
        public int NumeroDeAssentosCarroAlugado { get; set; }
        public double PrecoDaDiariaCarroAlugado { get; set; }
        public int CodigoUsuarioLocador { get; set; }
    }
}
