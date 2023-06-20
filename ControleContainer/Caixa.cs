using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleContainer
{
    public class Caixa
    {
        //Atributos
        public double largura { get; set; }
        public double comprimento { get; set; }
        public double altura { get; set; }
        public int quantidade { get; set; }
        public int comprimentoMaximo { get; set; }
        public int larguraMaxima { get; set; }
        
        //Métodos
            //Construtor
        public Caixa(double largura, double comprimento, double altura, int quantidade)
        {
            this.largura = largura;
            this.comprimento = comprimento;
            this.altura = altura;
            this.quantidade = quantidade;
        }

            //Outros Métodos

        public int caixaQuantidadeMaxLargura(double larguraContainer)
        {
            return Convert.ToInt32(larguraContainer/largura);
        }

        public int caixaQuantidadeMaxComprimento(double comprimentoContainer)
        {
            return Convert.ToInt32(comprimentoContainer / comprimento);
        }

        public List<int> conjuntoLargura(int quantidadeMaximaLargura)
        {
            List<int> conjunto = new List<int>();

            for(int i=0; i <= quantidadeMaximaLargura; i++)
            {
                conjunto.Add(i);
            }

            return conjunto;
        }

        public List<int> conjuntoComprimento(int quantidadeMaximaComprimento)
        {
            List<int> conjunto = new List<int>();

            for (int i = 0; i <= quantidadeMaximaComprimento; i++)
            {
                conjunto.Add(i);
            }

            return conjunto;
        }
    }
}
