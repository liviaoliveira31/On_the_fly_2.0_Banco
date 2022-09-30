using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace On_the_fly_2._0
{
    internal class PassagemVoo
    {
        public string Id_Passagem { get; set; }
        public string IdVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public double Valor { get; set; }
        public char Situacao { get; set; }
        Banco banco = new Banco();

        public PassagemVoo()
        {
        }

        public void Pausa()
        {
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }

        #region gerador de passagens acionado a partir da geração de um voo
        public int BuscaCapacidade(string inscricao)
        {
            int capacidade;

            capacidade = banco.BuscaEspecifica(inscricao, "Inscricao", "Aeronave", "capacidade");
            return capacidade;
        }
        public void GerarPassagem(string idAeronave, string idVoo)
        {
            int capacidade = 0;
            float valorPas;
            capacidade = BuscaCapacidade(idAeronave);

            Console.WriteLine(" Digite o valor da Passagem: ");
            valorPas = float.Parse(Console.ReadLine().Replace(",", ""));
            int i = 0;
            for (i = 1; i <= capacidade; i++)
            {
                //int id = Random(0001, 9999);
                if (i < 10)
                {
                    Id_Passagem = $"PA000{i}";
                }
                else if (i < 100)
                {
                    Id_Passagem = $"PA00{i}";
                }
                else if (i < 1000)
                {
                    Id_Passagem = $"PA0{i}";
                }
                else
                {
                    Id_Passagem = $"PA{i}";
                }
                Id_Passagem = Id_Passagem;
                IdVoo = idVoo;
                Valor = valorPas;
                DataCadastro = DateTime.Now;
                Situacao = 'L';
             
                string cmdinsert = $"INSERT INTO Passagem(Id_Passagem,Id_Voo,Valor,Data_Ultima_Operacao,Situacao) VALUES('{Id_Passagem}','{IdVoo}','{Valor}','{DataCadastro}','{Situacao}')";
                banco.Insert(cmdinsert);
              
            }
        }
        #endregion

        #region localização e impressão de passagens
        public void LocalizarPassagem()
        {
            if (VerificarIdPassagem())
            {
                int opc = 8;
                string cmdselect = $"SELECT ID_PASSAGEM, ID_VOO, VALOR, DATA_ULTIMA_OPERACAO, SITUACAO FROM PASSAGEM WHERE ID_PASSAGEM = '{Id_Passagem}'";
                banco.Select(cmdselect, opc);
            }
        }
        public void imprimirPassagem()
        {
            if (AcharIdVoo())
            {
                Console.WriteLine("PASSAGENS DISPONIVEIS: ");

                int opc = 8;
                string cmdselect = $"SELECT ID_PASSAGEM, ID_VOO, VALOR, DATA_ULTIMA_OPERACAO, SITUACAO FROM PASSAGEM WHERE ID_VOO= '{IdVoo}' and SITUACAO  ='L'";
                banco.Select(cmdselect, opc);
            }

          
        }

        #endregion

        #region alteração de passagem com validação de dados
        public void AlterarPassagens()
        {
            bool verifica;
            if (AcharIdVoo())
            {
                Console.WriteLine("PRESSIONE ENTER PARA VER AS PASSAGENS");
                Console.ReadKey();
               
                int opc = 8;
                string cmdselect = $"SELECT ID_PASSAGEM, ID_VOO, VALOR, DATA_ULTIMA_OPERACAO, SITUACAO FROM PASSAGEM WHERE ID_VOO= '{IdVoo}' and SITUACAO  ='L'";

                verifica = banco.Select(cmdselect, opc);

                if (verifica)
                {

                    Console.WriteLine("Qual dado deseja alterar?\n1-Valor das passagens\n2-Situação de todas as passagens\n3-Situação de uma passagem especifica");
                    int num = int.Parse(Console.ReadLine());
                    switch (num)
                    {
                        case 1:
                            Console.WriteLine("INSIRA O NOVO VALOR DAS PASSAGENS DO VOO ESCOLHIDO");
                            double novoval = double.Parse(Console.ReadLine().Replace(",", ""));
                            string cmdupdate = $"UPDATE PASSAGEM SET VALOR = '{novoval}' WHERE ID_VOO = '{IdVoo}'";
                            banco.Update(cmdupdate);
                            opc = 8;
                            cmdselect = $"SELECT ID_PASSAGEM, ID_VOO, VALOR, DATA_ULTIMA_OPERACAO, SITUACAO FROM PASSAGEM WHERE ID_VOO= '{IdVoo}' and SITUACAO  ='L'";
                            banco.Select(cmdselect, opc);
                            break;

                        case 2:
                            Console.WriteLine("INSIRA A NOVA SITUAÇÃO DE TODAS AS PASSAGENS[L/V]:");
                            string novasit = Console.ReadLine().ToUpper();
                            while (novasit != "L" && novasit != "V")
                            {
                                Console.WriteLine("INSIRA UM VALOR VALIDO! [L/V]");
                                novasit = Console.ReadLine();
                            }
                            while (novasit.Length > 1)
                            {
                                Console.WriteLine("INSIRA UM VALOR VALIDO! [L/V]");
                                novasit = Console.ReadLine();
                            }
                            cmdupdate = $"UPDATE PASSAGEM SET SITUACAO = '{novasit}' WHERE ID_VOO = '{IdVoo}'";
                            banco.Update(cmdupdate);
                            opc = 8;
                            cmdselect = $"SELECT ID_PASSAGEM, ID_VOO, VALOR, DATA_ULTIMA_OPERACAO, SITUACAO FROM PASSAGEM WHERE ID_VOO= '{IdVoo}' and SITUACAO  ='{novasit}'";
                            banco.Select(cmdselect, opc);
                            break;

                        case 3:

                            if (VerificarIdPassagem())
                            {
                                Console.WriteLine("Insira a nova situação dessa passagem");
                                novasit = Console.ReadLine().ToUpper();
                                while (novasit != "L" && novasit != "V")
                                {
                                    Console.WriteLine("INSIRA UM VALOR VALIDO! [L/V]");
                                    novasit = Console.ReadLine();
                                }
                                while (novasit.Length > 1)
                                {
                                    Console.WriteLine("INSIRA UM VALOR VALIDO! [L/V]");
                                    novasit = Console.ReadLine();
                                }
                                cmdupdate = $"UPDATE PASSAGEM SET SITUACAO = '{novasit}' WHERE ID_PASSAGEM = '{Id_Passagem}'";
                                banco.Update(cmdupdate);
                                opc = 8;
                                cmdselect = $"SELECT ID_PASSAGEM, ID_VOO, VALOR, DATA_ULTIMA_OPERACAO, SITUACAO FROM PASSAGEM WHERE ID_VOO= '{IdVoo}' and ID_PASSAGEM = '{Id_Passagem}'";
                                banco.Select(cmdselect, opc);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Passagem não localizada");
                                Pausa();
                            }
                            break;
                    }
                }
            }
        }
       
        public bool VerificarIdPassagem()
        {    
            do
            {
                Console.WriteLine("INSIRA O ID DA PASSAGEM QUE DESEJA");
                Id_Passagem = Console.ReadLine().ToUpper();
                if (banco.VerificarDadoExistente(Id_Passagem, "ID_PASSAGEM", "PASSAGEM"))
                {
                    Console.WriteLine("Passagem localizada");
                    return true;
                }
                else
                {
                    Console.WriteLine("PASSAGEM NÃO ENCONTRADA! TENTE NOVAMENTE");

                    Pausa();
                    return false;

                }
            } while (Id_Passagem.Length == 0);
        }
        #endregion
        public bool AcharIdVoo()
        {
            do
            {
                Console.WriteLine("Insira o ID do VOO do qual pertencem as passagens que deseja");
                IdVoo = Console.ReadLine().ToUpper();
                if (banco.VerificarDadoExistente(IdVoo, "ID_VOO", "VOO"))
                {
                    Console.WriteLine("Voo localizad0");
                    return true;
                }
                else
                {
                    Console.WriteLine("VOO NÃO ENCONTRADO! TENTE NOVAMENTE MAIS TARDE");

                    Pausa();
                    return false;

                }
            } while (IdVoo.Length == 0);

        }
        public override string ToString()
        {
            return $"{Id_Passagem}{IdVoo}{DataCadastro}{Valor}{Situacao}";
        }         
      
    }
}




