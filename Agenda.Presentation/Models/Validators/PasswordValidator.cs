using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models.Validators
{
    /// <summary>
    /// Classe de validação customizada para campos de senha
    /// </summary>
    public class PasswordValidator : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value != null)
            {
                // capturar o valor do campo que está sendo validado
                var password = value.ToString();

                return password.Length >= 8 // mínimmo de 8 caracteres
                    && password.Length <= 20 // máximo de 20 caracteres
                    && password.Any(char.IsUpper) // pelo menos 1 letra maiúscula
                    && password.Any(char.IsLower) // pelo menos 1 letra minúscula
                    && password.Any(char.IsDigit) // pelo menos 1 dígito numérico
                    && ( // pelo menos 1 dos caracteres especiais abaixo
                        password.Contains("!") ||
                        password.Contains("@") ||
                        password.Contains("#") ||
                        password.Contains("$") ||
                        password.Contains("%") ||
                        password.Contains("&")
                    );
            }

            return false;
        }
    }
}
