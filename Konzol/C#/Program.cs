using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        string? basePath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName;

        if (basePath == null)
        {
            Console.WriteLine("A basePath nem található.");
            return;
        }

        string sikeresFile = Path.Combine(basePath, "Forras", "sikeres.csv");
        string sikertelenFile = Path.Combine(basePath, "Forras", "sikertelen.csv");

        if (!File.Exists(sikeresFile) || !File.Exists(sikertelenFile))
        {
            Console.WriteLine("A megadott fájlok nem találhatók.");
            return;
        }

        Console.WriteLine("1. FELADAT: Adatok beolvasása");
        var sikeresVizsgak = DataReader.BeolvasFajl(sikeresFile);
        var sikertelenVizsgak = DataReader.BeolvasFajl(sikertelenFile);
        Console.WriteLine("Adatok beolvasása kész.");

        Console.WriteLine("2. FELADAT: A legnépszerűbb nyelvek meghatározása");
        Feladatok.LegnepszerubbNyelvek(sikeresVizsgak, sikertelenVizsgak);

        Console.WriteLine("3. FELADAT: Kérjen be egy évet");
        int ev = Feladatok.BekerEv();

        Console.WriteLine("4. FELADAT: A legnagyobb sikertelen vizsgák aránya");
        Feladatok.LegnagyobbSikertelenVizsgakAranya(sikeresVizsgak, sikertelenVizsgak, ev);

        Console.WriteLine("5. FELADAT: Nyelvek, amelyekből nem volt vizsgázó");
        Feladatok.NemVoltVizsgazo(sikeresVizsgak, sikertelenVizsgak, ev);

        Console.WriteLine("6. FELADAT: Összesítés készítése");
        Feladatok.Osszesites(sikeresVizsgak, sikertelenVizsgak, "osszesites.csv");
    }
}