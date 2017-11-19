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
        public List<Variable> VariaveisResolvidas { get; set; }
        public double ValorFuncaoObjetivoResolvido { get; set; }

        public Solucao(List<Variable> variaveisResolvidas, double valorFuncaoObjetivoResolvido)
        {
            VariaveisResolvidas = variaveisResolvidas;
            ValorFuncaoObjetivoResolvido = valorFuncaoObjetivoResolvido;
        }
    }
}
