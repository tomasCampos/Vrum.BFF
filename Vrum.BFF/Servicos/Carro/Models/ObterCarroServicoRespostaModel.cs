﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vrum.BFF.Entidades;

namespace Vrum.BFF.Servicos.Carro.Models
{
    public class ObterCarroServicoRespostaModel : RespostaServicoBase
    {
        public ObterCarroServicoRespostaModel(CarroEntidade carro) : base()
        {
            Carro = carro;
        }

        public ObterCarroServicoRespostaModel(string mensagemErro) : base(mensagemErro) { }

        public CarroEntidade Carro { get; set; }
    }
}
