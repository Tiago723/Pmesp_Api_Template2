using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Pmesp_Api_Template2.Domain.Entidades;
using Pmesp_Api_Template2.Domain.Interfaces;
using Pmesp_Api_Template2.infra.Connection;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Pmesp_Api_Template2.infra.Repositories
{
    public class PessoaRepository : BaseRepository, IPessoaRepository  
    {
        private readonly IDbConnection conn;
        public readonly HttpClient _client = new HttpClient();
        private readonly IConfiguration _config;

        public PessoaRepository(ConnectionStrings connectionDb, IConfiguration config) : base(connectionDb)
        {
            conn = new SqlConnectionDB().ConnectionDB(_connectionDb.BD);
            _config = config;
        }

        public async Task<Resultado<List<Pessoa>>> ListaTodosClientes()
        {
            Resultado<List<Pessoa>> resultadoOperacao = new();

            var lista = new List<Pessoa>();
            var query = "select * from clientes";

            using (SqlConnection conexao = new SqlConnection(conn.ConnectionString))
            {
                try
                {
                    conexao.Open();
                    lista = (await conexao.QueryAsync<Pessoa>(query, conexao)).ToList();

                    resultadoOperacao.resultado = lista;
                    resultadoOperacao.ExecutouComSucesso = true;
                    resultadoOperacao.MensagemRetorno = "A operação executou com sucesso";
                    resultadoOperacao.Codigo = "200";
                    resultadoOperacao.Detalhe = "";

                    if (lista.Count == 0)
                    {
                        resultadoOperacao.MensagemRetorno = "NENHUM RESULTADO ENCONTRADO";
                        return resultadoOperacao;
                    }
                    else
                    {
                        return resultadoOperacao;
                    }
                }
                catch (Exception)
                {
                    if (lista.Count == 0)
                    {
                        resultadoOperacao.MensagemRetorno = "FALHA AO RETORNAR A LISTA";
                    }

                    throw;
                }
                finally
                {
                    conexao?.Close();  
                }
            }
        }

        public async Task<Resultado<List<Pessoa>>> ConsultaCliente(string id)
        {
            Resultado<List<Pessoa>> resultadoOperacao = new();

            var cliente = new List<Pessoa>();
            var query = "select id, nome, tel, email from clientes WHERE id ='"+id+"'";

            using (SqlConnection conexao = new SqlConnection(conn.ConnectionString))
            {
                try
                {
                    conexao.Open();
                    cliente = (await conexao.QueryAsync<Pessoa>(query, conexao)).ToList();

                    if (cliente.Count == 0)
                    {
                        resultadoOperacao.MensagemRetorno = "NENHUM RESULTADO ENCONTRADO";
                        return resultadoOperacao;
                    }
                    else
                    {
                        resultadoOperacao.resultado = cliente;
                        resultadoOperacao.ExecutouComSucesso = true;
                        resultadoOperacao.MensagemRetorno = "A operação executou com sucesso";
                        resultadoOperacao.Codigo = "200";
                        resultadoOperacao.Detalhe = "";

                        return resultadoOperacao;
                    }
                }
                catch (Exception)
                {
                    if (cliente.Count == 0)
                    {
                        resultadoOperacao.MensagemRetorno = "FALHA AO RETORNAR O CLIENTE";
                    }

                    throw;
                }
                finally
                {
                    conexao?.Close();
                }
            }
        }

        public async Task<Resultado<List<Pessoa>>> InsereCliente(InsereCliente request)
        {
            Resultado<List<Pessoa>> resultadoOperacao = new();
            var cliente = new List<Pessoa>();

            using (SqlConnection conexao = new SqlConnection(conn.ConnectionString))
            {
                try
                {
                    conexao.Open();
                    var query = "INSERT INTO clientes (nome, tel, email) VALUES (@nome, @tel, @email)";

                    using (SqlCommand comando = new SqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", request.Nome);
                        comando.Parameters.AddWithValue("@tel", request.Tel);
                        comando.Parameters.AddWithValue("@email", request.Email);

                        comando.ExecuteNonQuery();
                    }

                    resultadoOperacao.resultado = cliente;
                    resultadoOperacao.ExecutouComSucesso = true;
                    resultadoOperacao.MensagemRetorno = "A operação executou com sucesso";
                    resultadoOperacao.Codigo = "200";
                    resultadoOperacao.Detalhe = "";

                    return resultadoOperacao;
                }
                catch (Exception)
                {
                    if (cliente.Count == 0)
                    {
                        resultadoOperacao.MensagemRetorno = "FALHA AO CADASTRAR O CLIENTE";
                    }

                    throw;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public async Task<Resultado<List<Pessoa>>> EditaCliente(string id, string nome, string tel, string email)
        {
            Resultado<List<Pessoa>> resultadoOperacao = new();
            var cliente = new List<Pessoa>();

            using (SqlConnection conexao = new SqlConnection(conn.ConnectionString))
            {
                try
                {
                    conexao.Open();
                    var query = "UPDATE clientes SET nome='" + nome + "', tel='" + tel + "', email='" + email + "' WHERE id= '" + id + "'";

                    using (SqlCommand command = new SqlCommand(query, conexao))
                    {
                        command.ExecuteNonQuery();

                        resultadoOperacao.resultado = cliente;
                        resultadoOperacao.ExecutouComSucesso = true;
                        resultadoOperacao.MensagemRetorno = "A operação executou com sucesso";
                        resultadoOperacao.Codigo = "200";
                        resultadoOperacao.Detalhe = "";

                        return resultadoOperacao;
                    }
                }
                catch (Exception)
                {
                    if (cliente.Count == 0)
                    {
                        resultadoOperacao.MensagemRetorno = "FALHA AO EDITAR O CLIENTE";
                    }

                    throw;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public async Task<Resultado<List<Pessoa>>> ExcluiCliente(string id)
        {
            Resultado<List<Pessoa>> resultadoOperacao = new();
            var cliente = new List<Pessoa>();

            using (SqlConnection conexao = new SqlConnection(conn.ConnectionString))
            {
                try
                {
                    conexao.Open();
                    var query = "DELETE FROM clientes WHERE id= '" + id + "'";
                   
                    using (SqlCommand command = new SqlCommand(query, conexao))
                    {
                        command.ExecuteNonQuery();

                        resultadoOperacao.resultado = cliente;
                        resultadoOperacao.ExecutouComSucesso = true;
                        resultadoOperacao.MensagemRetorno = "A operação executou com sucesso";
                        resultadoOperacao.Codigo = "200";
                        resultadoOperacao.Detalhe = "";

                        return resultadoOperacao;
                    }
                }
                catch (Exception)
                {
                    if (cliente.Count == 0)
                    {
                        resultadoOperacao.MensagemRetorno = "FALHA AO EXCLUIR O CLIENTE";
                    }

                    throw;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public async Task<byte[]> RelatorioPDF(string Id)
        {
            try
            {
                CredenciaisReportService credenciais = new CredenciaisReportService();

                HttpClientHandler Autenticacao = new HttpClientHandler();
                Autenticacao.Credentials = new NetworkCredential(credenciais.username, credenciais.password);

                using (HttpClient client = new HttpClient(Autenticacao))
                {
                    string servidor = "http://localhost/";
                    string nomeRelatorio = "Report1";

                    string url = $"{servidor}ReportServer/Pages/ReportViewer.aspx?%2fRelatorio1%2f{nomeRelatorio}&rs:Format=PDF&id={Id}";

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        byte[] byteArray = await response.Content.ReadAsByteArrayAsync();

                        return byteArray;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> GenerateToken(string id)
        {
            // Gere uma chave secreta de 32 bytes (256 bits)
            string chaveSecreta = GenerateRandomKey(32);

            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(chaveSecreta)))
            {
                // Converte o ID em bytes
                byte[] idBytes = Encoding.UTF8.GetBytes(id);

                // ComputeHash irá gerar um hash do ID
                byte[] hashBytes = hmac.ComputeHash(idBytes);

                // Converte o hash em uma representação de string segura
                string autentication = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return autentication;
            }
        }

        public string GenerateRandomKey(int length)
        {
            using (RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider())
            {
                // Cria um array de bytes com o comprimento especificado pela variável 'length'
                byte[] keyBytes = new byte[length];

                // Gera bytes seguros usando o RNGCryptoServiceProvider e armazena no array keyBytes
                rngCrypto.GetBytes(keyBytes);

                // Converte os bytes aleatórios em string hexadecimal e remove os hifens e gera uma string de chave aleatória hexadecimal
                return BitConverter.ToString(keyBytes).Replace("-", "").ToLower();
            }
        }
    }
}
