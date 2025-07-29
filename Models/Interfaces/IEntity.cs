namespace IrisSandbox.Models.Interfaces
{
    /// <summary>
    /// Interface que representa uma entidade do sistema na base de dados
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Identificador único gerado pela base de dados
        /// </summary>
        long? ID { get; set; }
    }
}
