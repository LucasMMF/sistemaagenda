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
    /// Classe de repositório de dados para usuário
    /// </summary>
    public class UsuarioRepository
    {
        /// <summary>
        /// Método para inserir um usuário no banco de dados
        /// </summary>
        public void Create(Usuario usuario)
        {
            var query = @"
                INSERT INTO USUARIO(
                    IDUSUARIO,
                    NOME,
                    EMAIL,
                    SENHA,
                    DATACRIACAO)
                VALUES(
                    @IdUsuario,
                    @Nome,
                    @Email,
                    CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2),
                    @DataCriacao
                )
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                connection.Execute(query, usuario);
            }
        }

        /// <summary>
        /// Método para atualizar somente a senha do usuário
        /// </summary>
        public void Update(Guid idUsuario, string novaSenha)
        {
            var query = @"
                UPDATE USUARIO
                SET SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @novaSenha), 2)
                WHERE IDUSUARIO = @idUsuario
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                connection.Execute(query,new { idUsuario, novaSenha });
            }
        }

        /// <summary>
        /// Método para consultar 1 usuário baseado no email
        /// </summary>
        public Usuario? GetByEmail(string email)
        {
            var query = @"
                SELECT * FROM USUARIO
                WHERE EMAIL = @email
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                return connection
                    .Query<Usuario>(query, new { email })
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Método para consultar 1 usuário baseado no email e senha
        /// </summary>
        public Usuario? GetByEmailAndSenha(string email, string senha)
        {
            var query = @"
                SELECT * FROM USUARIO
                WHERE EMAIL = @email
                AND SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @senha), 2)
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                return connection
                    .Query<Usuario>(query, new { email, senha })
                    .FirstOrDefault();
            }
        }

    }
}
