using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Data.Entities
{
    /// <summary>
    /// Classe de modelo de dados para usuário
    /// </summary>
    public class Usuario
    {
        #region Propriedades

        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; }

        #endregion

        #region Relacionamentos

        public List<Contato> Contatos { get; set; }

        #endregion

    }
}
