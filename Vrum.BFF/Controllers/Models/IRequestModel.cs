using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Controllers.Models
{
    interface IRequestModel
    {
        public ValidacaoRequisicaoModel Validar();
    }
}
