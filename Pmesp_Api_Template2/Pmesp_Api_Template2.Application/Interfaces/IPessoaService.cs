using Pmesp_Api_Template2.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pmesp_Api_Template2.Application.Interfaces
{
    public interface IPessoaService
    {
        Task<Resultado<List<Pessoa>>> ConsultaCliente(string id);
        Task<Resultado<List<Pessoa>>> EditaCliente(string id, string nome, string tel, string email);
        Task<Resultado<List<Pessoa>>> ExcluiCliente(string id);
        Task<Resultado<List<Pessoa>>> InsereCliente(InsereCliente request);
        Task<Resultado<List<Pessoa>>> ListaTodosClientes();
        Task<byte[]> RelatorioPDF(string id);
        Task <string> GenerateToken(string id);
    }
}
