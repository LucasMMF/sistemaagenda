using Agenda.Data.Repositories;
using Agenda.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Agenda.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // ROTA: /Home/Index
        public IActionResult Index()
        {
            var lista = new List<DashboardModel>();

            try
            {
                // Passo 1: Capturar o usuário autenticado no sistema (Cookie)
                var json = User.Identity.Name;
                var usuario = JsonConvert.DeserializeObject<AuthenticationModel>(json);

                // Passo 2: Pesquisar todos os contatos cadastrados deste usuário no banco de dados
                var contatoRepository = new ContatoRepository();
                var contatos = contatoRepository.GetAllByUsuario(usuario.IdUsuario);

                // Passo 3: Totalizar os dados que serão exibidos no dashboard
                var totalAmigos = 0;
                var totalFamilia = 0;
                var totalTrabalho = 0;
                var totalOutros = 0;

                foreach (var item in contatos)
                {
                    if(item.Tipo == 1)
                        totalAmigos++;
                    else if(item.Tipo == 2)
                        totalTrabalho++;
                    else if(item.Tipo == 3)
                        totalFamilia++;
                    else
                        totalOutros++;
                }

                lista.Add(new DashboardModel { TipoContato = "Amigos", Quantidade = totalAmigos });
                lista.Add(new DashboardModel { TipoContato = "Trabalho", Quantidade = totalTrabalho });
                lista.Add(new DashboardModel { TipoContato = "Família", Quantidade = totalFamilia });
                lista.Add(new DashboardModel { TipoContato = "Outros", Quantidade = totalOutros });
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = $"Falha ao gerar dashboard: {e.Message}";
            }

            return View(lista);
        }
    }
}
