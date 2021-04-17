﻿using Repositorio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrum.BFF.Entidades
{
    public class CarroEntidade
    {
        public CarroEntidade(CarroDto carroDto)
        {
            Codigo = carroDto.Codigo;
            Placa = carroDto.Placa;
            Modelo = carroDto.Modelo;
            Marca = carroDto.Marca;
            Cor = carroDto.Cor;
            NumeroDeAssentos = carroDto.NumeroDeAssentos;
            Disponibilidade = carroDto.Disponibilidade;
            Descricao = carroDto.Descricao;
            Imagem = carroDto.Imagem;
            Ano = carroDto.Ano;
            PrecoDaDiaria = carroDto.PrecoDaDiaria;

            Dono = new DonoDocarro
            {
                CodigoUsuario = carroDto.CodigoDoUsuarioDoDono,
                Nome = carroDto.NomeDoDono,
                Telefone = carroDto.TelefoneDoDono,
                Email = carroDto.EmailDoDono,
                Endereco = carroDto.Localizacao,
                Estado = carroDto.EstadoLocalizacao
            };
        }

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
        public DonoDocarro Dono { get; set; }
    }

    public class DonoDocarro
    {
        public int CodigoUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
    }
}