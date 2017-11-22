using Entities;
using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Resolvedor
    {
        public Solucao ResolverProblemaMatematico(ModeloMatematico modelo)
        {
            // Instanciando um  Solver 
            // (Classe da framework do google que obtém os dados e resolve o problema)
            // Construtor recebe o nome do problema e o tipo de problema 
            // (nesse caso programação linear)
            Solver solver = new Solver(modelo.NomeModeloMatematico,
                                       Solver.GLOP_LINEAR_PROGRAMMING);
            // Referência para as variáveis inseridas no Solver.
            List<Variable> variaveis = new List<Variable>();
            // Declara no solver a função objetivo de acordo com as variáveis.
            // Define se é maximização ou minimização no solver.
            resolverFuncaoObjetivo(modelo, solver, variaveis);
            resolverRestricoes(modelo.Restricoes,
                               solver,
                               variaveis);
            // Resolver problema Linear
            solver.Solve();
            // Retorna solução no esquema de classes do projeto.
            return new Solucao(modelo.NomeModeloMatematico, variaveis, solver.Objective().Value());
        }

        private void resolverFuncaoObjetivo(ModeloMatematico modelo,
                                            Solver solver,
                                            List<Variable> variaveisInseridas)
        {
            Objective objective = solver.Objective();
            // Instancia as variáveis do modelo e 
            //define os seus valores na função objetivo (Objective)     
            foreach (KeyValuePair<string, double> v in modelo.Variaveis)
            {
                // Definindo que existe uma variável com o nome de x (v.Key)
                // e seu valor pode variar de infinito negativo à infinito positivo
                var variavel = solver.MakeNumVar(double.NegativeInfinity, double.PositiveInfinity, v.Key);
                // Pegando referência para variável inserida no Solver.
                variaveisInseridas.Add(variavel);
                // Definindo na função objetivo o coeficiente da variável declarada
                // Ex: 5 (coeficiente) * cervejaPilsen (variável)
                objective.SetCoefficient(variavel, v.Value);
            }
            // Definindo se a função é maximização ou minimização de acordo
            // com o enum definido no modelo
            if(modelo.Direcao == DirecaoOptimizacao.MAXIMIZACAO)
            {
                objective.SetMaximization();
            }
            else
            {
                objective.SetMinimization();
            }
        }

        private void resolverRestricoes(List<Restricao> restricoes,
                                                    Solver solver,
                                                    List<Variable> variaveisModelo)
        {
            // Definir coeficientes de restrições e expressão de restrição
            // Exemplo : cervejaPilsen é uma variável da função objetivo.
            //           cervejaTrigo é um variável da função objetivo.
            // Na função objetivo cervejaPilsen vale 3 e cerveja trigo 2.
            // Ex:
            // Na restrição ->
            // 2 *cervejaPilsen + 4*cervejaTrigo <= 50                  
            //    coeficientes de restrição     | expressão de restrição
            //
            List<Constraint> retorno = new List<Constraint>();
            Constraint c = null;
            foreach (Restricao r in restricoes)
            {
                // Define o lado direito da expressão de restrição (<= Comparado)
                // Ex : 2x + 5y <= 50
                //    |esquerdo|DIREITO|
                c = GetExpressaoDeRestricao(solver, r);
                // Define o lado esquerdo da expressão de restrição (2x + 5y)
                // Ex : 2x + 5y <= 50
                //    |ESQUERDO|direito|
                SetCoeficientesRestricao(c, r.Variaveis, variaveisModelo);
            }
        }

        private Constraint GetExpressaoDeRestricao(Solver solver, Restricao r)
        {
            Constraint c = null;
            var digits = Math.Ceiling(Math.Log10(r.Expressao));
            switch (r.Operador)
            {
                case Operador.MENOR_IGUAL:
                    // Ex: 2x + 3y <= 10
                    c = solver.MakeConstraint(double.NegativeInfinity, r.Expressao, r.NomeRestricao);
                    break;
                case Operador.MAIOR_IGUAL:
                    // Ex: 2x + 3y >= 2
                    c = solver.MakeConstraint(r.Expressao, double.PositiveInfinity, r.NomeRestricao);
                    break;
                case Operador.MENOR:
                    // Ex: 2x < 5
                    c = solver.MakeConstraint(double.NegativeInfinity, r.Expressao - Math.Pow(1, -(15 - digits)), r.NomeRestricao);
                    break;
                case Operador.MAIOR:
                    // Ex: 4x > 0
                    c = solver.MakeConstraint(r.Expressao + Math.Pow(1, -(15 - digits)), double.PositiveInfinity, r.NomeRestricao);
                    break;
            }

            return c;
        }

        private void SetCoeficientesRestricao(Constraint c,
                                             Dictionary<string, double> variaveisRestricao,
                                             List<Variable> variaveisModelo)
        {
            foreach(KeyValuePair<string, double> variavel in variaveisRestricao)
            {
                // Variável declarada no modelo que tem o nome igual à restrição da iteração atual
                // Ex => Variáveis Modelo { x, y, z } (objeto Variable da Framework)
                //       Variáveis Restrição { x : 5, y : 10 } (Dictionary<string,double>)
                //       Variavel (iteração atual) { x : 5 }
                //       Procura no Variáveis Modelo Restrição a variável com o nome = x (iteração atual).
                var objetoVariavelDeclarada = variaveisModelo.Where(
                    v => v.Name() == variavel.Key).FirstOrDefault();
                // Adiciona ao objeto Constraint (Restrição) da Framework o valor da restrição.
                // Passando o objeto encontrado com o valor da variável.
                // Ex => Encontrado -> { x } (Variaveis modelo)
                //       Iteração atual -> { x : 5 }
                //       coeficiente de x para esta restrição é 5.
                c.SetCoefficient(objetoVariavelDeclarada, variavel.Value);
            }
        }
    }
}
