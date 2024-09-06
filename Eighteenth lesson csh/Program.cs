using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Eighteenth_lesson_csh
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CurrencyExchange currencyExchange = new CurrencyExchange(new List<Currency>
            {
                new Currency("UAH", 1, 1),
                new Currency("USD", 36.569, 37.453),
                new Currency("EUR", 38.530, 40.000)
            });
            Wallet wallet = new Wallet(new List<CurrencyWallet>
            {
                new CurrencyWallet("UAH", 24000.000)
            });

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;
            
            string pathExchange = Path.Combine(projectDirectory, "CurrencyExchange.xml");
            XmlSerializer serializerExchange = new XmlSerializer(typeof(CurrencyExchange), new XmlRootAttribute("Currency Exchange"));
            string pathWallet = Path.Combine(projectDirectory, "Wallet.xml");
            XmlSerializer serializerWallet = new XmlSerializer(typeof(Wallet), new XmlRootAttribute("Wallet"));
            
            using (FileStream FS = new FileStream(pathExchange, FileMode.Open))
            {
                CurrencyExchange tmp = (CurrencyExchange)serializerExchange.Deserialize(FS);
            }
            using (FileStream FS = new FileStream(pathWallet, FileMode.Open))
            {
                Wallet tmp = (Wallet)serializerWallet.Deserialize(FS);
            }

            while (true)
            {
                Console.Write("\n0 - Exit\n1 - Show currency exchange\n2 - Buy currency\n3 - Sell currency\n4 - Add new currency" +
                "\n5 - Show my wallet\nEnter your choice: "); int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 0:
                        using (FileStream FS = new FileStream(pathExchange, FileMode.Truncate))
                        {
                            serializerExchange.Serialize(FS, currencyExchange);
                        }
                        using (FileStream FS = new FileStream(pathWallet, FileMode.Truncate))
                        {
                            serializerWallet.Serialize(FS, wallet);
                        }

                        Console.WriteLine("\n\tEXIT!");
                        return;
                    case 1:
                        Console.WriteLine();
                        currencyExchange.ShowCurrencyExchang();
                        break;
                    case 2:
                        Console.Write("\nEnter which currency do you want to buy: ");
                        string choiceBuy = Console.ReadLine();
                        bool isNotFoundBuy1 = true;
                        bool isNotFoundBuy2 = true;
                        try
                        {
                            for (int i = 0; i < currencyExchange.Currencys.Count; i++)
                            {

                                if (choiceBuy == currencyExchange.Currencys[i].Name)
                                {
                                    isNotFoundBuy1 = false;
                                    for (int j = 0; j < wallet.Currencies.Count; j++)
                                    {
                                        if (choiceBuy == wallet.Currencies[j].Name)
                                        {
                                            isNotFoundBuy2 = false;
                                            Console.Write("Enter the amount that you want to receive: ");
                                            double sum1 = Convert.ToDouble(Console.ReadLine());
                                            double buy1 = currencyExchange.Currencys[i].Buy(sum1);
                                            if (buy1 > wallet.Currencies[0].Amount)
                                                throw new MyException("\tNot enough money!");
                                            else
                                            {
                                                wallet.Currencies[0].Amount -= buy1;
                                                wallet.Currencies[j].Amount += sum1;
                                            }
                                            break;
                                        }
                                    }
                                    if (isNotFoundBuy2)
                                    {
                                        Console.Write("\nEnter the amount that you want to receive: ");
                                        double sum2 = Convert.ToDouble(Console.ReadLine());
                                        double buy2 = currencyExchange.Currencys[i].Buy(sum2);
                                        if (buy2 > wallet.Currencies[0].Amount)
                                            throw new MyException("\tNot enough money!");
                                        else
                                        {
                                            wallet.Currencies[0].Amount -= buy2;
                                            wallet.Currencies.Add(new CurrencyWallet(choiceBuy, sum2));
                                        }
                                        break;
                                    }
                                }
                            }
                            if (isNotFoundBuy1)
                                throw new MyException("\tIs NOT found in currency exchange!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 3:
                        Console.Write("\nEnter which currency do you want to sell: ");
                        string choiceSell = Console.ReadLine();
                        bool isNotFoundSell1 = true;
                        bool isNotFoundSell2 = true;
                        try
                        {
                            for (int i = 0; i < currencyExchange.Currencys.Count; i++)
                            {

                                if (choiceSell == currencyExchange.Currencys[i].Name)
                                {
                                    isNotFoundSell1 = false;
                                    for (int j = 0; j < wallet.Currencies.Count; j++)
                                    {
                                        if (choiceSell == wallet.Currencies[j].Name)
                                        {
                                            isNotFoundSell2 = false;
                                            Console.Write("Enter the amount that you want to receive: ");
                                            double sum = Convert.ToDouble(Console.ReadLine());
                                            if (sum > wallet.Currencies[0].Amount)
                                                throw new MyException("\tNot enough money!");
                                            else
                                            {
                                                wallet.Currencies[j].Amount -= currencyExchange.Currencys[j].Sell(sum);
                                                wallet.Currencies[0].Amount += sum;
                                            }
                                            break;
                                        }
                                    }
                                    if (isNotFoundSell2)
                                        throw new MyException("\tIs NOT found in wallet!");
                                }
                            }
                            if (isNotFoundSell1)
                                throw new MyException("\tIs NOT found in currency exchange!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 4:
                        Console.Write("\nEnter name of currency: ");
                        string name = Console.ReadLine();
                        bool isNotFound = true;
                        try
                        {
                            for (int i = 0; i < currencyExchange.Currencys.Count; i++)
                            {
                                if (name == currencyExchange.Currencys[i].Name)
                                {
                                    isNotFound = false;
                                    throw new MyException("\tThis currency is already exists!");
                                }
                            }
                            if (isNotFound)
                            {
                                Console.Write("Enter the buy rate of currency: ");
                                double buyRate = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Enter the sell rate of currency: ");
                                double sellRate = Convert.ToDouble(Console.ReadLine());
                                if (buyRate <= 1 || sellRate <= 1)
                                    throw new MyException("\tThis currency can't exists!");
                                currencyExchange.Currencys.Add(new Currency(name, buyRate, sellRate));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 5:
                        Console.WriteLine("\n\tWallet");
                        for (int i = 0; i < wallet.Currencies.Count; i++)
                            Console.WriteLine(wallet.Currencies[i]);
                        break;
                    case 6:
                        
                        break;
                }
            }
        }
    }
}