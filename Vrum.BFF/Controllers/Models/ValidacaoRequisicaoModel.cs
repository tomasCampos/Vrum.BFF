using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Controllers.Models
{
    public class ValidacaoRequisicaoModel
    {
        public bool Valido { get; set; }
        public List<string> Erros { get; set; }
        public string MensagemDeErro { get { return string.Join(", ", Erros); } private set { } }
    }
}
