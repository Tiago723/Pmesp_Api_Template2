using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pmesp_Api_Template2.Domain.Entidades;
using Pmesp_Api_Template2.Domain.Interfaces;
using Pmesp_Api_Template2.infra.Connection;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Pmesp_Api_Template2.infra.Repositories
{
    public class AutenticacaoRepository : BaseRepository, IAutenticacaoRepository  
    {
        private readonly IDbConnection conn;

        public readonly HttpClient _client = new HttpClient();
        private readonly IConfiguration _config;

        public AutenticacaoRepository(ConnectionStrings connectionDb, IConfiguration config) : base(connectionDb)
        {
            conn = new SqlConnectionDB().ConnectionDB(_connectionDb.BD);
            _config = config;
        }

        public async Task<string> ChecarUsuario(string usuario, string senha)
        {
            bool Resultado = false;

            var query = "SELECT id_administrador, nome, usuario, senha FROM administrador WHERE usuario = '" + usuario + "' AND senha = '" + senha + "' ";

            using (SqlConnection conexao = new SqlConnection(conn.ConnectionString))
            {
                try
                {
                    conexao.Open();
                    using (var comando = new SqlCommand(query, conexao))
                    {
                        using (var dr = comando.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                if (usuario == dr["usuario"].ToString() && senha == dr["senha"].ToString())
                                {
                                    Resultado = true;
                                }
                                else
                                {
                                    Resultado = false;
                                }
                            }
                        }
                    }

                    return Resultado.ToString();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conexao?.Close();
                }
            }
        }
    }
}
