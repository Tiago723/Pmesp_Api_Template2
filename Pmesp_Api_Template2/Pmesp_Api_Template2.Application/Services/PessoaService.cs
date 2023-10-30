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
    public class PessoaService : IPessoaService
    {
        private readonly ILogger<PessoaService> _logger;
        private readonly IPessoaRepository _pessoaRepository;
        public PessoaService(ILogger<PessoaService> logger, IPessoaRepository pessoaRepository)
        {
            _logger = logger;
            _pessoaRepository = pessoaRepository;
        }

        public Task<Resultado<List<Pessoa>>> ConsultaCliente(string id)
        {
            return _pessoaRepository.ConsultaCliente(id);
        }

        public Task<Resultado<List<Pessoa>>> ListaTodosClientes()
        {
            return _pessoaRepository.ListaTodosClientes();
        }

        public Task<Resultado<List<Pessoa>>> InsereCliente(InsereCliente request)
        {
            return _pessoaRepository.InsereCliente(request);
        }

        public Task<Resultado<List<Pessoa>>> EditaCliente(string id, string nome, string tel, string email)
        {
            return _pessoaRepository.EditaCliente(id, nome, tel, email);
        }
        public Task<Resultado<List<Pessoa>>> ExcluiCliente(string id)
        {
            return _pessoaRepository.ExcluiCliente(id);
        }
        public Task<byte[]> RelatorioPDF(string id)
        {
            return _pessoaRepository.RelatorioPDF(id);
        }
        public Task<string> GenerateToken(string id)
        {
            return _pessoaRepository.GenerateToken(id);
        }
    }
}
