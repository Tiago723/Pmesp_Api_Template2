using Pmesp_Api_Template2.infra.Connection;

namespace Pmesp_Api_Template2.infra.Repositories
{
    public abstract class BaseRepository
    {

        public ConnectionStrings _connectionDb;
        public BaseRepository(ConnectionStrings connectionDb )
        {
            _connectionDb = connectionDb;
        }

    }
}
