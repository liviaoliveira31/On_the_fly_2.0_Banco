using System;
using System.Data.SqlClient;

namespace On_the_fly_2._0
{
    internal class Program
    {

       

        static void Pausa()
        {
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }
        static void Main(string[] args)
        {


            MostrarMenuInicial();
        }

        static void MostrarMenuInicial()
        {
            int opcao = 7;
            do
            {
                Console.Clear();
                Console.WriteLine(" °°°  MENU  INICIAL  °°°");
                Console.WriteLine(" Opção 1 : Menu cadastro");
                Console.WriteLine(" Opção 2 : Menu localizar");
                Console.WriteLine(" Opção 3 : Menu editar");
                Console.WriteLine(" Opção 4 : Menu imprimir");
                Console.WriteLine(" Opção 5 : Menu bloqueados");
                Console.WriteLine(" Opção 6 : Menu restritos");
                Console.WriteLine(" Opção 7 : IATAS disponiveis");

                Console.WriteLine(" Opção 0 : Sair");

                Console.Write("\n Informe a opção: ");

                do
                {
                    try
                    {
                        opcao = int.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                    }

                    switch (opcao)
                    {
                        case 0:
                            Environment.Exit(0);
                            break;

                        case 1:
                            Console.Clear();
                            MostrarMenuCadastrar();
                            break;

                        case 2:
                            Console.Clear();
                            MostrarMenuLocalizar();
                            break;

                        case 3:
                            Console.Clear();
                            MostrarMenuEditar();
                            break;

                        case 4:
                            Console.Clear();
                            MostrarMenuImprimir();

                            break;

                        case 5:
                            Console.Clear();
                            Bloqueado b = new Bloqueado();
                            b.GerarMenu();
                            MostrarMenuInicial();
                            break;

                        case 6:
                            Console.Clear();
                            Restrito r = new Restrito();
                            r.GerarMenu();
                            MostrarMenuInicial();
                            break;
                        case 7:
                         Console.Clear();
                            IATAS veriata = new();
                            veriata.veriatas();
                            MostrarMenuInicial();

                            break;
                       
                        
                        default:
                            Console.Write("\n Opcao Inválida!\n Digite novamente: ");
                            break;
                    }
                } while (opcao > 7);
            } while (true);
        }


        static void MostrarMenuCadastrar()
        {
            int opcao = 7;

            Passageiro passageiro = new();
            CompanhiaAerea companhiaAerea = new();
            Aeronave aeronave = new();
            VOO voo = new();
            Venda venda = new();

            Console.WriteLine(" °°°  MENU  CADASTRO  °°°");
            Console.WriteLine(" Opção 1 : Cadastrar passageiro");
            Console.WriteLine(" Opção 2 : Cadastrar companhia aerea");
            Console.WriteLine(" Opção 3 : Cadastrar aeronave");
            Console.WriteLine(" Opção 4 : Cadastro de voo");
            Console.WriteLine(" Opção 5 : Cadastrar venda de passagem");
            Console.WriteLine(" Opção 6 : Voltar ao Menu Iniciar");
            Console.WriteLine(" Opção 0 : Sair");

            Console.Write("\n Informe a opção: ");

            do
            {
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }
                switch (opcao)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine("Cadastrar passageiro");
                        Console.Clear();
                        passageiro.CadastraPassageiro();
                        Console.ReadKey();
                        MostrarMenuInicial();
                        break;
                    case 2:
                        Console.WriteLine(" Cadastrar companhia aerea");
                        Console.Clear();
                        companhiaAerea.CadCompanhia();
                        Console.ReadKey();
                        MostrarMenuInicial();
                        break;
                    case 3:
                        Console.WriteLine(" Cadastrar aeronave");
                        Console.Clear();
                        aeronave.CadastraAeronave();
                        MostrarMenuInicial();
                        break;

                    case 4:                       
                        Console.Clear();
                        voo.CadastrarVoo();
                        MostrarMenuInicial();
                        break;

                    case 5:
                        Console.WriteLine(" Cadastrar venda de passagem");
                        Console.Clear();
                        venda.RealizarVenda();
                        MostrarMenuInicial();
                        break;
                    case 6:
                        Console.WriteLine(" Menu Inicial");
                        MostrarMenuInicial();
                        break;

                    default:
                        Console.Write("\n Opção invalida\n Digite novamente:");
                        break;
                }
            } while (true);
        }

        #region menu localizar
        static void MostrarMenuLocalizar() //inoperante
        {
            int opcao = 8;

            Passageiro passageiro = new();
            CompanhiaAerea companhiaAerea = new();
            Aeronave aeronave = new();
            VOO voo = new();
            PassagemVoo passagemVoo = new();
            //Venda venda = new();

            Console.WriteLine(" °°°  MENU  LOCALIZAR  °°°");
            Console.WriteLine(" Opção 1 : Localizar passageiro");
            Console.WriteLine(" Opção 2 : Localizar companhia aerea");
            Console.WriteLine(" Opção 3 : Localizar aeronave");
            Console.WriteLine(" Opção 4 : Localizar voo");
            Console.WriteLine(" Opção 5 : Localizar passagem");
            //Console.WriteLine(" Opção 6 : Localizar venda de passagem");
            Console.WriteLine(" Opção 7 : Voltar ao Menu Iniciar");
            Console.WriteLine(" Opção 0 : Sair");

            Console.Write("\n Informe a opção: ");

            do
            {
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }

                switch (opcao)
                {
                    case 0:
                        Environment.Exit(0);
                        break;

                    case 1:
                        Console.WriteLine("Localizar passageiro");
                        Console.Clear();
                        passageiro.LocalizarPassageiro();
                        passageiro.Pausa();
                        MostrarMenuInicial();
                        break;

                    case 2:
                        Console.WriteLine("Localizar companhia aerea");
                        Console.Clear();
                        companhiaAerea.LocalizarCompanhia();
                        companhiaAerea.Pausa();
                        MostrarMenuInicial();

                        break;

                    case 3:
                        Console.WriteLine("Localizar aeronave");
                        Console.Clear();
                        aeronave.LocalizarAeronave();
                        MostrarMenuInicial();
                        break;

                    case 4:
                        Console.WriteLine("Localizar voo");
                        Console.Clear();
                        voo.BuscarVoo();
                        break;

                    case 5:
                        Console.Clear();
                        passagemVoo.LocalizarPassagem();
                       
                        MostrarMenuInicial();
                        break;

                    //case 6:
                    //    Console.WriteLine("Localizar venda de passagem");
                    //    Console.Clear();
                    //    //venda.Localizar();
                    //    break;

                    case 7:
                        Console.Clear();
                        MostrarMenuInicial();
                        break;

                    default:
                        Console.Write("\n Opcao Inválida!\n Digite novamente: ");
                        break;
                }

            } while (true);
        }
        #endregion

        static void MostrarMenuEditar()
        {
            int opcao = 0;

            Passageiro passageiro = new();
            CompanhiaAerea companhiaAerea = new();
            Aeronave aeronave = new();
            VOO voo = new();
            PassagemVoo passagemVoo = new();

            Console.WriteLine(" °°°  MENU  EDITAR  °°°");
            Console.WriteLine(" Opção 1 : Editar passageiro");
            Console.WriteLine(" Opção 2 : Editar companhia aerea");
            Console.WriteLine(" Opção 3 : Editar aeronave");
            Console.WriteLine(" Opção 4 : Editar voo");
            Console.WriteLine(" Opção 5 : Editar valor passagem");
            Console.WriteLine(" Opção 6 : Voltar ao Menu Iniciar");
            Console.WriteLine(" Opção 0 : Retorna ao menu INICIAR");

            Console.Write("\n Informe a opção: ");
            do
            {
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine(" Retornando ao menu anterior");
                        Console.WriteLine();
                        Console.Clear();
                        break;

                    case 1:
                        Console.WriteLine("Editar passageiro");
                        Console.Clear();
                        passageiro.AlterarCadastroPassageiro();
                        MostrarMenuInicial();
                        break;

                    case 2:
                        Console.WriteLine("Editar companhia aerea");
                        Console.Clear();
                        companhiaAerea.AlterarCadastroCompanhia();
                        companhiaAerea.Pausa();
                        MostrarMenuInicial();
                        break;

                    case 3:
                        Console.WriteLine("Editar aeronave");
                        Console.Clear();
                        aeronave.AlterarCadastroAeronave();
                        MostrarMenuInicial();
                        break;

                    case 4:
                        Console.WriteLine("Editar voo");
                        Console.Clear();
                        voo.AlterarVoo();
                        MostrarMenuInicial();
                        break;

                    case 5:
                        Console.WriteLine("Editar passagem");
                        Console.Clear();
                        passagemVoo.AlterarPassagens();
                        MostrarMenuInicial();
                        break;

                    case 6:
                        Console.Clear();
                        MostrarMenuInicial();
                        break;

                    default:
                        Console.Write("\n Opcao Inválida!\n Digite novamente: ");
                        break;
                }

            } while (opcao > 6);

            return;
        }

        static void MostrarMenuImprimir()
        {
            int opcao = 8;

            Passageiro passageiro = new();
            CompanhiaAerea companhiaAerea = new();
            Aeronave aeronave = new();
            VOO voo = new();
            PassagemVoo passagemVoo = new();
            //Venda venda = new();

            Console.WriteLine(" °°°  MENU  IMPRIMIR  °°°");
            Console.WriteLine(" Opção 1 : Imprime passageiros");
            Console.WriteLine(" Opção 2 : Imprime companhias aereas");
            Console.WriteLine(" Opção 3 : Imprime aeronaves");
            Console.WriteLine(" Opção 4 : Imprime voos");
            Console.WriteLine(" Opção 5 : Imprime passagens");
            //Console.WriteLine(" Opção 6 : Imprime venda de passagens");
            Console.WriteLine(" Opção 7 : Voltar ao Menu Iniciar");
            Console.WriteLine(" Opção 0 : Sair");

            Console.Write("\n Informe a opção: ");

            do
            {
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }

                switch (opcao)
                {
                    case 0:
                        Environment.Exit(0);
                        break;

                    case 1:
                        Console.WriteLine("Imprime passageiro");
                        Console.Clear();
                        passageiro.ImprimirPassageiro();
                        MostrarMenuInicial();
                        break;

                    case 2:
                        Console.WriteLine("Imprime companhia aerea");
                        Console.Clear();
                        companhiaAerea.ImprimirCompanhia();
                        MostrarMenuInicial();
                        break;

                    case 3:
                        Console.WriteLine("Imprime aeronave");
                        Console.Clear();
                        aeronave.ImprimirAeronave();
                        MostrarMenuInicial();
                        break;

                    case 4:               
                        Console.Clear();
                        voo.ImprimirVoo();
                        MostrarMenuInicial();
                        break;

                    case 5:
                        Console.Clear();
                        passagemVoo.imprimirPassagem();
                        MostrarMenuInicial();
                        break;

                    //case 6:
                    //    Console.WriteLine("Imprime venda de passagem");
                    //    Console.Clear();
                    //    //venda.Imprimir();
                    //    break;

                    case 7:
                        Console.Clear();
                        MostrarMenuInicial();
                        break;

                    default:
                        Console.Write("\n Opcao Inválida!\n Digite novamente: ");
                        break;
                }

            } while (true);
        }
    }
}

