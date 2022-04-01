﻿using System.Collections.Generic;

namespace Vrum.BFF.Controllers.Models
{
    public class ValidacaoRequisicaoModel
    {
        public bool Valido { get; set; }
        public List<string> Erros { get; set; }
        public string MensagemDeErro { get { return string.Join(", ", Erros); } private set { } }
    }
}
