using Agenda.Data.Entities;
using Agenda.Reports.Interfaces;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Reports.Services
{
    public class ContatosReportServicePdf : IContatosReport
    {
        public byte[] CreateReport(List<Contato> contatos, Usuario usuario)
        {
            var memoryStream = new MemoryStream();
            var pdf = new PdfDocument(new PdfWriter(memoryStream));

            // montando o documento PDF
            using (var document = new Document(pdf))
            {
                document.Add(new Paragraph("Relatório de Contatos\n"));

                document.Add(new Paragraph($"Nome do usuário: {usuario.Nome}"));
                document.Add(new Paragraph($"Email do usuário: {usuario.Email}"));
                document.Add(new Paragraph($"Data/Hora de geração: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}"));

                var table = new Table(5);
                table.SetWidth(UnitValue.CreatePercentValue(100));

                table.AddHeaderCell("Nome do contato");
                table.AddHeaderCell("Email");
                table.AddHeaderCell("Telefone");
                table.AddHeaderCell("Data Nasc");
                table.AddHeaderCell("Tipo");

                foreach (var item in contatos)
                {
                    table.AddCell(item.Nome);
                    table.AddCell(item.Email);
                    table.AddCell(item.Telefone);
                    table.AddCell(item.DataNascimento.ToString("dd/MM/yyyy"));
                    table.AddCell(item.Tipo == 1 ? "Amigos" : item.Tipo == 2 ? "Trabalho" : item.Tipo == 3 ? "Família" : "Outros");
                }

                document.Add(table);
            }

            return memoryStream.ToArray();
        }
    }
}
