namespace IrisSandbox.Enums
{
    /// <summary>
    /// Enum para listar os tipos de estado de alteração de uma entidade
    /// </summary>
    public enum EntityHistoryStateEnum
    {
        /// <summary>
        /// Representa que a entidade em questão foi criada
        /// </summary>
        Added = 1,

        /// <summary>
        /// Representa que a entidade em questão foi modificada
        /// </summary>
        Modified = 2,

        /// <summary>
        /// Representa que a entidade em questão foi apagada
        /// </summary>
        Deleted = 3
    }
}
