﻿namespace Vrum.BFF.Servicos
{
    public abstract class RespostaServicoBase
    {
        protected RespostaServicoBase()
        {
            Mensagem = null;
            Sucesso = true;
        }

        protected RespostaServicoBase(string mensagemErro)
        {
            Mensagem = mensagemErro;
            Sucesso = false;
        }

        public bool Sucesso { get; }
        public string Mensagem { get; }
    }
}
