using System.Collections.Generic;
using System.Linq;
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
        public CadastrarEnderecoRequestModel EnderecoUsuario { get; set; }

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

            if (EnderecoUsuario != null)
            {
                if (string.IsNullOrWhiteSpace(EnderecoUsuario.Cep))
                    erros.Add("O campo CEP do endereço do usuário deve ser preenchido");
                if (string.IsNullOrWhiteSpace(EnderecoUsuario.Localidade))
                    erros.Add("O campo localidade do endereço do usuário deve ser preenchido");
                if (string.IsNullOrWhiteSpace(EnderecoUsuario.Logradouro))
                    erros.Add("O campo logradouro do endereço do usuário deve ser preenchido");
                if (string.IsNullOrWhiteSpace(EnderecoUsuario.Numero))
                    erros.Add("O campo número do endereço do usuário deve ser preenchido");
                if (string.IsNullOrWhiteSpace(EnderecoUsuario.Uf))
                    erros.Add("O campo UF do endereço do usuário deve ser preenchido");
            }

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }

        public UsuarioEntidade CriarEntidade()
        {
            var codigoPerfil = string.Equals("LOCADOR", PerfilUsuario) ? 0 : 1;
            var Entidade =  new UsuarioEntidade(EmailUsuario, SenhaUsuario, NomeUsuario, CpfUsuario, NumeroTelefoneUsuario, codigoPerfil);

            if (EnderecoUsuario != null)
            {
                Entidade.Endereco = new EnderecoEntidade
                {
                    Bairro = EnderecoUsuario.Bairro,
                    Cep = EnderecoUsuario.Cep,
                    Complemento = EnderecoUsuario.Complemento,
                    Logradouro = EnderecoUsuario.Logradouro,
                    Numero = EnderecoUsuario.Numero,
                    Uf = EnderecoUsuario.Uf,
                    Localidade = EnderecoUsuario.Localidade
                };
            }

            return Entidade;
        }
    }

    public class CadastrarEnderecoRequestModel
    {
        public string Cep { get; set; }
        public string Localidade { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
    }
}
