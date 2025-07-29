using IrisSandbox.Enums;
using IrisSandbox.Models.Interfaces;

namespace IrisSandbox.Models.Common
{
    /// <summary>
    /// Classe que representa uma entidade de histórico do sistema na base de dados
    /// </summary>
    public class EntityHistory<TEntity> : Entity<TEntity>, IEntityHistory
        where TEntity : IEntity, new()
    {
        /// <summary>
        /// Identificador que representa uma ação de alteração no banco de dados. Esse identificador poderá ser repetido para diferentes itens de histórico que foram salvos na mesma ação/edição
        /// </summary>
        public string ActionGuid { get; set; } = null!;

        /// <summary>
        /// Identificador respectivo à entidade que se está registrando o histórico
        /// </summary>
        public long? PrimaryID { get; set; }

        /// <summary>
        /// Identificador da entidade principal que se está registrando o histórico
        /// Se uma sub-entidade estiver sendo gravada, o valor gravado será o identificador da entidade principal
        /// </summary>
        public long? PrincipalEntityID { get; set; }

        /// <summary>
        /// Nome da entidade que registrou os dados de histórico
        /// </summary>
        public string EntityName { get; set; } = null!;

        /// <summary>
        /// Data e hora da alteração que gerou o ponto de histórico
        /// </summary>
        public DateTimeOffset DataHora { get; set; }

        /// <summary>
        /// Usuário que provocou a alteração que gerou o ponto de histórico, se houver
        /// </summary>
        public long? UsuarioID { get; set; }

        /// <summary>
        /// Estado da entidade aplicado na alteração
        /// </summary>
        public EntityHistoryStateEnum State { get; set; }

        /// <summary>
        /// Comando do sistema que provocou a alteração que gerou o ponto de histórico, se houver
        /// </summary>
        public string? Command { get; set; }

        /// <summary>
        /// Chaves estrangeiras da entidade
        /// </summary>
        public string? ForeignKeys { get; set; }

        /// <summary>
        /// Valores anteriores à modificação realizada. Existirão valores anteriores em caso de edição e remoção da entidade
        /// </summary>
        public string? OldValues { get; set; }

        /// <summary>
        /// Valores novos aplicados na modificação realizada. Existirão valores novos em caso de edição e inclusão da entidade
        /// </summary>
        public string? NewValues { get; set; }
    }
}
