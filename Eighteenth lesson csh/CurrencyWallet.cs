using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eighteenth_lesson_csh
{
    [Serializable]
    public class CurrencyWallet
    {
        [XmlText]
        public string Name { get; set; }
        [XmlAttribute]
        public double Amount { get; set; }

        public CurrencyWallet() { }
        public CurrencyWallet(string name, double amount)
        {
            Name = name;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{Amount} {Name}";
        }
    }
}