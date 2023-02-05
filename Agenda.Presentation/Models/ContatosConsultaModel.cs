namespace Agenda.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para a página de consulta de contatos
    /// </summary>
    public class ContatosConsultaModel
    {
        #region Propriedades

        public Guid IdContato { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string DataNascimento { get; set; }
        public string Tipo { get; set; }

        #endregion
    }
}
