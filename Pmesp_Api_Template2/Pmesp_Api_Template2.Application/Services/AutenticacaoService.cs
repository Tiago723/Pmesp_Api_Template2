using Microsoft.Extensions.Logging;
using Pmesp_Api_Template2.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pmesp_Api_Template2.Application.Interfaces;
using Pmesp_Api_Template2.Domain.Entidades;

namespace Pmesp_Api_Template2.Application.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly ILogger<AutenticacaoService> _logger;
        private readonly IAutenticacaoRepository _autenticacaoRepository;
        public AutenticacaoService(ILogger<AutenticacaoService> logger, IAutenticacaoRepository autenticacaoRepository)
        {
            _logger = logger;
            _autenticacaoRepository = autenticacaoRepository;
        }

        public Task<string> ChecarUsuario(string usuario, string senha)
        {
            return _autenticacaoRepository.ChecarUsuario(usuario, senha);
        }
    }
}
