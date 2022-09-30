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
                if(ValidarVoo())
                {
                    Console.WriteLine("PASSAGENS DISPONIVEIS");
                    cmdselect = $"SELECT AERONAVE.CAPACIDADE, VOO.ID_VOO FROM AERONAVE, VOO WHERE (VOO.AERONAVE = AERONAVE.INSCRICAO)";

                }

            }
        }

        public bool ValidarVoo()
        {
           
                Console.WriteLine("Insira o id do voo que deseja comprar passagens");
                string idvoo = Console.ReadLine();
                if (banco.VerificarDadoExistente(idvoo, "ID_VOO", "VOO"))
                {

                    Console.WriteLine("VOO Localizado");
                    return true;
                }
                else
                {
                    Console.WriteLine("VOO NÃO ENCONTRADO! TENTE NOVAMENTE MAIS TARDE");


                    return false;

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
    }
}
