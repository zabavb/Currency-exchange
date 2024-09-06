using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Eighteenth_lesson_csh
{
    [Serializable]
    public class CurrencyExchange
    {
        public List<Currency> Currencys { get; set; }
        public CurrencyExchange() { }
        public CurrencyExchange(List<Currency> currency)
        {
            Currencys = currency;
        }

        public void ShowCurrencyExchang()
        {
            for (int i = 0;i < Currencys.Count; i++)
                Console.WriteLine(Currencys[i]);
        }
    }
}
