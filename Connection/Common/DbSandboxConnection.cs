using IrisSandbox.Connection.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace IrisSandbox.Connection.Common
{
    /// <summary>
    /// Classe que gerencia e representa uma conexão baseada em um DbConnection
    /// </summary>
    public abstract class DbSandboxConnection : IDbSandboxConnection,
        IDbSandboxConnectionDefault,
        IDbSandboxConnectionHistory
    {
        /// <summary>
        /// Aplica a conexão no DbContext do EntityFramework
        /// </summary>
        public abstract void UseDatabaseEF(DbContextOptionsBuilder optionsBuilder);

        /// <summary>
        /// Objeto de conexão com o banco de dados (SQLServer, PostgreSQL, Odbc, etc)
        /// </summary>
        public abstract DbConnection Connection { get; }

        /// <summary>
        /// Definição da string de conexão para o banco de dados
        /// </summary>
        protected string ConnectionString { get; }

        public DbSandboxConnection(string? connectionString)
        {
            // Valida a connectionstring crua com o que está no arquivo de configuração
            ConnectionString = ValidateConnectionString(connectionString);
        }

        /// <summary>
        /// Obtém a string de conexão com o banco de dados em uso
        /// </summary>
        protected virtual string ValidateConnectionString(string? connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception($"Empty Connection String");
            }

            return connectionString;
        }

        #region IDbLASConnection
        public virtual bool IsConexaoAberta()
        {
            return Connection.State.HasFlag(ConnectionState.Open);
        }

        public virtual bool Abre()
        {
            Connection.Open();

            return IsConexaoAberta();
        }

        public virtual bool Fecha()
        {
            Connection.Close();

            return Connection.State == ConnectionState.Closed;
        }

        public virtual void Dispose()
        {
            // Por garantia, se a conexão ainda estiver aberta ao realizar o Dispose do objeto de conexão,
            // primeiro fecha a conexão antes de fazer o Dispose da Connection.
            // Com isso, toda vez que uma conexão for utilizada, mesmo que de forma exclusiva e independente,
            // ao descartá-la, o fechamento da conexão será realizado adequadamente e de forma transparente.
            if (IsConexaoAberta())
            {
                Fecha();
            }

            Connection?.Dispose();
        }
        #endregion
    }
}
