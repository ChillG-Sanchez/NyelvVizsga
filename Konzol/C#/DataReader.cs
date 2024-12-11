using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class DataReader
{
    public static Dictionary<string, List<int>> BeolvasFajl(string fileName)
    {
        var result = new Dictionary<string, List<int>>();

        using (var reader = new StreamReader(fileName, Encoding.UTF8))
        {
            string? line;
            bool isHeader = true;

            while ((line = reader.ReadLine()) != null)
            {
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                var parts = line.Split(';');
                var nyelv = parts[0];
                var vizsgak = parts.Skip(1).Select(int.Parse).ToList();
                result[nyelv] = vizsgak;
            }
        }

        return result;
    }
}