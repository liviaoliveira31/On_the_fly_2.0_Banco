using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace On_the_fly_2._0
{
    internal class VOO
    {

        public string Id { get; set; }
        public string Aeronave { get; set; }
        public string Destino { get; set; }
        public double Preco_passagem { get; set; }
        public string DataVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Situacao { get; set; }

        Banco banco = new Banco();
        public VOO()
        {

        }
        public void Pausa()
        {
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }
        public override string ToString()
        {
            return $"{Id}{Aeronave}{Destino}{Preco_passagem}{DataVoo}{DataCadastro}{Situacao}";
        }

        #region cadastro e verificações do voo
        public bool BuscarIata()
        {
            do
            {
                Console.Write("Informe a IATA do destino:");
                Destino = Console.ReadLine().ToUpper();

                if (banco.VerificarDadoExistente(Destino, "IATA", "IATA"))
                {
                    Console.WriteLine("encontrado");
                    return true;
                    //Console.WriteLine("Codigo de inscrição existente!");
                    ////Thread.Sleep(2000);

                }
                else
                {
                    Console.WriteLine("Destino NÃO ENCONTRADA");
                    Destino = "";

                }
            } while (Destino.Length == 0);
            return false;
        }
        public bool BuscarAeronave()
        {
            do
            {
                Console.Write("Informe a aeronave que vai realizar o voo:");
                Aeronave = Console.ReadLine().ToUpper();

                if (banco.VerificarDadoExistente(Aeronave, "Inscricao", "Aeronave"))
                {
                    return true;
                    //Console.WriteLine("Codigo de inscrição existente!");
                    ////Thread.Sleep(2000);

                }
                else
                {
                    Console.WriteLine("AERONAVE NÃO ENCONTRADA");
                    Aeronave = "";

                }
            } while (Aeronave.Length == 0);
            return false;
        }
        public bool GerarIdVoo()
        {
            Random random = new Random();
            Id = "V" + random.Next(0001, 9999).ToString("0000");

            if (banco.VerificarDadoExistente(Id, "ID_VOO", "VOO"))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public void CadastrarVoo()
        {
            Console.WriteLine(">>> CADASTRO DE VOO <<<");

            BuscarIata();

            BuscarAeronave();

            if (!GerarIdVoo())
                return;
            Console.WriteLine("ID DO VOO: "+Id);

            Console.Write("Informe a data de partida do voo: ");
            DateTime dataVoo;
            while (!DateTime.TryParse(Console.ReadLine(), out dataVoo))
            {
                Console.Write("Informe a data de partida do voo: ");
            }
            Console.Write("Informe a hora de partida do voo: ");
            DateTime horaVoo;
            while (!DateTime.TryParse(Console.ReadLine(), out horaVoo))
            {
                Console.Write("Informe a hora de partida do voo: ");
            }
            DataVoo = dataVoo.ToString("dd-MM-yyyy") + horaVoo.ToString(" HH:mm");
            DataCadastro = DateTime.Now;

            Situacao = "A";
            //Console.WriteLine("Informe o preço das passagens:");
            //Preco_passagem = float.Parse(Console.ReadLine());


            string cmdinsert = $"INSERT INTO VOO(ID_VOO, AERONAVE, iata,  DATA_VOO, DATA_CADASTRO, SITUACAO) VALUES('{Id}','{Aeronave}','{Destino}','{DataVoo}','{DataCadastro}','{Situacao}');";
            banco.Insert(cmdinsert);

            PassagemVoo passagemVoo = new();
            passagemVoo.GerarPassagem(Aeronave, Id);  //passa o cod da aeronave e o id do voo
            Console.WriteLine("\n CADASTRO REALIZADO COM SUCESSO!\nPressione Enter para continuar...");
            Console.ReadKey();


        }
        #endregion

        #region buscar e imprimir voo
        public void BuscarVoo()
        {

            if (AcharIdVoo())
            {
                int opc = 7;
                string cmdselect = $"SELECT ID_VOO, AERONAVE, iata,  DATA_VOO, DATA_CADASTRO, SITUACAO FROM VOO WHERE ID_VOO = '{Id}'";
                banco.Select(cmdselect, opc);
            }
        }

        public void ImprimirVoo()

        {
            Console.WriteLine("VOOS CADASTRADOS");
            int opc = 7;
            Console.WriteLine("\n VOOS ATIVOS");
            string cmdselect = $"SELECT ID_VOO, AERONAVE, iata,  DATA_VOO, DATA_CADASTRO, SITUACAO FROM VOO WHERE SITUACAO = 'A' ";
            banco.Select(cmdselect, opc);
            Console.WriteLine("\n VOOS INATIVOS");
            cmdselect = $"SELECT ID_VOO, AERONAVE, iata,  DATA_VOO, DATA_CADASTRO, SITUACAO FROM VOO WHERE SITUACAO = 'I' ";
            banco.Select(cmdselect, opc);
        }

        public bool AcharIdVoo()
        {
            do
            {
                Console.WriteLine("Insira o ID do voo que deseja localizar");
                Id = Console.ReadLine().ToUpper();
                if (banco.VerificarDadoExistente(Id, "ID_VOO", "VOO"))
                {
                    Console.WriteLine("Voo localizado");
                    return true;
                }
                else
                {
                    Console.WriteLine("VOO NÃO ENCONTRADO! TENTE NOVAMENTE MAIS TARDE");

                    Pausa();
                    return false;

                }
            } while (Id.Length == 0);

        }
        #endregion

        #region alterações de voo
        public void AlterarVoo()
        {
            bool verifica;
            Console.WriteLine("ALTERAÇÃO DE DADOS");
            if (AcharIdVoo())
            {
                int opc = 7;
                string cmdselect = $"SELECT ID_VOO, AERONAVE, iata, PRECO_PASSAGEM, DATA_VOO, DATA_CADASTRO, SITUACAO FROM VOO WHERE ID_VOO = '{Id}'";
                banco.Select(cmdselect, opc);

                verifica = banco.Select(cmdselect, opc);

                if (verifica)
                {
                    Console.WriteLine("QUAL DADO DESEJA ALTERAR? ");
                    Console.WriteLine("1- AERONAVE");
                    Console.WriteLine("2- DESTINO");
                    Console.WriteLine("3- SITUAÇÃO");
                    int op = int.Parse(Console.ReadLine());

                    switch (op)
                    {
                        case 1:

                            if (!AlterarAeronave())
                                return;
                            string cmdupdate = $"UPDATE VOO SET AERONAVE = '{Aeronave}' ";
                            banco.Update(cmdupdate);
                            Console.WriteLine("Aeronave alterada com sucesso");
                            opc = 7;
                            cmdselect = $"SELECT ID_VOO, AERONAVE, iata, PRECO_PASSAGEM, DATA_VOO, DATA_CADASTRO, SITUACAO FROM VOO WHERE ID_VOO = '{Id}'";
                            banco.Select(cmdselect, opc);

                            break;

                        case 2:
                            if (!AlterarDestino())
                                return;

                            cmdupdate = $"UPDATE VOO SET DESTINO = '{Destino}' ";
                            banco.Update(cmdupdate);
                            Console.WriteLine("Destino alterada com sucesso");
                            opc = 7;
                            cmdselect = $"SELECT ID_VOO, AERONAVE, iata, PRECO_PASSAGEM, DATA_VOO, DATA_CADASTRO, SITUACAO FROM VOO WHERE ID_VOO = '{Id}'";
                            banco.Select(cmdselect, opc);
                            break;

                        case 3:
                            Console.WriteLine("QUAL A NOVA SITUAÇÃO DO VOO? [A/C]");
                            string novasit = Console.ReadLine().ToUpper();
                            while (novasit != "A" && novasit != "C")
                            {
                                Console.WriteLine("insira uma opção valida [A/C]");
                                novasit = Console.ReadLine();
                            }
                            while (novasit.Length > 1)
                            {
                                Console.WriteLine("insira uma opção valida [A/C]");
                                novasit = Console.ReadLine();
                            }

                            cmdupdate = $"UPDATE VOO SET situacao = '{novasit}' ";
                            Console.WriteLine("SITUAÇÃO ALTERADA COM SUCESSO!");
                            banco.Update(cmdupdate);
                            opc = 7;
                            cmdselect = $"SELECT ID_VOO, AERONAVE, iata, PRECO_PASSAGEM, DATA_VOO, DATA_CADASTRO, SITUACAO FROM VOO WHERE ID_VOO = '{Id}'";
                            banco.Select(cmdselect, opc);
                            break;

                    }

                }
                else
                {
                    Console.WriteLine("VOO NAO ENCONTRADO");
                    Pausa();
                }
            }
           
        }
        public bool AlterarAeronave()
        {
            do
            {
                Console.Write("Informe a aeronave que vai realizar o voo:");
                Aeronave = Console.ReadLine().ToUpper();

                if (banco.VerificarDadoExistente(Aeronave, "Inscricao", "Aeronave"))
                {
                    Console.WriteLine("AERONAVE ENCONTRADA!");
                    return true;


                }
                else
                {
                    Console.WriteLine("AERONAVE NÃO ENCONTRADA, TENTE NOVAMENTE");
                    Aeronave = "";

                }
            } while (Aeronave.Length == 0);
            return false;
        }
        public bool AlterarDestino()
        {
            do
            {
                Console.Write("Informe a IATA do novo destino:");
                Destino = Console.ReadLine().ToUpper();

                if (banco.VerificarDadoExistente(Destino, "IATA", "IATA"))
                {
                    Console.WriteLine("IATA ENCONTRADA");
                    return true;
                    //Console.WriteLine("Codigo de inscrição existente!");
                    ////Thread.Sleep(2000);

                }
                else
                {
                    Console.WriteLine("IATA NÃO ENCONTRADA, TENTE NOVAMENTE");
                    Destino = "";

                }
            } while (Destino.Length == 0);
            return false;
        }

#endregion



    }
}

