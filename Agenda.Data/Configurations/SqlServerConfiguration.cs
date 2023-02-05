using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Data.Configurations
{
    /// <summary>
    /// Classe para configuração da ConnectionString.
    /// </summary>
    public class SqlServerConfiguration
    {
        /// <summary>
        /// Método para retornar a ConnectionString do Banco de Dados.
        /// </summary>
        public static string GetConnectionString
            => @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDAgenda;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}
