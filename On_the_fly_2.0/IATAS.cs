using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace On_the_fly_2._0
{
    internal class IATAS
    {
        public string iata { get; set; }
        Banco banco = new Banco();
        public IATAS()
        {       }
        public void veriatas()
        {
            Console.WriteLine("IATAS CADASTRADAS");
            string cmdselect=$"SELECT IATA, DESTINO FROM IATA";
            int opc = 6;
            banco.Select(cmdselect, opc);
        }
    }
}
