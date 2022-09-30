using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace On_the_fly_2._0
{
    internal class Passageiro
    {
        private string Cpf { get; set; }
        private string Nome { get; set; }
        private DateTime DataNascimento { get; set; }
        private string Sexo { get; set; }
        private DateTime UltimaCompra { get; set; }
        private DateTime DataCadastro { get; set; }
        private string Situacao { get; set; }

        Banco banco = new Banco();

        public Passageiro()
        { }
        public void Pausa()
        {
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }

        public override string ToString()
        {
            return $"{Cpf}{Nome}{DataNascimento}{Sexo}{UltimaCompra}{DataCadastro}{Situacao}";
        }

        //Cadastra o CPF
        public bool CadastraCpf()
        {
            do
            {
                Console.Write("Digite seu CPF: ");
                Cpf = Console.ReadLine().Replace(".", "").Replace("-", "");
                if (Cpf == "0")
                    return false;
                if (!ValidaCPF(Cpf))
                {
                    Console.WriteLine("Digite um CPF Válido!");
                    Thread.Sleep(2000);
                }

                else
                {
                    if (banco.VerificarDadoExistente(Cpf, "CPF", "Passageiro"))
                    {
                        Console.WriteLine("cnpj ja cadastrado!");
                        //Thread.Sleep(2000);
                        Cpf = "";
                    }
                }
            } while (!ValidaCPF(Cpf));

            return true;
        }
        //Cadastra o Nome
        public bool CadastraNome()
        {
            do
            {
                Console.Write("Digite seu Nome (Max 50 caracteres): ");
                Nome = Console.ReadLine();
                if (Nome == "0")
                    return false;
                if (Nome.Length > 50)
                {
                    Console.WriteLine("Infome um nome menor que 50 caracteres!!!!");
                    Thread.Sleep(2000);
                }
            } while (Nome.Length > 50);

            for (int i = Nome.Length; i <= 50; i++)
                Nome += " ";
            return true;
        }
        //Cadastra a data de nascimento
        public bool CadastraDataNasc()
        {
            Console.Write("Digite sua data de nascimento (Mês/Dia/Ano): ");
            DateTime dataNasc;
            while (!DateTime.TryParse(Console.ReadLine(), out dataNasc))
            {
                //if (DataNascimento.Contains("0"))
                //    return false;
                //Console.WriteLine("Formato de data incorreto!");
                //Console.Write("Digite sua data de nascimento (Mês/Dia/Ano): ");
            }

            DataNascimento = dataNasc;
            return true;
        }
        //Cadastra o sexo do passageiro
        public bool CadastraSexo()
        {
            do
            {
                Console.WriteLine("Digite seu sexo [M] Masculino / [F] Feminino / [N] Prefere não informar: ");
                Sexo = Console.ReadLine().ToUpper();
                if (Sexo == "0")
                    return false;
                if (Sexo != "M" && Sexo != "N" && Sexo != "F")
                {
                    Console.WriteLine("Digite um opção válida!!!");
                    Thread.Sleep(2000);
                }
            } while (Sexo != "M" && Sexo != "N" && Sexo != "F");
            return true;
        }
        //Altera a situação do cadastro do passageiro
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
        //Cadastra um novo passageiro
        public void CadastraPassageiro()
        {
            Console.WriteLine(">>> CADASTRO DE PASSAGEIRO <<<");
            Console.WriteLine("Para cancelar o cadastro digite 0:\n");

            if (!CadastraCpf())
                return;

            if (!CadastraNome())
                return;

            if (!CadastraDataNasc())
                return;

            if (!CadastraSexo())
                return;

            UltimaCompra = DateTime.Now;

            DataCadastro = DateTime.Now;

            Situacao = "A";

            //Caminho para gravar o novo passageiro
            string cmdinsert = $"INSERT INTO Passageiro (CPF,NOME,SEXO,DATA_CADASTRO,DATA_NASCIMENTO,ULTIMA_COMPRA,SITUACAO) VALUES('{Cpf}','{Nome}','{Sexo}','{DataCadastro}','{DataNascimento}','{UltimaCompra}','{Situacao}');";
            banco.Insert(cmdinsert);

            Console.WriteLine("\nCADASTRO REALIZADO COM SUCESSO!\nPressione Enter para continuar...");
            Console.ReadKey();
        }


        public void AlterarCadastroPassageiro()
        {
            bool verifica;
            Console.WriteLine("ALTERAÇÃO DE DADOS");
            Console.WriteLine("Insira o CPF do passageiro que deseja alterar");
            string ncpf = Console.ReadLine();
            int opc = 1;
            string cmdselect = $"SELECT CPF, NOME, SEXO, DATA_CADASTRO, DATA_NASCIMENTO, ULTIMA_COMPRA, SITUACAO FROM PASSAGEIRO WHERE CPF = '{ncpf}'";

            verifica = banco.Select(cmdselect, opc);

            if (verifica)
            {


                Console.WriteLine("Que dado deseja alterar?");
                Console.WriteLine("1- nome");
                Console.WriteLine("2- sexo");
                Console.WriteLine("3- situação");
                string num;
                num = Console.ReadLine();
                do
                {


                    if (num != "1" && num != "2" && num != "3" && num != "0")
                    {
                        Console.WriteLine("Opção inválida!");
                        Thread.Sleep(3000);
                    }

                } while (num != "1" && num != "2" && num != "3" && num != "0");

                switch (num)
                {
                    case "1":
                        Console.WriteLine("Insira o novo nome");
                        string novonome = Console.ReadLine();
                        string cmdupdate = $"UPDATE Passageiro SET Nome = '{novonome}' where CPF = '{ncpf}'";
                        banco.Update(cmdupdate);

                        cmdselect = $"SELECT CPF, NOME, SEXO, DATA_CADASTRO, DATA_NASCIMENTO, ULTIMA_COMPRA, SITUACAO FROM PASSAGEIRO WHERE CPF = '{ncpf}'";
                        banco.Select(cmdselect, opc);

                        break;

                    case "2":
                        Console.WriteLine("Insira o novo sexo [F/M]");

                        string novosexo = Console.ReadLine().ToUpper();
                        while (novosexo.Length > 1)
                        {
                            Console.WriteLine("VALOR INVALIDO! Insira o novo sexo [F/M]");

                            novosexo = Console.ReadLine().ToUpper();
                        }

                        while (novosexo != "F" && novosexo != "M")
                        {
                            Console.WriteLine("VALOR INVALIDO! Insira o novo sexo [F/M]");

                            novosexo = Console.ReadLine().ToUpper();
                        }


                        cmdupdate = $"UPDATE Passageiro SET Sexo = '{novosexo}' where CPF = '{ncpf}'";
                        banco.Update(cmdupdate);

                        cmdselect = $"SELECT CPF, NOME, SEXO, DATA_CADASTRO, DATA_NASCIMENTO, ULTIMA_COMPRA, SITUACAO FROM PASSAGEIRO WHERE CPF = '{ncpf}'";
                        banco.Select(cmdselect, opc);

                        break;

                    case "3":
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
                        cmdupdate = $"UPDATE Passageiro SET Situacao = '{novasit}' where CPF = '{ncpf}'";
                        banco.Update(cmdupdate);

                        cmdselect = $"SELECT CPF, NOME, SEXO, DATA_CADASTRO, DATA_NASCIMENTO, ULTIMA_COMPRA, SITUACAO FROM PASSAGEIRO WHERE CPF = '{ncpf}'";
                        banco.Select(cmdselect, opc);

                        break;
                }
            }

            else 
                do
                {
                    {
                        Console.WriteLine("CPF INEXISTENTE");
                        ncpf = "";
                        Pausa();
                    }
                } while (ncpf.Length>11);
        }


        public void ImprimirPassageiro()
        {
            int opc = 1;
            Console.WriteLine("Lista de passageiros cadastros");

            string cmdselect = $"SELECT CPF, NOME, SEXO, DATA_CADASTRO, DATA_NASCIMENTO, ULTIMA_COMPRA, SITUACAO FROM PASSAGEIRO";
            banco.Select(cmdselect, opc);

        }

        public void LocalizarPassageiro()
        {

            Console.WriteLine("LOCALIZAÇÃO DE PASSAGEIROS");
            Console.WriteLine("Insira o CPF do passageiro que deseja localizar");
            string ncpf = Console.ReadLine();
            int opc = 1;
            string cmdselect = $"SELECT CPF, NOME, SEXO, DATA_CADASTRO, DATA_NASCIMENTO, ULTIMA_COMPRA, SITUACAO FROM PASSAGEIRO WHERE CPF = '{ncpf}'";
            banco.Select(cmdselect, opc);
        }



  

        #region valida cpf
        private static bool ValidaCPF(string vrCPF)
        {
            string valor = vrCPF.Replace(".", "");

            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)

                if (valor[i] != valor[0])

                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)

                numeros[i] = int.Parse(

                  valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)

                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)

            {
                if (numeros[9] != 0)
                    return false;
            }

            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)

            {
                if (numeros[10] != 0)
                    return false;
            }

            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }
        #endregion
    }
}