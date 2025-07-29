namespace IrisSandbox.Models.Interfaces
{
    /// <summary>
    /// Interface que representa uma entidade principal na base de dados versionada para tratamento de concorrência em edições
    /// </summary>
    public interface IEntityDefaultVersioned : IEntityVersioned, IEntityDefault
    {
    }
}
