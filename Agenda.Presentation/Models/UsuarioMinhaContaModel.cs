using Agenda.Presentation.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para a página /Usuário/MinhaConta
    /// </summary>
    public class UsuarioMinhaContaModel
    {
        #region Propriedades

        [PasswordValidator(ErrorMessage = "Informe de 8 a 20 caracteres com pelo menos 1 letra minúscula, 1 letra maiúscula, 1 número e 1 caractere especial.")]
        [Required(ErrorMessage = "Por favor, informe a sua senha.")]
        public string NovaSenha { get; set; }

        [Compare("NovaSenha", ErrorMessage = "Senhas não conferem, por favor verifique.")]
        [Required(ErrorMessage = "Por favor, confirme a sua senha.")]
        public string NovaSenhaConfirmacao { get; set; }

        #endregion

    }
}
