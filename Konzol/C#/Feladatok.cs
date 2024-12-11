using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Feladatok
{
    public static void LegnepszerubbNyelvek(Dictionary<string, List<int>> sikeresVizsgak, Dictionary<string, List<int>> sikertelenVizsgak)
    {
        var osszesVizsga = new Dictionary<string, int>();

        foreach (var nyelv in sikeresVizsgak.Keys)
        {
            int sikeres = sikeresVizsgak[nyelv].Sum();
            int sikertelen = sikertelenVizsgak.ContainsKey(nyelv) ? sikertelenVizsgak[nyelv].Sum() : 0;
            osszesVizsga[nyelv] = sikeres + sikertelen;
        }

        var legnepszerubbNyelvek = osszesVizsga.OrderByDescending(x => x.Value).Take(3);

        foreach (var nyelv in legnepszerubbNyelvek)
        {
            Console.WriteLine(nyelv.Key);
        }
    }

    public static int BekerEv()
    {
        int ev;
        while (true)
        {
            Console.Write("Adjon meg egy évet (2009 és 2017 között): ");
            string? input = Console.ReadLine();

            if (input != null && int.TryParse(input, out ev) && ev >= 2009 && ev <= 2017)
            {
                break;
            }
            else
            {
                Console.WriteLine("Helytelen év. Kérem, adjon meg egy évet 2009 és 2017 között.");
            }
        }
        return ev;
    }

    public static void LegnagyobbSikertelenVizsgakAranya(Dictionary<string, List<int>> sikeresVizsgak, Dictionary<string, List<int>> sikertelenVizsgak, int ev)
    {
        int index = ev - 2009;
        string legnagyobbNyelv = "";
        double legnagyobbArany = 0;

        foreach (var nyelv in sikeresVizsgak.Keys)
        {
            int sikeres = sikeresVizsgak[nyelv][index];
            int sikertelen = sikertelenVizsgak.ContainsKey(nyelv) ? sikertelenVizsgak[nyelv][index] : 0;
            int osszes = sikeres + sikertelen;
            double arany = osszes > 0 ? (double)sikertelen / osszes * 100 : 0;

            if (arany > legnagyobbArany)
            {
                legnagyobbArany = arany;
                legnagyobbNyelv = nyelv;
            }
        }

        Console.WriteLine($"{ev}-ben a legnagyobb sikertelen vizsgák aránya {legnagyobbNyelv} nyelvből volt: {legnagyobbArany:F2}%");
    }

    public static void NemVoltVizsgazo(Dictionary<string, List<int>> sikeresVizsgak, Dictionary<string, List<int>> sikertelenVizsgak, int ev)
    {
        int index = ev - 2009;
        bool voltVizsgazo = false;

        foreach (var nyelv in sikeresVizsgak.Keys)
        {
            int sikeres = sikeresVizsgak[nyelv][index];
            int sikertelen = sikertelenVizsgak.ContainsKey(nyelv) ? sikertelenVizsgak[nyelv][index] : 0;

            if (sikeres == 0 && sikertelen == 0)
            {
                Console.WriteLine(nyelv);
                voltVizsgazo = true;
            }
        }

        if (!voltVizsgazo)
        {
            Console.WriteLine("Minden nyelvből volt vizsgázó.");
        }
    }

    public static void Osszesites(Dictionary<string, List<int>> sikeresVizsgak, Dictionary<string, List<int>> sikertelenVizsgak, string outputFile)
    {
        using (var writer = new StreamWriter(outputFile, false, Encoding.UTF8))
        {
            foreach (var nyelv in sikeresVizsgak.Keys)
            {
                int sikeres = sikeresVizsgak[nyelv].Sum();
                int sikertelen = sikertelenVizsgak.ContainsKey(nyelv) ? sikertelenVizsgak[nyelv].Sum() : 0;
                int osszes = sikeres + sikertelen;
                double arany = osszes > 0 ? (double)sikeres / osszes * 100 : 0;

                writer.WriteLine($"{nyelv};{osszes};{arany:F2}");
            }
        }
    }
}