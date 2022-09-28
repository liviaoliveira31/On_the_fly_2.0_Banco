using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace On_the_fly_2._0
{
    internal class IATA
    {
        public string iata { get; set; }
        Banco banco = new Banco();

        public IATA()
        {

        }

        public void inserirIata()
        {
            string cmdinsert = $"INSERT INTO IATA (IATA, DESTINO) VALUES('CGH','CONGONHAS'), ('BSB', 'BRASILIA'), ('GIG','GALEAO'), ('SSA','SALVADOR'), ('FLN','FLORIANOPOLIS'), ('POA','PORTO ALEGRE'), ('VCP','CAMPINAS'), ('GRU','GUARULHOS'), ('REC','RECIFE'), ('CWB','CURITIBA'), ('BEL','BELO HORIZONTE');";
            banco.Insert(cmdinsert);
            Console.WriteLine("Inseridas com sucesso");

        }

        public void veriatas()
        {
            Console.WriteLine("IATAS CADASTRADAS");
            string cmdselect=$"SELECT IATA, DESTINO FROM IATA";
            int opc = 6;
            banco.Select(cmdselect, opc);
        }
    }
}
