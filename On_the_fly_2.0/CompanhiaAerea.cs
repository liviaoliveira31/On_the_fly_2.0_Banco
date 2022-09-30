using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace On_the_fly_2._0
{
    internal class CompanhiaAerea
    {

        private string Cnpj { get; set; }
        private string RazaoSocial { get; set; }
        private DateTime DataAbertura { get; set; }
        private DateTime DataCadastro { get; set; }
        private DateTime UltimoVoo { get; set; }
        private string Situacao { get; set; }

        Banco banco = new Banco();
        public override string ToString()
        {
            return $"{Cnpj}{RazaoSocial}{DataAbertura}{DataCadastro}{UltimoVoo}{Situacao}";
        }

        public void Pausa()
        {
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }

        #region verificações de dados e cadastro de companhia
        public bool CadCNPJ()
        {
            do
            {
                Console.Write("Digite o CNPJ: ");
                Cnpj = Console.ReadLine().Replace(".", "").Replace("-", "").Replace("/", "");
                if (Cnpj == "0")
                    return false; ;
                if (!ValidaCNPJ(Cnpj))
                {
                    Console.WriteLine("Digite um CNPJ Válido!");
                    Thread.Sleep(2000);
                }

                else
                {
                    if (banco.VerificarDadoExistente(Cnpj, "CNPJ", "Companhia_Aerea"))
                    {
                        Console.WriteLine("cnpj ja cadastrado!");
                        //Thread.Sleep(2000);
                        Cnpj = "";
                    }
                }
            } while (!ValidaCNPJ(Cnpj));
            
            return true;
        }
     
        public bool CadDataAbertura()
        {
            Console.Write("Digite a data de abertura (Mês/Dia/Ano): ");
            DateTime dataAbertura;
            while (!DateTime.TryParse(Console.ReadLine(), out dataAbertura))
            {
                Console.WriteLine("Formato de data incorreto!");
                Console.Write("Digite a data de abertura (Mês/Dia/Ano): ");
            }
            DateTime verData = dataAbertura;
            if (verData > DateTime.Now.AddMonths(-6))
            {
                Console.WriteLine("Não é possível cadastrar empresas com menos de 6 meses!!!");
                Thread.Sleep(2000);
                return false;
            }
            DataAbertura = dataAbertura;
           
            return true;
        }
       
        public bool CadRazao()
        {
            Console.Write("Digite a Razão Social:  (Max 50 caracteres): ");
            RazaoSocial = Console.ReadLine();
            if (RazaoSocial == "0")
                return false;
            if (RazaoSocial.Length > 50)
            {
                Console.WriteLine("Infome uma Razão Social menor que 50 caracteres!!!!");
                Thread.Sleep(2000);
            }
            while (RazaoSocial.Length > 50) ;

            for (int i = RazaoSocial.Length; i <= 50; i++)
                RazaoSocial += " ";
            return true;
        }
 
        public void CadCompanhia()
        {
            Console.WriteLine(">>> CADSTRO DE COMPANHIA AEREA <<<");
            Console.WriteLine("Para cancelar o cadastro digite 0:\n");

            if (!CadCNPJ())
                return;

            if (!CadDataAbertura())
                return;

            if (!CadRazao())
                return;

            UltimoVoo = DateTime.Now;

            DataCadastro = DateTime.Now;

            Situacao = "A";

            //Insere no arquivo a nova Companhia
            string cmdinsert = $"INSERT INTO Companhia_Aerea (CNPJ, Razao_Social, Data_Abertura, Data_Cadastro, Ultimo_Voo, Situacao)  VALUES('{Cnpj}','{RazaoSocial}','{DataAbertura}','{DataCadastro}','{UltimoVoo}','{Situacao}');";
            banco.Insert(cmdinsert);

            Pausa();
        }
        #endregion

        #region alteração de dados da companhia
        public void AlterarCadastroCompanhia()
        {
            bool verifica;
            Console.WriteLine("ALTERAÇÃO DE DADOS");
            Console.WriteLine("Insira o CNPJ da companhia que deseja alterar");
            string ncnpj = Console.ReadLine().Replace(".", "").Replace("-", "").Replace("/", "");
            int opc = 2;
            string cmdselect = $"SELECT CNPJ, Razao_Social, Data_Abertura, Data_Cadastro, Ultimo_Voo, Situacao FROM Companhia_Aerea WHERE CNPJ = '{ncnpj}'";

            verifica = banco.Select(cmdselect, opc);

            if (verifica)
            {


                Console.WriteLine("Que dado deseja alterar?");
                Console.WriteLine("1- Razao social");
                Console.WriteLine("2- Situação");
               
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
                        Console.WriteLine("Insira a nova razão social [50 caracteres]");
                        string novarazao = Console.ReadLine();
                        if(novarazao.Length>50)
                        {
                            Console.WriteLine("Insira a nova razão social[50 caracteres]");
                            novarazao = Console.ReadLine();
                        }
                        string cmdupdate = $"UPDATE Companhia_Aerea SET Razao_social = '{novarazao}' where CNPJ = '{ncnpj}'";
                        banco.Update(cmdupdate);

                        cmdselect = $"SELECT CNPJ, Razao_Social, Data_Abertura, Data_Cadastro, Ultimo_Voo, Situacao FROM Companhia_Aerea WHERE CNPJ = '{ncnpj}'";

                        verifica = banco.Select(cmdselect, opc);
                        Pausa();
                        break;

                    case "2":
                        Console.WriteLine("Insira a nova situação[A/I]");

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

                        cmdupdate = $"UPDATE Companhia_Aerea SET Situacao = '{novasit}' where CNPJ = '{ncnpj}'";
                        banco.Update(cmdupdate);

                        cmdselect = $"SELECT CNPJ, Razao_Social, Data_Abertura, Data_Cadastro, Ultimo_Voo, Situacao FROM Companhia_Aerea WHERE CNPJ = '{ncnpj}'";
                        verifica = banco.Select(cmdselect, opc);
                        Pausa();
                        break;

                }
            }

            else
            {
                Console.WriteLine("COMPANHIA NAO ENCONTRADA");
                Pausa();
            }
        }
        #endregion

        #region impressão e localização da companhia
        public void ImprimirCompanhia()
        {
            int opc = 2;
            Console.WriteLine("Lista de Companhias Cadastradas");

            string cmdselect = $"SELECT CPF, NOME, SEXO, DATA_CADASTRO, DATA_NASCIMENTO, ULTIMA_COMPRA, SITUACAO FROM PASSAGEIRO";
            banco.Select(cmdselect, opc);
            Pausa();
        }
        public void LocalizarCompanhia()
        {
            int opc = 2;
            Console.WriteLine("LOCALIZAÇÃO DE COMPANHIA");
            Console.WriteLine("Insira o CNPJ da companhia que deseja encontrar");
            string ncnpj = Console.ReadLine().Replace(".", "").Replace("-", "").Replace("/", "");
          
            string cmdselect = $"SELECT CNPJ, Razao_Social, Data_Abertura, Data_Cadastro, Ultimo_Voo, Situacao FROM Companhia_Aerea WHERE CNPJ = '{ncnpj}'";
            banco.Select(cmdselect, opc);
            Pausa();
        }
        #endregion

        #region validacnpj
        public bool ValidaCNPJ(string vrCNPJ)

        {
            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;

            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));

                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));

                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (
                         resultado[nrDig] == 1))

                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);

                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));

                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }
        #endregion

    }
}
