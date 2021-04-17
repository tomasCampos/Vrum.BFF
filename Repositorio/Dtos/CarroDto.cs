using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Dtos
{
    public class CarroDto
    {        
        public int Codigo { get; set; }
	    public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Cor { get; set; }
        public string NumeroDeAssentos { get; set; }
        public string Disponibilidade { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public int? Ano { get; set; }
        public double PrecoDaDiaria { get; set; }
        public string NomeDoDono { get; set; }
        public string EmailDoDono { get; set; }
        public string TelefoneDoDono { get; set; }
        public string Localizacao { get; set; }
        public string EstadoLocalizacao { get; set; }
        public int CodigoDoUsuarioDoDono { get; set; }
    }
}
