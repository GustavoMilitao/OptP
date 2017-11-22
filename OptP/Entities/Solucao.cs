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
        public List<Variable> VariaveisResolvidas { get; set; }
        public double ValorFuncaoObjetivoResolvido { get; set; }

        public Solucao(string NomeModelo, List<Variable> variaveisResolvidas, double valorFuncaoObjetivoResolvido)
        {
            VariaveisResolvidas = variaveisResolvidas;
            ValorFuncaoObjetivoResolvido = valorFuncaoObjetivoResolvido;
        }
    }
}
