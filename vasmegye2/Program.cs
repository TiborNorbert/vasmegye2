using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace vasmegye2
{
    class Program
    {
        static List<Adat> szemelyiszamok = new List<Adat>();
        static void Main(string[] args)
        {
            Console.WriteLine("\n2.feladat: adatok beolvasása");
            adatatokbeolvasasa("vas.txt", Encoding.Default);
            Console.WriteLine("\n4. feladat ellenőrzés");
            feladatok4();
            Console.WriteLine($"\n5.feladat: Vas megében a vizsgált évek alatt {szemelyiszamok.Count} csecsemő volt");
            Console.WriteLine($"\n6.feladat: Fiúk száma: {szemelyiszamok.FindAll(a=>a.Szm[0]=='1'|| a.Szm[0]=='3').Count}");
            Console.WriteLine($"\n7.feladat: Vizsgált időszsak: {szemelyiszamok.Min(a=>a.evSzam())}-{szemelyiszamok.Max(a=>a.evSzam())}");
            if (szokoevbenszuletett())
            {
                Console.WriteLine("8.feladat: Szükőnapon született baba.");
            }
            else
            {
                Console.WriteLine("8.feladat: Szökőnapon nem született baba.");
            }
            feladat09();
            Console.WriteLine("\nProgram vége");
            Console.ReadLine();  
        }
        private static void feladat09()
        {
            Console.WriteLine("9.feladati Statisztika");
            var statiszika = szemelyiszamok.GroupBy(a => a.evSzam()).Select(b=>new{ev = b.Key, fo = b.Count()});
            foreach (var item in statiszika)
            {
                Console.WriteLine($"{item.ev}-{item.fo}fő");
            }
        }
        private static bool szokoevbenszuletett()
        {
            var szokoevi = szemelyiszamok.Find(a => a.evSzam() % 4 == 0 && a.Szm.Substring(4, 4).Equals("0224"));
            return szokoevi != null;
        }
        public static void feladatok4()
        {
            List<Adat> hibas = szemelyiszamok.FindAll(a=>!CdvEll(a.Szm));
            foreach(Adat item in hibas)
            {
                Console.WriteLine($"Hibás a {item.Szm} személyi azonositó");
            }
        }
        public static bool CdvEll(string szm)
        {
            //-3 feladat
            string szamnumerikus = new string(szm.Where(a => char.IsDigit(a)).ToArray());
            if (szamnumerikus.Length != 11)
            {
                return false;
            }
            double szum =0;
            for (int i = 0; i < szamnumerikus.Length-1; i++)
            {
                szum += char.GetNumericValue(szamnumerikus[i])*(10-i);
            }
            return char.GetNumericValue(szamnumerikus[10]) == szum % 11;
        }
        private static void adatatokbeolvasasa(string adatfile, Encoding @Default)
        {
            if (!File.Exists(adatfile))
            {
                Console.WriteLine("Foráss adatok nem elérhetőek");
                Console.ReadLine();
                Environment.Exit(0);
            }
            using (StreamReader sr=new StreamReader(adatfile))
            {
                while (!sr.EndOfStream)
                {
                    szemelyiszamok.Add(new Adat(sr.ReadLine()));
                }
            }

        }
    }
}

