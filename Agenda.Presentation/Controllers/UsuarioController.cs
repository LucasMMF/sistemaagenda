using Agenda.Data.Repositories;
using Agenda.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Agenda.Presentation.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        // ROTA: /Usuario/MinhaConta
        public IActionResult MinhaConta()
        {
            return View();
        }

        // POST: /Usuario/MinhaConta (submit do formulário)
        [HttpPost] // recebe o submit post do formulário
        public IActionResult MinhaConta(UsuarioMinhaContaModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    // capturando os dados do usuário autenticado
                    var json = User.Identity.Name;
                    var auth = JsonConvert.DeserializeObject<AuthenticationModel>(json);

                    // atualizando a senha do usuário no banco de dados
                    var usuarioRepository = new UsuarioRepository();
                    usuarioRepository.Update(auth.IdUsuario, model.NovaSenha);

                    TempData["MensagemSucesso"] = "Sua senha foi atualizada com sucesso.";
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = $"Ocorreu um erro: {e.Message}";
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preenchimento do formulário, por favor verifique.";
            }

            return View();
        }
    }
}
