using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para a página de cadastro de contatos
    /// </summary>
    public class ContatosCadastroModel
    {
        #region Propriedades

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do contato.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o telefone do contato.")]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de e-mail válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do contato.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de nascimento do contato.")]
        public string DataNascimento { get; set; }

        [Required(ErrorMessage = "Por favor, selecione o tipo do contato.")]
        public string Tipo { get; set; }

        #endregion

    }
}
