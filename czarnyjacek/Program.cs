using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    public enum Figura
    {
        Dwa = 2, Trzy, Cztery, Pięć, Sześć, Siedem, Osiem, Dziewięć, Dziesięć,
        Walet = 10, Dama = 10, Król = 10, As = 11
    }

    public enum Kolor
    {
        Pik, Kier, Karo, Trefl
    }

    class Program
    {
        static List<(Figura, Kolor)> Talia = new();
        static Random random = new();

        static void Main(string[] args)
        {
            InicjalizujTalie();
            List<(Figura, Kolor)> Gracz = new();
            List<(Figura, Kolor)> Krupier = new();

            Gracz.Add(Dobierz());
            Gracz.Add(Dobierz());
            Krupier.Add(Dobierz());
            Krupier.Add(Dobierz());

            while (true)
            {
                Console.Clear();
                WypiszReke("Twoje karty", Gracz);
                Console.WriteLine($"Wartość: {ObliczWartosc(Gracz)}");
                if (ObliczWartosc(Gracz) > 21)
                {
                    Console.WriteLine("Przegrałeś! Przekroczyłeś 21.");
                    return;
                }

                Console.WriteLine("Dobierasz (d) czy pasujesz (p)?");
                string decyzja = Console.ReadLine();
                if (decyzja.ToLower() == "d")
                {
                    Gracz.Add(Dobierz());
                }
                else if (decyzja.ToLower() == "p")
                {
                    break;
                }
            }

            while (ObliczWartosc(Krupier) < 17)
            {
                Krupier.Add(Dobierz());
            }
           
            Console.Clear();
            WypiszReke("Twoje karty", Gracz);
            Console.WriteLine($"Wartość: {ObliczWartosc(Gracz)}");
            WypiszReke("Karty krupiera", Krupier);
            Console.WriteLine($"Wartość: {ObliczWartosc(Krupier)}");

            if (ObliczWartosc(Gracz) > 21)
            {
                Console.WriteLine("Przegrałeś! Przekroczyłeś 21.");
            }
            else if (ObliczWartosc(Krupier) > 21 || ObliczWartosc(Gracz) > ObliczWartosc(Krupier))
            {
                Console.WriteLine("Wygrałeś!");
            }
            else if (ObliczWartosc(Gracz) == ObliczWartosc(Krupier))
            {
                Console.WriteLine("Remis!");
            }
            else
            {
                Console.WriteLine("Przegrałeś!");
            }
        }

        static void InicjalizujTalie()
        {
            foreach (Figura figura in Enum.GetValues(typeof(Figura)))
            {
                foreach (Kolor kolor in Enum.GetValues(typeof(Kolor)))
                {
                    Talia.Add((figura, kolor));
                }
            }
        }

        static (Figura, Kolor) Dobierz()
        {
            int indeks = random.Next(Talia.Count);
            var karta = Talia[indeks];
            Talia.RemoveAt(indeks);
            return karta;
        }

        static int ObliczWartosc(List<(Figura, Kolor)> reka)
        {
            int suma = reka.Sum(karta => (int)karta.Item1);
            int liczbaAsow = reka.Count(karta => karta.Item1 == Figura.As);

            while (suma > 21 && liczbaAsow > 0)
            {
                suma -= 10;
                liczbaAsow--;
            }

            return suma;
        }

        static void WypiszReke(string tytul, List<(Figura, Kolor)> reka)
        {
            Console.WriteLine(tytul);
            foreach (var karta in reka)
            {
                Console.WriteLine($"{karta.Item1} {karta.Item2}");
            }
        }
    }
}
