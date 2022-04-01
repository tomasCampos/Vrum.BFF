namespace Vrum.BFF.Servicos.Aluguel.Models
{
    public class ObterResumoAlugueisUsuarioServicoResponseModel : RespostaServicoBase
    {
        public ObterResumoAlugueisUsuarioServicoResponseModel(int pendentes, int emAndamento, int rejeitados, int finalizados) : base() 
        {
            QuantidadeDeAlugueisEmAndamento = emAndamento;
            QuantidadeDeAlugueisPendentes = pendentes;
            QuantidadeDeAlugueisRejeitados = rejeitados;
            QuantidadeDeAlugueisFinalizados = finalizados;
        }

        public ObterResumoAlugueisUsuarioServicoResponseModel(string mensagemErro) : base(mensagemErro) {}

        public int QuantidadeDeAlugueisPendentes { get; }
        public int QuantidadeDeAlugueisEmAndamento { get; } 
        public int QuantidadeDeAlugueisRejeitados { get; }
        public int QuantidadeDeAlugueisFinalizados { get; }
    }
}
