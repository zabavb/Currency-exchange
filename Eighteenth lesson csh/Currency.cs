using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Eighteenth_lesson_csh
{
    [Serializable]
    public class Currency
    {
        [XmlText]
        public string Name { get; set; }
        [XmlAttribute]
        public double BuyRate { get; set; }
        [XmlAttribute]
        public double SellRate { get; set; }

        public Currency() { }
        public Currency(string name, double buyRate, double sellRate)
        {
            Name = name;
            BuyRate = buyRate;
            SellRate = sellRate;
        }

        public double Buy(double sum)
        {
            return sum * BuyRate;
        }
        public double Sell(double sum)
        {
            return sum / SellRate;
        }

        public override string ToString()
        {
            return $"\t{Name}\nBuy: {BuyRate} | Sell: {SellRate}";
        }
    }
}
