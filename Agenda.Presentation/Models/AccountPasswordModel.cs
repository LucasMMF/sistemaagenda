using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para a página de recuperação de senha do usuário
    /// </summary>
    public class AccountPasswordModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de emaill válido.")]
        [Required(ErrorMessage = "Por favor, informe o seu email de acesso.")]
        public string Email { get; set; }

    }
}
