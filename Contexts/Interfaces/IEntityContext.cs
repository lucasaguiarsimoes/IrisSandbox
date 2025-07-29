using System.Data.Common;

namespace IrisSandbox.Contexts.Interfaces
{
    /// <summary>
    /// Interface que representa um contexto de entidades com acesso à base de dados
    /// </summary>
    public interface IEntityContext
    {
        /// <summary>
        /// Abre e usa conexão com a base de dados neste contexto de entidades
        /// </summary>
        DbConnection UseConnection();

        /// <summary>
        /// Aciona a execução de todas as migrations pendentes para o contexto em questão
        /// </summary>
        Task MigrateAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Finaliza e desvincula o uso deste contexto
        /// </summary>
        void DetachContext();

        /// <summary>
        /// Renova o contexto de entidades limpando todos os tracking changes
        /// </summary>
        void DetachEntries();
    }
}
