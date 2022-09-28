using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace On_the_fly_2._0
{
    internal class Banco
    {
        private static string Conexao = "Data Source =localhost; Initial Catalog =AEROPORTO; User ID=sa; Password=Livia_Livia31;"; //Tela de login do sqlserver. começa a conexao, insere seu usuario e valida a senha
                                                                                                                                       //Banco chamandocaminho = new Banco();
        private static SqlConnection ConexaoSql = new SqlConnection(Conexao);
        public Banco()
        {
        }
        public string Caminho()
        {
            return Conexao; //retorna essa conexao pra onde for chamado
        }
        public void Insert(string cmd)
        {
            try
            {
                ConexaoSql.Open(); //Abrindo conexão sql
                SqlCommand comando = new SqlCommand(cmd, ConexaoSql);
                comando.ExecuteNonQuery();
                ConexaoSql.Close();
            }
            catch (Exception ex)

            {
                Console.WriteLine("Erro ao seu comunicar com o banco\n" + ex.Message + "Tecle enter para continuar ");
                ConexaoSql.Close();
                Console.ReadKey();
            }


        }
        public bool Select(string cmd, int opc)
        {
            bool retorna = false;
            try
            {
                ConexaoSql.Open(); //Abrindo conexão sql
                SqlCommand comando = new SqlCommand(cmd, ConexaoSql);
                comando.ExecuteNonQuery();
               
                switch(opc)
                {
                    case 1:
                        using (SqlDataReader leitor = comando.ExecuteReader())
                        {
                          
                          
                            while (leitor.Read()) //enquanto leitor for verdadeiro
                            {

                                Console.WriteLine("\nN° CPF: {0}", leitor.GetString(0)); //{0} posição da variavel q vai informar, parametro 0, cada linha começa no zero
                                Console.WriteLine("Nome: {0}", leitor.GetString(1));
                                Console.WriteLine("Sexo: {0}", leitor.GetString(2));
                                Console.WriteLine("Data cadastro: {0}", leitor.GetDateTime(3).ToShortDateString());
                                Console.WriteLine("Data nascimento: {0}", leitor.GetDateTime(4).ToShortDateString());
                                Console.WriteLine("Ultima compra: {0}", leitor.GetDateTime(5).ToShortDateString());
                                Console.WriteLine("Situação: {0}", leitor.GetString(6));
                               retorna = true;
                            }
                        }
                        break;

                    case 2:
                        using (SqlDataReader leitor = comando.ExecuteReader())
                        {


                            while (leitor.Read()) //enquanto leitor for verdadeiro
                            {

                                Console.WriteLine("\nN° CNPJ: {0}", leitor.GetString(0)); //{0} posição da variavel q vai informar, parametro 0, cada linha começa no zero
                                Console.WriteLine("Razao_Social: {0}", leitor.GetString(1));
                                Console.WriteLine("Data Abertura: {0}", leitor.GetDateTime(2).ToShortDateString());
                                Console.WriteLine("Data Cadastro: {0}", leitor.GetDateTime(3).ToShortDateString());
                                Console.WriteLine("Ultimo voo: {0}", leitor.GetDateTime(4).ToShortDateString());
                                Console.WriteLine("Situação: {0}", leitor.GetString(5));
                                retorna = true;
                            }
                        }
                        break;

                    case 3:
                        using (SqlDataReader leitor = comando.ExecuteReader())
                        {


                            while (leitor.Read()) //enquanto leitor for verdadeiro
                            {

                                Console.WriteLine("\nINSCRIÇÃO: {0}", leitor.GetString(0)); //{0} posição da variavel q vai informar, parametro 0, cada linha começa no zero
                                Console.WriteLine("CNPJ da companhia aerea proprietaria: {0}", leitor.GetString(1));
                                Console.WriteLine("Capacidade: {0}", leitor.GetString(2));
                                Console.WriteLine("Assentos ocupados: {0}", leitor.GetString(3));
                                Console.WriteLine("Data Cadastro: {0}", leitor.GetDateTime(4).ToShortDateString());
                                Console.WriteLine("Ultimo venda: {0}", leitor.GetDateTime(5).ToShortDateString());
                                Console.WriteLine("Situação: {0}", leitor.GetString(6));
                                retorna = true;
                            }
                        }
                        break;
                }

               
                ConexaoSql.Close();
                return retorna;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao seu comunicar com o banco\n" + ex.Message + "Tecle enter para continuar ");
                ConexaoSql.Close();
                Console.ReadKey();
                return false;
            }
        }
        public void Update(string cmd)
        {
            try
            {
                ConexaoSql.Open(); //Abrindo conexão sql
                SqlCommand comando = new SqlCommand(cmd, ConexaoSql);
                comando.ExecuteNonQuery();
                ConexaoSql.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao seu comunicar com o banco\n" + ex.Message + "Tecle enter para continuar ");
                ConexaoSql.Close();
                Console.ReadKey();
            }
        }
        public void Delete()
        {
            try
            {
                ConexaoSql.Open(); //Abrindo conexão sql
                SqlCommand comando = new SqlCommand();
                comando.ExecuteNonQuery();
                ConexaoSql.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao seu comunicar com o banco\n" + ex.Message + "Tecle enter para continuar ");
                ConexaoSql.Close();
                Console.ReadKey();
            }
        }

        public bool VerificarDadoExistente(string dado, string campo, string tabela)
        {
            string queryString = $"SELECT {campo} FROM {tabela} WHERE {campo} = '{dado}'";
            try
            {
                SqlCommand command = new SqlCommand(queryString, ConexaoSql);
                ConexaoSql.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ConexaoSql.Close();
                        return true;
                    }
                    else
                    {
                        ConexaoSql.Close();
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                ConexaoSql.Close();
                Console.WriteLine($"Erro ao acessar o Banco de Dados!!!\n{e.Message}");
                Console.WriteLine("Tecle Enter para continuar....");
                Console.ReadKey();
                return false;
            }
        }
    }
}