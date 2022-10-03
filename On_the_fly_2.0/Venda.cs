using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace On_the_fly_2._0
{
    internal class Venda
    {

        public int Id { get; set; }
        public string DataVenda { get; set; }
        public string Passageiro { get; set; }
        public string ValorTotal { get; set; }

        Banco banco = new Banco();
        public Venda() { }

        //public override string ToString()
        //{
        //    return $"{Id}{DataVenda}{Passageiro}{ValorTotal}";
        //}
        public override string ToString()
        {
            return $"{Id.ToString("00000")}{Passageiro}{DataVenda}{ValorTotal}";


        }

        //public bool verificarpassageiro()
        //{


        //}

        public void RealizarVenda()
        {
            if (AcharPassageiro()) 
            {
                string cmdselect = $"SELECT CPF, NOME, SEXO, DATA_CADASTRO, DATA_NASCIMENTO, ULTIMA_COMPRA, SITUACAO FROM PASSAGEIRO WHERE CPF = '{Passageiro}' ";
                int opc = 1;
                banco.Select(cmdselect, opc);
                Console.WriteLine("BEM VINDO AO MENU DE COMPRAS! VOOS DISPONIVEIS PARA COMPRA:");
                cmdselect = $"SELECT ID_VOO, AERONAVE, IATA, DATA_VOO, DATA_CADASTRO, SITUACAO FROM VOO WHERE SITUACAO = 'A'";
                opc = 7;
                banco.Select(cmdselect, opc);
               string idvoo =  ValidarVoo();
                
                    Console.WriteLine("PASSAGENS DISPONIVEIS");
                opc = 8;
                cmdselect = $"SELECT Id_Passagem,Id_Voo,Valor,Data_Ultima_Operacao,Situacao FROM PASSAGEM WHERE ID_VOO = '{idvoo}' AND SITUACAO = 'L'";
                banco.Select(cmdselect, opc);
                Console.WriteLine();

                

            }
        }

        public string ValidarVoo()
        {
           
                Console.WriteLine("Insira o id do voo que deseja comprar passagens");
                string idvoo = Console.ReadLine();
                if (banco.VerificarDadoExistente(idvoo, "ID_VOO", "VOO"))
                {

                    Console.WriteLine("VOO Localizado");
                    return idvoo ;
                }
                else
                {
                    Console.WriteLine("VOO NÃO ENCONTRADO! TENTE NOVAMENTE MAIS TARDE");


                    return "";

                }
           
        }

        public bool AcharPassageiro()
        {
            do
            {
                Console.WriteLine("Insira seu cpf");
                Passageiro = Console.ReadLine().Replace(".", "").Replace("-", "");
                if (banco.VerificarDadoExistente(Passageiro, "CPF", "Passageiro"))
                {
                   
                    if (banco.VerificarDadoExistente(Passageiro, "CPF", "RESTRITO"))
                    {
                        Console.WriteLine("CPF RESTRITO. CONSULTE A RECEITA FEDERAL PARA MAIS INFORMAÇÕES");
                        return false;
                    }
                    //else
                    //{
                    //    string cmdselect = $"SELECT DATA_NASCIMENTO FROM PASSAGEIRO WHERE CPF ='{Passageiro}";
                    //    banco.SelectGenerico(cmdselect);
                    //}
                    Console.WriteLine("Passageiro Localizado");
                    return true;
                }
                else
                {
                    Console.WriteLine("VOO NÃO ENCONTRADO! TENTE NOVAMENTE MAIS TARDE");

                   
                    return false;

                }
            } while (Passageiro.Length == 0);

        }

        public bool ValidarIdPassagem()
        {
            Console.WriteLine("Insira o id da passagem que deseja comprar:");
            string idpas = Console.ReadLine();
            
            if (banco.VerificarDadoExistente(idpas ,"ID_PASSAGEM", "PASSAGEM"))
            {

               
                Console.WriteLine("Passagem localizada");
                Console.WriteLine("INSIRA [S] PARA COMPRAR E [N] PARA VOLTAR");
                string resp = Console.ReadLine().ToUpper();
                while (resp != "S" && resp != "N")
                {
                    Console.WriteLine("VALOR INVALIDO! INSIRA [S] PARA COMPRAR E [N] PARA VOLTAR");
                    resp = Console.ReadLine().ToUpper();
                }
                if(resp == "S")
                {
                    Console.WriteLine("PASSAGEM COMPRADA COM SUCESSO!");
                    string cmdupdate = $"UPDATE PASSAGEM SET SITUACAO = 'L' WHERE ID_PASSAGEM = '{idpas}'";
                }
                return true;
            }
            else
            {
                Console.WriteLine("VOO NÃO ENCONTRADO! TENTE NOVAMENTE MAIS TARDE");


                return false;

            }
        }
    }
}
