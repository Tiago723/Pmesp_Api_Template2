using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pmesp_Api_Template2.Domain.Entidades
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string? nome { get; set; }
        public string? Tel { get; set;}
        public string? email { get; set;}
    }

    public class InsereCliente
    {
        public string? Nome { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }
    }

    public class Consulta
    {
        public string? Id { get; set; }
    }

    public class EditaCliente
    {
        public string? Id { get; set; }
        public string? nome { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }
    }

    public class ExcluiCliente
    {
        public string? Id { get; set; }
    }

    public class CredenciaisReportService
    {
        public string username = "";
        public string password = "";
    }
}
