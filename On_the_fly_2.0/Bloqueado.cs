﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace On_the_fly_2._0
{
    internal class Bloqueado
    {

        public string Cnpj { get; set; }
        Banco banco = new Banco();
     
        public void GerarMenu()
        {
            int opc = 5;
            bool retorna = true;
            //BloquearCnpj();
            Console.WriteLine(" Digite a opção: \n" +
                "1 - Adicionar CNPJ\n" +
                "2 - Verificar CNPJ\n" +
                "3 - Remover CNPJ\n" +
                "4 - Listar cnpj bloqueados\n" +
                "0 - Sair");
            do
            {
                try
                {
                    opc = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }
                switch (opc)
                {
                    case 0:
                        retorna = false;
                        break;
                    case 1:
                        BloquearCnpj();
                        break;
                    case 2:
                        VerificarCnpj();
                        break;
                    case 3:
                        DesbloquearCnpj();
                        break;
                    case 4:
                        ListarCnpjBloqueado();
                        break;
                   
                    default: break;
                }
            } while (retorna);
        }
        public void VerificarCnpj()
        {
            int opc = 3;
            bool verifica;
            {
                bool validar;
                do
                {
                    Console.WriteLine(" Digite o CNPJ para ser verificado ");
                    string Cnpj = Console.ReadLine().Replace(".", "").Replace("/", "").Replace("-", "");
                    validar = ValidarCNPJ(Cnpj);
                    if (validar)
                    {
                        string query = $"SELECT CNPJ FROM Bloqueado WHERE CNPJ ={Cnpj}";
                        verifica = banco.Select(query, opc);
                        if (verifica)
                        {
                            Console.WriteLine($" Está Bloqueado ");
                        }
                        else
                        {
                            Console.WriteLine($"\n '{Cnpj}' Não está bloqueado");
                        }
                        Console.WriteLine("\n CNPJ foi adicionado a lista de bloqueado");
                        Console.ReadKey();
                    }
                    else if (!validar)
                    {
                        Console.WriteLine(" CNPJ invalido");
                        Console.WriteLine(" \nDigite um CPNJ valido\n");
                    }
                } while (validar == false);
            }
        }
        public void ListarCnpjBloqueado()
        {
            int opc = 4;
            Console.Clear();
            Console.WriteLine(" Lista de Cnpj restingido ");
            string query = $"SELECT CNPJ FROM Bloqueado ";
            banco.Select(query, opc);
        }
        public void BloquearCnpj()
        {
            bool validar;
            do
            {
                Console.WriteLine(" Digite o CNPJ a ser bloqueado");
                string Cnpj = Console.ReadLine().Replace(".", "").Replace("/", "").Replace("-", "");
                validar = ValidarCNPJ(Cnpj);
                if (validar)
                {
                    string query = $"INSERT INTO Bloqueado  (CNPJ) VALUES('{Cnpj}')";
                    banco.Insert(query);
                    Console.WriteLine("\n CNPJ foi adicionado a lista de bloqueado");
                    Console.ReadKey();
                }
                else if (!validar)
                {
                    Console.WriteLine(" CNPJ invalido");
                    Console.WriteLine(" \nDigite um CPNJ valido\n");
                }
            } while (validar == false);
        }
        public void DesbloquearCnpj()
        {
            bool validar;
            do
            {
                Console.WriteLine(" Digite o CNPJ que sera bloqueado");
                string Cnpj = Console.ReadLine().Replace(".", "").Replace("-", "");
                validar = ValidarCNPJ(Cnpj);
                if (validar)
                {
                    string query = $"DELETE FROM BLOQUEADO WHERE CNPJ=('{Cnpj}')";
                    banco.Insert(query);
                    Console.WriteLine($"\n Foi retirado a restrição do '{Cnpj}' \n Pressione ENTER para continuar");
                    Console.ReadKey();
                }
                else if (validar == false)
                {
                    Console.WriteLine(" CNPJ INVALIDO");
                    Console.WriteLine(" Digite um CNPJ valido\n");
                }
            } while (validar == false);
        }
        public override string ToString()
        {
            return $"{Cnpj}";
        }
        public static bool ValidarCNPJ(string vrCNPJ)
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

    }
}
