
using Microsoft.AspNetCore.Mvc;
using Pmesp_Api_Template2.Application.Interfaces;
using Pmesp_Api_Template2.Domain.Entidades;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Pmesp_Api_Template2.Api.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ILogger<AutenticacaoController> _logger;
        private readonly IAutenticacaoService _autenticacaoService;

        HttpClient Http = new HttpClient();

        public AutenticacaoController(ILogger<AutenticacaoController> logger, IAutenticacaoService autenticacaoService)
        {
            _logger = logger;
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost]
        [Route("ValidaLogin")]
        public async Task<IActionResult> ValidaLogin(string usuario, string senha)
        {
            var retorno = await _autenticacaoService.ChecarUsuario(usuario, senha);

            try
            {
                if (retorno == "True")
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status200OK) + "ValidaLogin" + " Sucesso " + retorno, EventLogEntryType.Information);
                    return Ok(retorno);
                }
                else if(retorno == "False")
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status401Unauthorized) + "ValidaLogin" + " Não autorizado " + retorno, EventLogEntryType.Warning);
                    return StatusCode(StatusCodes.Status401Unauthorized, "Usuário ou senha inválidos");
                }
                else
                {
                    EventLog.WriteEntry("Pmesp_Api_Template2", Convert.ToString(StatusCodes.Status503ServiceUnavailable) + "ValidaLogin" + " Erro no Serviço " + retorno, EventLogEntryType.Warning);
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "Erro no Serviço");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
