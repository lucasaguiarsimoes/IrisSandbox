namespace IrisSandbox.Attributes
{
    /// <summary>
    /// Atributo que sinaliza que Data e Hora é absoluta, ou seja, valor definido aqui é em UTC e será utilizado nesse formato independente de variações de fuso.
    /// Exemplo: Data de nascimento, vencimento e etc.
    /// </summary>
    public class UtcDatetimeAttribute : Attribute
    {
        /// <summary>
        /// Se informado, seta o formato a ser utilizado no Histórico do sistema para a propriedade específica
        /// </summary>
        public string? HistoryFormat { get; set; }

        public UtcDatetimeAttribute()
        {
        }
    }
}
