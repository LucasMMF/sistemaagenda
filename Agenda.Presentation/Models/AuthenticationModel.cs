namespace Agenda.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para as informações do usuário autenticado
    /// que serão gravadas no COOKIE de autenticação do AspNet
    /// </summary>
    public class AuthenticationModel
    {
        #region Propriedades
        
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataHoraAcesso { get; set; }

        #endregion
    }
}
