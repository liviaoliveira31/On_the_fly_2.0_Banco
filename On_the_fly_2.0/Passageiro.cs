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
        private DateTime DataNascimento;
        private string Sexo { get; set; }
        private DateTime UltimaCompra;
        private DateTime DataCadastro;
        private string Situacao { get; set; }

        Banco banco = new Banco();

        public Passageiro()
        {

        }
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
            } while (!ValidaCPF(Cpf));

            //if (VerificaPassageiro(this.caminho, Cpf))
            //{
            //    Console.WriteLine("Este CPF já está cadastrado!!");
            //    Thread.Sleep(3000);
            //    return false;
            //}
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

            else if (verifica == false)
                do
                {
                    {
                        Console.WriteLine("CPF INEXISTENTE");
                        
                    }
                } while (verifica == false);
        }


        public void ImprimirPassageiro ()
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



        #region CODANTIGO

        //Altera o cadastro do passageiro
        //public void AlteraDadoPassageiro()
        //{
        //    string cpf;
        //    do
        //    {
        //        string caminho = this.caminho;
        //        Console.WriteLine(">>> ALTERAR DADOS DE PASSAGEIRO <<<\nPara sair digite 's'.\n");
        //        Console.Write("Digite o CPF do passageiro: ");
        //        cpf = Console.ReadLine().Replace(".", "").Replace("-", "");
        //        if (cpf == "s")
        //            return;

        //        if (!ValidaCPF(cpf))
        //        {
        //            Console.WriteLine("CPF inválido!");
        //            Thread.Sleep(3000);
        //        }
        //    } while (!ValidaCPF(cpf));

        //    if (!VerificaPassageiro(caminho, cpf))
        //    {
        //        Console.WriteLine("Passageiro não encontrado!!");
        //        Thread.Sleep(3000);
        //        return;
        //    }
        //    //Busca os dados de passageiros no arquivo
        //    string[] lines = File.ReadAllLines(caminho);

        //    for (int i = 0; i < lines.Length; i++)
        //    {
        //        if (lines[i].Contains(cpf))
        //        {
        //            string num;
        //            do
        //            {
        //                Console.Clear();
        //                Console.WriteLine(">>> ALTERAR DADOS DE PASSAGEIRO <<<");
        //                Console.Write("Para alterar digite:\n\n[1] Nome\n[2] Sexo\n[3] Situação do Cadastro\n[0] Sair\nOpção: ");
        //                num = Console.ReadLine();

        //                if (num != "1" && num != "2" && num != "3" && num != "0")
        //                {
        //                    Console.WriteLine("Opção inválida!");
        //                    Thread.Sleep(3000);
        //                }

        //            } while (num != "1" && num != "2" && num != "3" && num != "0");

        //            if (num.Contains("0"))
        //                return;

        //            //Condição para alterar o dado em específico do passageiro
        //            switch (num)
        //            {
        //                case "1":
        //                    if (!CadastraNome())
        //                        return;

        //                    lines[i] = lines[i].Replace(lines[i].Substring(11, Nome.Length), Nome);
        //                    break;

        //                case "2":
        //                    if (!CadastraSexo())
        //                        return;
        //                    lines[i] = lines[i].Replace(lines[i].Substring(70, Sexo.Length), Sexo);
        //                    break;

        //                case "3":
        //                    if (!AlteraSituacao())
        //                        return;
        //                    lines[i] = lines[i].Replace(lines[i].Substring(87, Situacao.Length), Situacao);
        //                    break;
        //            }
        //            Console.WriteLine("Cadastro alterado com sucesso!");
        //            Thread.Sleep(3000);
        //        }
        //    }
        //    //Salva os dados atualizados do passageiro
        //    File.WriteAllLines(caminho, lines);
        //}
        ////Imprimi os passageiros cadastrados e ativos
        //public void ImprimiPassageiros()
        //{
        //    string[] lines = File.ReadAllLines(caminho);
        //    List<string> passageiros = new();

        //    for (int i = 1; i < lines.Length; i++)
        //    {
        //        //Verifica se o cadastro esta ativo
        //        if (lines[i].Substring(87, 1).Contains("A"))
        //            passageiros.Add(lines[i]);
        //    }

        //    //Laço para navegar nos cadastros de passageitos
        //    for (int i = 0; i < passageiros.Count; i++)
        //    {
        //        string op;
        //        do
        //        {
        //            Console.Clear();
        //            Console.WriteLine(">>> Cadastro Passageiros <<<\nDigite para navegar:\n[1] Próximo Cadasatro\n[2] Cadastro Anterior" +
        //                "\n[3] Último cadastro\n[4] Voltar ao Início\n[s] Sair\n");

        //            Console.WriteLine($"Cadastro [{i + 1}] de [{passageiros.Count}]");
        //            //Imprimi o primeiro da lista 
        //            LocalizaPassageiro(caminho, passageiros[i].Substring(0, 11));

        //            Console.Write("Opção: ");
        //            op = Console.ReadLine();

        //            if (op != "1" && op != "2" && op != "3" && op != "4" && op != "s")
        //            {
        //                Console.WriteLine("Opção inválida!");
        //                Thread.Sleep(2000);
        //            }
        //            //Sai do método
        //            else if (op.Contains("s"))
        //                return;

        //            //Volta no Cadastro Anterior
        //            else if (op.Contains("2"))
        //                if (i == 0)
        //                    i = 0;
        //                else
        //                    i--;

        //            //Vai para o fim da lista
        //            else if (op.Contains("3"))
        //                i = passageiros.Count - 1;

        //            //Volta para o inicio da lista
        //            else if (op.Contains("4"))
        //                i = 0;
        //            //Vai para o próximo da lista    
        //        } while (op != "1");
        //        if (i == passageiros.Count - 1)
        //            i--;

        //    }
        //}

        ////Verifica se o Cnpj já esta cadastrado
        //public bool VerificaPassageiro(string caminho, string cpf)
        //{
        //    foreach (string line in File.ReadLines(caminho))
        //    {
        //        if (line.Contains(cpf))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        ////localiza um passageiro em específico
        //public void LocalizaPassageiro(string caminho, string cpf)
        //{
        //    foreach (string line in File.ReadLines(caminho))
        //    {
        //        if (line.Contains(cpf))
        //        {
        //            Console.WriteLine($"CPF: {line.Substring(0, 11)}");
        //            Console.WriteLine($"Nome: {line.Substring(11, 50)}");
        //            Console.WriteLine($"Data de Nascimento: {line.Substring(62, 2)}/{line.Substring(64, 2)}/{line.Substring(66, 4)}");
        //            Console.WriteLine($"Sexo: {line.Substring(70, 1).ToUpper()}");
        //            Console.WriteLine($"Ùltima compra: {line.Substring(71, 2)}/{line.Substring(73, 2)}/{line.Substring(75, 4)}");
        //            Console.WriteLine($"Data do Cadastro: {line.Substring(79, 2)}/{line.Substring(81, 2)}/{line.Substring(83, 4)}");
        //            if (line.Substring(87, 1).Contains("A"))
        //                Console.WriteLine($"Situação: Ativo");
        //            else
        //                Console.WriteLine($"Situação: Desativado");

        //        }
        //    }
        //}
        //Valida o CPF
        #endregion

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