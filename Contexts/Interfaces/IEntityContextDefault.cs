namespace IrisSandbox.Contexts.Interfaces
{
    /// <summary>
    /// Interface que representa um contexto de entidades com acesso à base de dados
    /// </summary>
    public interface IEntityContextDefault : IEntityContext
    {
        /// <summary>
        /// Aplica as mudanças registradas nas entidades para o banco de dados
        /// </summary>
        Task ApplyChangesAsync(CancellationToken cancellationToken);
    }
}
