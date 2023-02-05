using Agenda.Data.Configurations;
using Agenda.Data.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Data.Repositories
{
    /// <summary>
    /// Classe de repositório de dados para contato
    /// </summary>
    public class ContatoRepository
    {
        public void Create(Contato contato)
        {
            var query = @"
                INSERT INTO CONTATO(
                    IDCONTATO,
                    NOME,
                    TELEFONE,
                    EMAIL,
                    DATANASCIMENTO,
                    TIPO,
                    IDUSUARIO)
                VALUES(
                    @IdContato,
                    @Nome,
                    @Telefone,
                    @Email,
                    @DataNascimento,
                    @Tipo,
                    @IdUsuario
                )
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                connection.Execute(query, contato);
            }
        }

        public void Update(Contato contato)
        {
            var query = @"
                UPDATE CONTATO
                SET
                    NOME = @Nome,
                    TELEFONE = @Telefone,
                    EMAIL = @Email,
                    DATANASCIMENTO = @DataNascimento,
                    TIPO = @Tipo
                WHERE
                    IDCONTATO = @IdContato
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                connection.Execute(query, contato);
            }
        }

        public void Delete(Contato contato)
        {
            var query = @"
                DELETE FROM CONTATO
                WHERE IDCONTATO = @IdContato
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                connection.Execute(query, contato);
            }
        }

        public List<Contato> GetAllByUsuario(Guid idUsuario)
        {
            var query = @"
                SELECT * FROM CONTATO
                WHERE IDUSUARIO = @idUsuario
                ORDER BY NOME
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                return connection.Query<Contato>(query, new { idUsuario }).ToList();
            }
        }

        public Contato? GetById(Guid idContato)
        {
            var query = @"
                SELECT * FROM CONTATO
                WHERE IDCONTATO = @idContato
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                return connection.Query<Contato>(query, new { idContato }).FirstOrDefault();
            }
        }
    }
}
