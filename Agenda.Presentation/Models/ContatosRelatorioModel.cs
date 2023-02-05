using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para a página de relatório de contatos
    /// </summary>
    public class ContatosRelatorioModel
    {
        [Required(ErrorMessage = "Por favor, selecione o formato do relatório.")]
        public string Formato { get; set; }

    }
}
