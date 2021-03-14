using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vrum.BFF.Entidades;

namespace Vrum.BFF.Controllers.Models.Usuario
{
    public class CadastrarUsuarioRequestModel : IRequestModel
    {
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string PerfilUsuario { get; set; }
        public string CpfUsuario { get; set; }
        public string NumeroTelefoneUsuario { get; set; }

        public ValidacaoRequisicaoModel Validar()
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(EmailUsuario))
                erros.Add("O campo email do usuario deve ser preenchido");
            if (string.IsNullOrWhiteSpace(SenhaUsuario))
                erros.Add("O campo senha do usuario deve ser preenchido");
            if (string.IsNullOrWhiteSpace(NomeUsuario))
                erros.Add("O campo nome do usuario deve ser preenchido");
            if (string.IsNullOrWhiteSpace(PerfilUsuario))
                erros.Add("O campo perfil do usuario deve ser preenchido");
            if(!string.Equals(PerfilUsuario, "LOCATARIO") && !string.Equals(PerfilUsuario, "LOCADOR"))
                erros.Add("O campo perfil do usuario deve ser exadamente um dos: 'LOCADOR' ou 'LOCATARIO'");

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }

        public UsuarioEntidade CriarEntidade()
        {
            return new UsuarioEntidade
            {
                Email = EmailUsuario,
                Senha = SenhaUsuario,
                Nome = NomeUsuario,
                Cpf = CpfUsuario,
                NumeroTelefone = NumeroTelefoneUsuario,
                Perfil = PerfilUsuario
            };
        }
    }
}
