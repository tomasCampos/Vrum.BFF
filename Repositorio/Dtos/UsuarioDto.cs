using System;

namespace Repositorio.Dtos
{
    public class UsuarioDto
    {     
        public int Codigo { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string NumeroTelefone { get; set; }
        public int Perfil { get; set; }
        public DateTime DataUltimaAlteracaoAlteracao { get; set; }
        public DateTime DataCriacaoRegistro { get; set; }        
    }
}

