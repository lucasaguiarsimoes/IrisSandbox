namespace IrisSandbox.Models.Interfaces
{
    /// <summary>
    /// Interface que representa uma entidade do sistema na base de dados versionada para tratamento de concorrência em edições
    /// </summary>
    public interface IEntityVersioned : IEntity
    {
        /// <summary>
        /// Versão de edição para controle de concorrência
        /// </summary>
        byte[]? RowVersion { get; set; }
    }
}
