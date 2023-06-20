using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Services;

namespace ControleContainer
{
    class Program
    {
        static Container container = new Container(2.48, 10, 3.28, 1);  // Cria um objeto container com os parâmetros propostos
        
        

        static List<Caixa> caixas = new List<Caixa>()                   // Cria uma lista de todas 5 caixas com os parâmetros propostos

        {
            new Caixa( 0.253, 0.608, 0.518, 210),
            new Caixa( 0.263, 0.480, 0.323, 200),
            new Caixa( 0.203, 0.403, 0.413, 200),
            new Caixa( 0.170, 0.530, 0.380, 200),
            new Caixa( 0.285, 0.435, 0.255, 140)
        };

        
        static void Main(string[] args)
        {
            SolverContext context = SolverContext.GetContext();       //Biblioteca para implementar o método simplex
            Model model = context.CreateModel();
            
            StringBuilder sb = new StringBuilder();                    
            List<Decision> quantidadeCaixas = new List<Decision>();         //Variáveis de decisão (foi definido a quantidade das caixas)
            

            for (int i=0; i < caixas.Count; i++ )
            {
                caixas[i].comprimentoMaximo = caixas[i].caixaQuantidadeMaxComprimento(container.comprimento);
                caixas[i].larguraMaxima = caixas[i].caixaQuantidadeMaxLargura(container.largura);

                quantidadeCaixas.Add(new Decision(Domain.RealNonnegative, $"caixa{(i+1)}"));  // implementa a variável de decisão com seu respectivo nome
                model.AddDecision(quantidadeCaixas[i]);

                sb.AppendLine( $"Valor máximo de caixa {i + 1} no container: {caixas[i].comprimentoMaximo * caixas[i].larguraMaxima}" ); // registra o valor máximo de caixas de cada tipo
            }

            
            // Para o método simplex, registra as restrições de não-negatividade
            model.AddConstraints("Quantidade", 0 <= quantidadeCaixas[0] <= caixas[0].quantidade,
                                               0 <= quantidadeCaixas[1] <= caixas[1].quantidade,
                                               0 <= quantidadeCaixas[2] <= caixas[2].quantidade,
                                               0 <= quantidadeCaixas[3] <= caixas[3].quantidade,
                                               0 <= quantidadeCaixas[4] <= caixas[4].quantidade);
          
            //   Restrições do problema (aqui foi definido que a quantidade de uma caixa * sua área <= área do container E o somatório de todas quantidades*área de cada caixa deve ser <= área do container
            model.AddConstraints("Tamanho", 0 <= quantidadeCaixas[0] * (caixas[0].comprimento * caixas[0].largura) <= container.comprimento * container.largura,
                                            0 <= quantidadeCaixas[1] * (caixas[1].comprimento * caixas[1].largura) <= container.comprimento * container.largura,
                                            0 <= quantidadeCaixas[2] * (caixas[2].comprimento * caixas[2].largura) <= container.comprimento * container.largura,
                                            0 <= quantidadeCaixas[3] * (caixas[3].comprimento * caixas[3].largura) <= container.comprimento * container.largura,
                                            0 <= quantidadeCaixas[4] * (caixas[4].comprimento * caixas[4].largura) <= container.comprimento * container.largura,
                                            
                                            0 <= quantidadeCaixas[0] * (caixas[0].comprimento * caixas[0].largura) +
                                            quantidadeCaixas[1] * (caixas[1].comprimento * caixas[1].largura) +
                                            quantidadeCaixas[2] * (caixas[2].comprimento * caixas[2].largura) +
                                            quantidadeCaixas[3] * (caixas[3].comprimento * caixas[3].largura) +
                                            quantidadeCaixas[4] * (caixas[4].comprimento * caixas[4].largura) <= container.comprimento * container.largura );

            // Aqui foi definido que o objetivo do modelo é Maximizar a quantidade de caixas
            model.AddGoal("QuantidadeCaixas", GoalKind.Maximize, quantidadeCaixas[0] + quantidadeCaixas[1] + quantidadeCaixas[2] + quantidadeCaixas[3] + quantidadeCaixas[4]);
            Solution solution = context.Solve(new SimplexDirective());
            Report report = solution.GetReport();

            Console.WriteLine("{0}", report);
            sb.AppendLine("---------------------------");
            sb.AppendLine("----------Parte 2----------");
            sb.AppendLine("---------------------------");
            for (int i=0; i< quantidadeCaixas.Count; i++)
            {
                sb.AppendLine($"Número máximo de caixas {(i+1)}: {(int)quantidadeCaixas[i].ToDouble()}");
               
            }

            string relatorio = sb.ToString();
            Console.Write( relatorio );

            using (StreamWriter writer = File.CreateText("relatorio.txt"))
            {
                writer.Write(relatorio);        //Gera o arquivo txt com o nome relatório de todos os dados impressos até então
            }
            
            
            

            Console.ReadLine();
        }
    }
}
