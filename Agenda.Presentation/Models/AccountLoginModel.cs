using Agenda.Presentation.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para a página de login de usuário
    /// </summary>
    public class AccountLoginModel
    {

        #region Propriedades

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de e-mail válido.")]
        [Required(ErrorMessage = "Por favor, informe seu email de acesso.")]
        public string Email { get; set; }

        [PasswordValidator(ErrorMessage = "Senha inválida.")]
        [Required(ErrorMessage = "Por favor, informe sua senha de acesso.")]
        public string Senha { get; set; }

        #endregion

    }
}
