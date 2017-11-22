using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Solucao
    {
        public string NomeModelo { get; set; }
        public Dictionary<string, double> VariaveisResolvidas { get; set; }
        public double ValorFuncaoObjetivoResolvido { get; set; }

        public Solucao(string nomeModelo, Dictionary<string, double> variaveisResolvidas, double valorFuncaoObjetivoResolvido)
        {
            NomeModelo = nomeModelo;
            VariaveisResolvidas = variaveisResolvidas;
            ValorFuncaoObjetivoResolvido = valorFuncaoObjetivoResolvido;
        }
    }
}
