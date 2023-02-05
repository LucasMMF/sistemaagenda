using Agenda.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Reports.Interfaces
{
    /// <summary>
    /// Interface para padronizar os métodos para geração
    /// de relatórios de contatos
    /// </summary>
    public interface IContatosReport
    {
        // método abstrato!
        byte[] CreateReport(List<Contato> contatos, Usuario usuario);
    }
}
