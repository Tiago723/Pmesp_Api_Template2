
using Elasticsearch.Net;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Mvc;
using Pmesp_Api_Template2.Application.Interfaces;
using Pmesp_Api_Template2.Application.Services;
using Pmesp_Api_Template2.Domain.Entidades;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using YamlDotNet.Core.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Pmesp_Api_Template2.Api.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly ILogger<PessoaController> _logger;
        private readonly IPessoaService _pessoaService;

        HttpClient Http = new HttpClient();

        public PessoaController(ILogger<PessoaController> logger, IPessoaService pessoaService)
        {
            _logger = logger;
            _pessoaService = pessoaService;
        }

        [HttpGet("ListaTodosClientes")]
        public async Task<IActionResult> ListaTodosClientes()
        {
            var retorno = await _pessoaService.ListaTodosClientes();

            try
            {
                if (retorno == null)
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status503ServiceUnavailable) + "ListaTodosClientes" + " Erro no Serviço " + retorno, EventLogEntryType.Warning);
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "Erro no Serviço");
                }
                else
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status200OK) + "ListaTodosClientes" + " Sucesso " + retorno, EventLogEntryType.Information);
                    return Ok(retorno);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("ConsultaCliente/id")]
        public async Task<IActionResult> ConsultaCliente(string id)
        {
            var retorno = await _pessoaService.ConsultaCliente(id);

            try
            {
                if (retorno == null)
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status503ServiceUnavailable) + "ConsultaCliente" + " Erro no Serviço " + retorno, EventLogEntryType.Warning);
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "Erro no Serviço");
                }
                else
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status200OK) + "ConsultaCliente" + " Sucesso " + retorno, EventLogEntryType.Information);
                    return Ok(retorno);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("InsereCliente")]
        public async Task<IActionResult> InsereCliente(InsereCliente request)
        {
            var retorno = await _pessoaService.InsereCliente(request);
           
            try
            {
                if (retorno == null)
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status503ServiceUnavailable) + "InsereCliente" + " Erro no Serviço " + retorno, EventLogEntryType.Warning);
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "Erro no Serviço");
                }
                else
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status200OK) + "InsereCliente" + " Sucesso " + retorno, EventLogEntryType.Information);
                    return Ok(retorno);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("EditaCliente")]
        public async Task<IActionResult> EditaCliente(string id, string nome, string tel, string email)
        {
            var retorno = await _pessoaService.EditaCliente(id, nome, tel, email);

            try
            {
                if (retorno == null)
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status503ServiceUnavailable) + "EditaCliente" + " Erro no Serviço " + retorno, EventLogEntryType.Warning);
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "Erro no Serviço");
                }
                else
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status200OK) + "EditaCliente" + " Sucesso " + retorno, EventLogEntryType.Information);
                    return Ok(retorno);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ExcluiCliente/id")]
        public async Task<IActionResult> ExcluiCliente(string id)
        {
            var retorno = await _pessoaService.ExcluiCliente(id);

            try
            {
                if (retorno == null)
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status503ServiceUnavailable) + "ExcluiCliente" + " Erro no Serviço " + retorno, EventLogEntryType.Warning);
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "Erro no Serviço");
                }
                else
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status200OK) + "ExcluiCliente" + " Sucesso " + retorno, EventLogEntryType.Information);
                    return Ok(retorno);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("Relatorio/{token}")]
        public async Task<IActionResult> RelatorioPDF(string id)
        {
            try
            {
                var PDF = await _pessoaService.RelatorioPDF(id);

                if (PDF == null)
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status503ServiceUnavailable) + "Relatorio" + " Erro no Serviço " + PDF, EventLogEntryType.Warning);
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "Erro no Serviço");
                }
                else
                {
                    //Obtém a data e hora atual
                    DateTime dateTime = DateTime.Now;

                    // Obtém somente a data
                    string dt = dateTime.ToShortDateString();

                    // Atribui a data como parte do nome do arquivo para download
                    string ArquivoPDF = "Relatório_" + dt.ToString() + ".pdf";

                    // Configura o cabeçalho de resposta para abrir o PDF no navegador
                    Response.Headers.Add("Content-Disposition", $"inline; filename=\"{ArquivoPDF}\"");

                    string tipoConteudo = "application/pdf";

                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status200OK) + "Relatorio" + " Sucesso " + PDF, EventLogEntryType.Information);

                    return File(PDF, tipoConteudo);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro no servidor");
            }
        }

        [HttpGet]
        [Route("Token")]
        public async Task<string> GenerateToken(string id)
        {
            var token = await _pessoaService.GenerateToken(id);

            return token;
        }
    }
}
