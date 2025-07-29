using InterSystems.Data.IRISClient;
using IrisSandbox.Connection.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisSandbox.Connection
{
    /// <summary>
    /// Definição de uma conexão padrão para o banco IRIS
    /// </summary>
    public class IrisConnection : DbSandboxConnection, IDisposable
    {
        /// <summary>
        /// Objeto de conexão para acesso ao banco
        /// </summary>
        public override DbConnection Connection => _Connection;
        private readonly IRISConnection _Connection;

        public IrisConnection(string? connectionString)
            : base(connectionString)
        {
            _Connection = new IRISConnection(ConnectionString);
        }

        public override void UseDatabaseEF(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseIris(Connection);
        }
    }
}
