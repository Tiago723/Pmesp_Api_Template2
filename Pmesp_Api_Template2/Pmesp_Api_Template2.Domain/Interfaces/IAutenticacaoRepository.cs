using Pmesp_Api_Template2.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pmesp_Api_Template2.Domain.Interfaces
{
    public interface IAutenticacaoRepository
    {
        Task<string> ChecarUsuario(string usuario, string senha);
    }
}
