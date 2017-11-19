using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ModeloMatematico
    {
        public String NomeModeloMatematico { get; set; }
        public Dictionary<string, double> Variaveis { get; set; }
        public List<Restricao> Restricoes { get; set; }
        public DirecaoOptimizacao Direcao { get; set; }
    }
}
