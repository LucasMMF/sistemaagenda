using Agenda.Data.Entities;
using Agenda.Data.Repositories;
using Agenda.Messages.Services;
using Agenda.Presentation.Models;
using Bogus;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Agenda.Presentation.Controllers
{
    public class AccountController : Controller
    {
        // ROTA: /Account/Login
        public IActionResult Login()
        {
            return View(); // acessar a página
        }

        [HttpPost] // Recebe o SUBMIT do formulário
        public IActionResult Login(AccountLoginModel model)
        {
            // verificar se a model não possui erros de validação
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();

                    // consultar o usuário no banco de dados através do email e da senha
                    var usuario = usuarioRepository.GetByEmailAndSenha(model.Email, model.Senha);

                    // verificar se o usuário não foi encontrado
                    if (usuario == null)
                    {
                        TempData["Mensagem"] = $"Acesso negado.";
                    }
                    else
                    {
                        // realizando a autenticação do usuário
                        var authenticationModel = new AuthenticationModel
                        {
                            IdUsuario = usuario.IdUsuario,
                            Nome = usuario.Nome,
                            Email = usuario.Email,
                            DataHoraAcesso = DateTime.Now
                        };

                        // serializando os dados da model em JSON
                        var json = JsonConvert.SerializeObject(authenticationModel);

                        // gerar o conteudo para gravação no cookie de autenticação
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, json) },
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        // gravando o cookie
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        // redirecionar para a página: /Home/Index
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = $"Erro: {e.Message}";
                }
            }

            return View(); // acessar a página
        }

        // ROTA: /Account/Register
        public IActionResult Register()
        {
            return View(); // acessar a página
        }

        [HttpPost] // Recebe o SUBMIT do formulário
        public IActionResult Register(AccountRegisterModel model)
        {
            // verificar se a model não possui erros de validação
            if(ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();

                    if (usuarioRepository.GetByEmail(model.Email) != null)
                    {
                        TempData["Mensagem"] = $"O email '{model.Email}' informado já está cadastrado para outro usuário.";
                    }
                    else
                    {
                        var usuario = new Usuario()
                        {
                            IdUsuario = Guid.NewGuid(),
                            Nome = model.Nome,
                            Email = model.Email,
                            Senha = model.Senha,
                            DataCriacao = DateTime.Now
                        };

                        usuarioRepository.Create(usuario);

                        TempData["Mensagem"] = $"Parabéns {usuario.Nome}, sua conta foi criada com sucesso!";
                        ModelState.Clear();
                    }
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = $"Erro: {e.Message}";
                }
            }

            return View(); // acessar a página
        }

        // ROTA: /Account/Password
        public IActionResult Password()
        {
            return View(); // acessar a página
        }

        [HttpPost] // Recebe o SUBMIT do formulário
        public IActionResult Password(AccountPasswordModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    // pesquisar o usuário no banco de dados através do email
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmail(model.Email);

                    if(usuario != null)
                    {
                        #region Gerando uma nova senha para o usuário

                        var faker = new Faker();
                        var novaSenha = $"@{faker.Internet.Password()}3";

                        #endregion

                        #region Enviando um email para o usuário contendo a nova senha

                        var subject = "Recuperação de Senha - Agenda de Contatos";
                        var body = @$"
                            <h3>Olá {usuario.Nome}</h3>
                            <p>Uma nova senha foi gerada com sucesso para o seu usuário.</p>
                            <p>Acesse a agenda com a senha: <strong>{novaSenha}</strong></p>
                            <p>Após acessar a agenda, você poderá utilizar o menu 'Minha Conta' para alterar sua senha.</p>
                            <br/>
                            <p>Att, <br/>Equipe Agenda de Contatos</p>
                        ";

                        var emailMessageService = new EmailMessageService();
                        emailMessageService.SendMessage(usuario.Email, subject, body);

                        #endregion

                        #region Atualizando a senha no banco de dados

                        usuarioRepository.Update(usuario.IdUsuario, novaSenha);

                        #endregion

                        TempData["Mensagem"] = "Uma nova senha foi gerada com sucesso, por favor verifique sua conta de email.";
                        ModelState.Clear();

                    }
                    else
                    {
                        TempData["Mensagem"] = "Email inválido. Usuário não encontrado.";
                    }
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao recuperar senha: {e.Message}";
                }
            }

            return View(); // acessar a página
        }

        // ROTA: /Account/Logout
        public IActionResult Logout()
        {
            // apagar o cookie de autenticação
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // redirecionar de volta para a página de login
            return RedirectToAction("Login", "Account"); // /Account/Login
        }
    }
}
