namespace Pmesp_Api_Template2.Domain.Entidades
{
    public sealed class Resultado<TResultObject> where TResultObject : class
    {
        public TResultObject? resultado { get; set; }
        public string? MensagemRetorno { get; set; }
        public bool ExecutouComSucesso { get; set; }
        public string? Codigo { get; set; }
        public string? Detalhe { get; set; }
    }
}
