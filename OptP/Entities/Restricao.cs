using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Restricao
    {
        public String NomeRestricao { get; set; }
        public Dictionary<string, double> Variaveis { get; set; }
        public Comparador Comparador { get; set; }
        public double Comparado { get; set; }
    }
}
