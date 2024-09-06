using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Eighteenth_lesson_csh
{
    [Serializable]
    public class Wallet
    {
        public List<CurrencyWallet> Currencies { get; set; }

        public Wallet() { }
        public Wallet(List<CurrencyWallet> currencies)
        {
            Currencies = currencies;
        }
    }
}
