using Agenda.Data.Entities;
using Agenda.Data.Repositories;
using Agenda.Presentation.Models;
using Agenda.Reports.Interfaces;
using Agenda.Reports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Agenda.Presentation.Controllers
{
    [Authorize]
    public class ContatosController : Controller
    {
        // ROTA: /Contatos/Cadastro
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost] // recebe o submit do formulário
        public IActionResult Cadastro(ContatosCadastroModel model)
        {
            // verificar se todos os campos da model
            // passaram nas regras de validação
            if(ModelState.IsValid)
            {
                try
                {
                    // capturar o usuário autenticado no sistema
                    var auth = ObterUsuarioAutenticado();

                    // capturar os dados do contato
                    var contato = new Contato
                    {
                        IdContato = Guid.NewGuid(),
                        Nome = model.Nome,
                        Telefone = model.Telefone,
                        Email = model.Email,
                        DataNascimento = DateTime.Parse(model.DataNascimento),
                        Tipo = int.Parse(model.Tipo),
                        IdUsuario = auth.IdUsuario
                    };

                    // cadastrando no banco de dados
                    var contatoRepository = new ContatoRepository();
                    contatoRepository.Create(contato);

                    TempData["MensagemSucesso"] = $"Contato {contato.Nome} cadastrado com sucesso.";
                    ModelState.Clear();
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = $"Falha ao cadastrar o contato: {e.Message}";
                }

            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preenchimento do formulário de cadastro, por favor verifique.";
            }

            return View();
        }

        // ROTA: /Contatos/Consulta
        public IActionResult Consulta()
        {
            var lista = new List<ContatosConsultaModel>();

            try
            {
                //obtendo o usuáro autenticado
                var auth = ObterUsuarioAutenticado();

                //consultar todos os contatos no banco de dados
                //pertencentes ao usuário que está autenticado
                var contatoRepository = new ContatoRepository();
                foreach (var item in contatoRepository.GetAllByUsuario(auth.IdUsuario))
                {
                    var model = new ContatosConsultaModel
                    {
                        IdContato = item.IdContato,
                        Nome = item.Nome.ToUpper(),
                        Telefone = item.Telefone,
                        Email = item.Email,
                        DataNascimento = item.DataNascimento.ToString("dd/MM/yyyy"),
                        Tipo = item.Tipo == 1 ? "Amigos" : item.Tipo == 2 ? "Trabalho" : item.Tipo == 3 ? "Família" : "Outros"
                    };

                    lista.Add(model);
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Falha ao consultar contatos: {e.Message}";
            }

            return View(lista);
        }

        // ROTA: /Contatos/Edicao
        public IActionResult Edicao(Guid id)
        {
            try
            {
                // consultar os dados do contato atraves do ID
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetById(id);

                // verificar se o contato foi encontrado
                // verificar se o contato pertence ao usuário autenticado
                if (contato != null && contato.IdUsuario == ObterUsuarioAutenticado().IdUsuario)
                {
                    // criando uma instancia da classe ContatosEdicaoModel
                    var model = new ContatosEdicaoModel
                    {
                        IdContato = contato.IdContato,
                        Nome = contato.Nome,
                        Email = contato.Email,
                        Telefone = contato.Telefone,
                        DataNascimento = contato.DataNascimento.ToString("dd/MM/yyyy"),
                        Tipo = contato.Tipo.ToString()
                    };

                    // enviando o objeto 'model' para a página
                    return View(model);
                }
                else
                {
                    TempData["MensagemAlerta"] = $"Contato não encontrado.";
                    return RedirectToAction("Consulta");
                }

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Falha ao obter contato: {e.Message}";
            }

            return View();
        }

        [HttpPost]
        public IActionResult Edicao(ContatosEdicaoModel model)
        {
            // verificar se todos os campos passaram nas regras de validação
            if(ModelState.IsValid)
            {
                try
                {
                    var contato = new Contato
                    {
                        IdContato = model.IdContato,
                        Nome = model.Nome,
                        Email = model.Email,
                        Telefone = model.Telefone,
                        DataNascimento = DateTime.Parse(model.DataNascimento),
                        Tipo = int.Parse(model.Tipo)
                    };

                    // atualizando no banco de dados
                    var contatoRepository = new ContatoRepository();
                    contatoRepository.Update(contato);

                    TempData["MensagemSucesso"] = $"Contato {contato.Nome} atualizado com sucesso.";
                    return RedirectToAction("Consulta");
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = $"Falha ao editar contato: {e.Message}";
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preenchimento do formulário de edição, por favor verifique.";
            }

            return View();
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                //consultar o contato atraves do ID
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetById(id);

                //verificar se o contato foi encontrado e
                //verificar se o contato pertence ao usuário autenticado
                if (contato != null && contato.IdUsuario == ObterUsuarioAutenticado().IdUsuario)
                {
                    //excluindo o contato
                    contatoRepository.Delete(contato);
                    TempData["MensagemSucesso"] = $"Contato {contato.Nome} excluído com sucesso.";
                }
                else
                {
                    TempData["MensagemAlerta"] = "Contato não encontrado.";
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Falha ao excluir o contato: {e.Message}";
            }

            return RedirectToAction("Consulta");
        }

        // ROTA: /Contatos/Relatorio
        public IActionResult Relatorio()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Relatorio(ContatosRelatorioModel model)
        {
            // verificar se todos os campos passaram nas validações
            if(ModelState.IsValid)
            {
                try
                {
                    string tipoArquivo = null; // MIME TYPE -> https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types
                    string nomeArquivo = $"contatos_{DateTime.Now.ToString("ddMMyyyyHHmmss")}";
                    IContatosReport contatosReport = null;

                    switch (model.Formato)
                    {
                        case "pdf":
                            tipoArquivo = "application/pdf";
                            nomeArquivo += ".pdf";
                            contatosReport = new ContatosReportServicePdf(); // Polimorfismo
                            break;

                        case "excel":
                            tipoArquivo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            nomeArquivo += ".xlsx";
                            contatosReport = new ContatosReportServiceExcel(); // Polimorfismo
                            break;
                    }

                    // capturando o usuário autenticado
                    var auth = ObterUsuarioAutenticado();
                    var usuario = new Usuario
                    {
                        IdUsuario = auth.IdUsuario,
                        Nome = auth.Nome,
                        Email = auth.Email
                    };

                    // consultando os contatos no banco de dados
                    var contatoRepository = new ContatoRepository();
                    var contatos = contatoRepository.GetAllByUsuario(usuario.IdUsuario);

                    // gerando o arquivo
                    var relatorio = contatosReport.CreateReport(contatos, usuario);

                    // download
                    return File(relatorio, tipoArquivo, nomeArquivo);
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = $"Falha ao gerar relatório: {e.Message}";
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preenchimento do formulário de relatório, por favor verifique.";
            }

            return View();
        }

        // método auxiliar para retornar o usuário autenticado
        private AuthenticationModel ObterUsuarioAutenticado()
        {
            //ler os dados contidos no cookie (JSON)
            var json = User.Identity.Name;
            //deserializar o JSON e retornar o objeto
            return JsonConvert.DeserializeObject<AuthenticationModel>(json);
        }

    }
}
