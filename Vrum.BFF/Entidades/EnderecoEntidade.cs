using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Entidades
{
    public class EnderecoEntidade
    {
        public int Codigo { get; set; }        
        public string ChaveIdentificacao { get; set; }
        public string Cep { get; set; }
        public string Localidade { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
    }
}
