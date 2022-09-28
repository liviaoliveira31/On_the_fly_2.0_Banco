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
        public int AssentosOcupados { get; set; }
        public DateTime DataVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Situacao { get; set; }

        Banco banco = new Banco();


        public VOO()
        {

        }

        public override string ToString()
        {
            return $"{Id}{Aeronave}{Destino}{AssentosOcupados}{DataVoo}{DataCadastro}{Situacao}";
        }

        public void CadastrarVoo()
        {
            Console.WriteLine(">>> CADASTRO DE VOO <<<");

            BuscarIata();

            BuscarAeronave();

            if (!GerarIdVoo())
                return;

            Console.Write("Informe a data E HORA de partida do voo: ");
            DateTime dataVoo;
            while (!DateTime.TryParse(Console.ReadLine(), out dataVoo))
            {
                Console.Write("Informe a data de partida do voo: ");
            }

            //Console.Write("Informe a hora de partida do voo: ");
            //DateTime horaVoo;
            //while (!DateTime.TryParse(Console.ReadLine(), out horaVoo))
            //{
            //    Console.Write("Informe a hora de partida do voo: ");
            //}

            DataVoo = dataVoo;

            DataCadastro = DateTime.Now;

            Situacao = "A";

            PassagemVoo passagemVoo = new();
            //passagemVoo.GerarPassagem(IdAeronave, Id);

            string cmdinsert = $"INSERT INTO VOO(ID_VOO, AERONAVE, DESTINO, ASSENTOS_OCUPADOS, DATA_VOO, DATA_CADASTRO, SITUACAO) VALUES('{Id}','{Aeronave}','{Destino}','{AssentosOcupados}','{DataVoo}','{DataCadastro}','{Situacao}');";
            banco.Insert(cmdinsert);
            Console.WriteLine("\n CADASTRO REALIZADO COM SUCESSO!\nPressione Enter para continuar...");
            Console.ReadKey();


        }


        public bool BuscarIata()
        {
            do
            {
                Console.Write("Informe a IATA do destino:");
                Destino = Console.ReadLine();

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
                Aeronave = Console.ReadLine();

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
            return true;
            //if (banco.VerificarDadoExistente(Id, "VOO", "ID_VOO"))
            //{
            //    return false;


            //}
            //else
            //{
            //    return true;

            //}

        }








    }
}
