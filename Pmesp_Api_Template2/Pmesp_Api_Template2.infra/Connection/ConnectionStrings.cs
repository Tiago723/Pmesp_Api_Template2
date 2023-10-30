using Microsoft.Extensions.Configuration;

namespace Pmesp_Api_Template2.infra.Connection
{
    public class ConnectionStrings
    {
        public string BD { get; private set; }

        public ConnectionStrings(IConfiguration configuration)
        {
            BD = configuration.GetConnectionString("ConectaBanco");
        }
    }
}
