using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace IrisSandbox.Connection.Interfaces
{
    /// <summary>
    /// Interface para o gerenciamento da conexão com o banco de dados
    /// </summary>
    public interface IDbSandboxConnection : IDisposable
    {
        /// <summary>
        /// Aplica a conexão no DbContext do EntityFramework
        /// </summary>
        void UseDatabaseEF(DbContextOptionsBuilder optionsBuilder);

        /// <summary>
        /// Objeto de conexão com o banco de dados (SQLServer, PostgreSQL, Odbc, etc)
        /// </summary>
        DbConnection Connection { get; }

        /// <summary>
        /// Verifica se a conexão com o banco está com status aberto
        /// </summary>
        bool IsConexaoAberta();

        /// <summary>
        /// Solicita a abertura da conexão com o banco e retorna se houve sucesso
        /// </summary>
        bool Abre();

        /// <summary>
        /// Solicita o encerramento da conexão com o banco e retorna se houve sucesso
        /// </summary>
        bool Fecha();
    }
}
