using Repositorio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Entidades
{
    public class UsuarioEntidade
    {
        public UsuarioEntidade() {}

        public UsuarioEntidade(UsuarioDto usuarioDto)
        {
            Codigo = usuarioDto.Codigo;
            Email = usuarioDto.Email;
            Senha = usuarioDto.Senha;
            Nome = usuarioDto.Nome;
            Cpf = usuarioDto.Cpf;
            NumeroTelefone = usuarioDto.NumeroTelefone;
            CodigoPerfil = usuarioDto.Perfil;
        }

        public int Codigo { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string NumeroTelefone { get; set; }
        public string Perfil { 
        get 
        {
            var a = (PerfilUsuario)this.CodigoPerfil;
            return a.ToString();
        }}

        private int CodigoPerfil { get; set; }

        public enum PerfilUsuario 
        {
            LOCADOR,
            LOCATARIO
        }
    }
}
