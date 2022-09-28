using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace On_the_fly_2._0
{
    internal class Aeronave
    {
        public string Inscricao { get; set; }
        public string CNPJ { get; set; }
        public string Capacidade { get; set; }
        public string AssentosOcupados { get; set; }
        public DateTime UltimaVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Situacao { get; set; }
        Banco banco = new Banco();
        public Aeronave()
        {
        }
        public override string ToString()
        {
            return $"{Inscricao}{CNPJ}{Capacidade}{AssentosOcupados}{UltimaVenda}{DataCadastro}{Situacao}";
        }

        public void CadastraAeronave()
        {
            Console.WriteLine(">>> CADASTRO DE AERONAVE <<<");
            if (!CadastroCNPJ())
                return;
            if (!CadastraIdAeronave())
                return;
            CadastraCapacidade();
            AssentosOcupados = "000";
            UltimaVenda = DateTime.Now;
            DataCadastro = DateTime.Now;
            Situacao = "A";
            string cmdinsert = $"INSERT INTO Aeronave (Inscricao, CNPJ, Capacidade, AssentosOcupados, Data_Cadastro, Ultima_Venda, Situacao)  VALUES('{Inscricao}','{CNPJ}','{Capacidade}','{AssentosOcupados}','{UltimaVenda}','{DataCadastro}','{Situacao}');";
            banco.Insert(cmdinsert);

            Console.WriteLine("\n CADASTRO REALIZADO COM SUCESSO!\nPressione Enter para continuar...");
            Console.ReadKey();

            Console.ReadKey();
        }

        public void AlterarCadastroAeronave()
        {
            bool verifica;
            Console.WriteLine("ALTERAÇÃO DE DADOS");
            Console.WriteLine("Insira a inscrição da aeronave que deseja alterar");
            string naero = Console.ReadLine().ToUpper().Trim().Replace("-", "");
            int opc = 3;
            string cmdselect = $"SELECT Inscricao, CNPJ, Capacidade, AssentosOcupados, Data_Cadastro, Ultima_Venda, Situacao FROM AERONAVE WHERE INSCRICAO = '{naero}'";

            verifica = banco.Select(cmdselect, opc);

            if (verifica)
            {


                Console.WriteLine("Que dado deseja alterar?");
                Console.WriteLine("1- Capacidade");
                Console.WriteLine("2- situação");
                string num;
                num = Console.ReadLine();
                do
                {


                    if (num != "1" && num != "2"  && num != "0")
                    {
                        Console.WriteLine("Opção inválida!");
                        Thread.Sleep(3000);
                    }

                } while (num != "1" && num != "2"  && num != "0");

                switch (num)
                {
                    case "1":
                        Console.WriteLine("Insira a capacidade");
                        string novacap = Console.ReadLine();
                        string cmdupdate = $"UPDATE Aeronave SET Capacidade = '{novacap}' where INSCRICAO = '{naero}'";
                        banco.Update(cmdupdate);

                        cmdselect = $"SELECT Inscricao, CNPJ, Capacidade, AssentosOcupados, Data_Cadastro, Ultima_Venda, Situacao FROM AERONAVE WHERE INSCRICAO = '{naero}'";

                        verifica = banco.Select(cmdselect, opc);

                        break;

                 

                    case "2":
                        Console.WriteLine("Insira a nova situação: [A/I] ");
                        string novasit = Console.ReadLine().ToUpper();
                        while (novasit.Length > 1)
                        {
                            Console.WriteLine("VALOR INVALIDO! Insira a nova situação [A/I]");
                            novasit = Console.ReadLine().ToUpper();
                        }

                        while (novasit != "A" && novasit != "I")
                        {
                            Console.WriteLine("VALOR INVALIDO! Insira a nova situação [A/I]");
                            novasit = Console.ReadLine().ToUpper();
                        }
                        cmdupdate = $"UPDATE Aeronave SET Situacao= '{novasit}' where Inscricao = '{naero}'";
                        banco.Update(cmdupdate);

                   
                        break;
                }
            }
        }

        public void ImprimirAeronave()
        {
            int opc = 3;
            Console.WriteLine("Lista de Aeronaves");

            string cmdselect = $"SELECT Inscricao, CNPJ, Capacidade, AssentosOcupados, Data_Cadastro, Ultima_Venda, Situacao FROM AERONAVE WHERE situacao = 'A'";

            banco.Select(cmdselect, opc);

        }

        public void LocalizarAeronave()
        {
            int opc = 3;
            Console.WriteLine("LOCALIZAÇÃO DE AERONAVE");
            Console.WriteLine("Insira a inscrição da aeronave que deseja localizar");
            string naero = Console.ReadLine().ToUpper().Trim().Replace("-", "");

            string cmdselect = $"SELECT Inscricao, CNPJ, Capacidade, AssentosOcupados, Data_Cadastro, Ultima_Venda, Situacao FROM AERONAVE";
            banco.Select(cmdselect, opc);
          
        }
        public bool CadastraIdAeronave()
        {
            do
            {
                Console.Write("Informe o código de identificação da aeronave seguindo o padrão definido pela ANAC (XX-XXX):");
                Inscricao = Console.ReadLine().ToUpper().Trim().Replace("-", "");

                if (banco.VerificarDadoExistente(Inscricao, "Inscricao", "Aeronave"))
                {
                    Console.WriteLine("Codigo de inscrição existente!");
                    //Thread.Sleep(2000);
                    Inscricao = "";
                }

            } while (Inscricao.Length != 5);

            return true;
        }
        public bool CadastraCapacidade()
        {
            do
            {
                Console.Write("Informe a capacidade de pessoas que a aeronave comporta: ");
                Capacidade = Console.ReadLine();
            } while (int.Parse(Capacidade) < 0 || int.Parse(Capacidade) > 999);
            if (int.Parse(Capacidade) > 9 && int.Parse(Capacidade) < 100)
            {
                Capacidade = "0" + Capacidade;
            }
            if (int.Parse(Capacidade) < 10)
            {
                Capacidade = "00" + Capacidade;
            }
            return true;
        }
        public bool AlteraSituacao()
        {
            string num;
            do
            {
                Console.Write("Alterar Situação [A] Ativo / [I] Inativo / [0] Cancelar: ");
                num = Console.ReadLine().ToUpper();
                if (num != "A" && num != "I" && num != "0")
                {
                    Console.WriteLine("Digite um opção válida!!!");
                    Thread.Sleep(2000);
                }
            } while (num != "A" && num != "I" && num != "0");
            if (num.Contains("0"))
                return false;
            Situacao = num;
            return true;
        }

        public bool CadastroCNPJ()
        {
            do
            {
                Console.Write("Informe o CNPJ da qual a aeronave pertence:");
                CNPJ = Console.ReadLine().Replace(".", "").Replace("-", "").Replace("/", "");

                if (banco.VerificarDadoExistente(CNPJ, "CNPJ", "Companhia_Aerea"))
                {
                    return true;
                    //Console.WriteLine("Codigo de inscrição existente!");
                    ////Thread.Sleep(2000);

                }
                else
                {
                    Console.WriteLine("CNPJ NÃO ENCONTRADO");
                    CNPJ = "";

                }
            } while (CNPJ.Length == 0);
            return false;
        }


    }
}
