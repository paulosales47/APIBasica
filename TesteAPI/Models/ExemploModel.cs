using System;
using System.Configuration;
using System.Data.SqlClient;


namespace TesteAPI.Models
{
    public class ExemploModel
    {
        private string _stringConexao;

        public ExemploModel()
        {
            _stringConexao = ConfigurationManager.ConnectionStrings["CONEXAO"].ToString();
        }
        public string PropiedadeTeste1 { get; set; }

        public int PropiedadeTeste2 { get; set; }
        
        public string PropiedadeTeste3 { get; set; }
        
        public string ExecutarProcedure(ExemploModel url)
        {
            SqlDataReader reader = null;

            using (SqlConnection connection = new SqlConnection(_stringConexao))
            {
                //CRIAÇÃO DO OBJETO QUE REALIZARÁ O COMANDO NO BANCO DE DADOS
                SqlCommand command = new SqlCommand("NOME_PROCEDURE", connection);

                //setando o tipo de artefato que será chamado no banco de dados
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //ADIÇÃO DOS PARÂMETROS DAS PROCEDURES
                command.Parameters.Add(new SqlParameter("@PARAMETRO_1", url.PropiedadeTeste1));
                command.Parameters.Add(new SqlParameter("@PARAMETRO_2", url.PropiedadeTeste2));

                try
                {
                    //abre conexão com banco de dados
                    connection.Open();
                    //executa o comando
                    reader = command.ExecuteReader();

                    return LeituraResposta(reader);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //verifica se a conexão não está fechada
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }

        private string LeituraResposta(SqlDataReader reader)
        {
            string valorRetorno = null;

            try
            {
                //VERIFICA SE O OBJETO RETORNADO DO BANCO DE DADOS POSSUI DADOS
                if (reader.HasRows)
                {
                    //FAZ O LOOP PARA LER OS DADOS RETORNADOS
                    while (reader.Read())
                    {
                        valorRetorno = reader["URL_GERADA"].ToString();
                    }
                }

                return valorRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}