using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace view
{
    public class Sinal
    {
        public Sinal(string signal,double lot, string symbol, double preco)
        {
            Lot = lot;
            Symbol = symbol;
            Preco = preco;
            Signal = signal;
            
        }
        public string Signal { get; set; }
        public double Lot { get; set; }
        public string Symbol { get; set; }
        public double Preco { get; set; }
    }
}
