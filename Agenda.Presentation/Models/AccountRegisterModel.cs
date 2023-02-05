using Agenda.Presentation.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para capturar os campos 
    /// do formulário de cadastro de usuário
    /// </summary>
    public class AccountRegisterModel
    {
        #region Propriedades

        [RegularExpression("^[A-Za-zÀ-Üà-ü\\s]{6,150}$",
            ErrorMessage = "Por favor, informe um nome válido de 6 a 150 caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o seu nome.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o seu email.")]
        public string Email { get; set; }

        [PasswordValidator(ErrorMessage = "Informe de 8 a 20 caracteres com pelo menos 1 letra minúscula, 1 letra maiúscula, 1 número e 1 caractere especial.")]
        [Required(ErrorMessage = "Por favor, informe a sua senha.")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem, por favor verifique.")]
        [Required(ErrorMessage = "Por favor, confirme a sua senha.")]
        public string SenhaConfirmacao { get; set; }

        #endregion

    }
}
