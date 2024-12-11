using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nyelvvizsga
{
	public class DataProcessor
	{
		private Dictionary<string, List<int>> successfulExams = new Dictionary<string, List<int>>();
		private Dictionary<string, List<int>> failedExams = new Dictionary<string, List<int>>();

		public void LoadData(string successfulFile, string failedFile)
		{
			var successfulLines = File.ReadAllLines(successfulFile);
			var failedLines = File.ReadAllLines(failedFile);

			for (int i = 1; i < successfulLines.Length; i++)
			{
				var successfulParts = successfulLines[i].Split(';');
				var failedParts = failedLines[i].Split(';');

				string language = successfulParts[0];
				var successfulCounts = successfulParts.Skip(1).Select(int.Parse).ToList();
				var failedCounts = failedParts.Skip(1).Select(int.Parse).ToList();

				successfulExams[language] = successfulCounts;
				failedExams[language] = failedCounts;
			}
		}

		public List<string> GetTopLanguages()
		{
			var languageScores = new List<Tuple<string, int>>();

			foreach (var language in successfulExams.Keys)
			{
				int totalExams = successfulExams[language].Sum() + failedExams[language].Sum();
				languageScores.Add(new Tuple<string, int>(language, totalExams));
			}

			languageScores.Sort((x, y) => y.Item2.CompareTo(x.Item2));

			return languageScores.Take(3).Select(x => x.Item1).ToList();
		}

		public string GetHighestFailureRate(int year)
		{
			int yearIndex = year - 2009;
			string highestFailureLanguage = null!;
			double highestFailureRate = 0;

			foreach (var language in successfulExams.Keys)
			{
				int successful = successfulExams[language][yearIndex];
				int failed = failedExams[language][yearIndex];
				int total = successful + failed;

				if (total > 0)
				{
					double failureRate = (double)failed / total;
					if (failureRate > highestFailureRate)
					{
						highestFailureRate = failureRate;
						highestFailureLanguage = language;
					}
				}
			}

			return $"{highestFailureLanguage} - {highestFailureRate * 100:F2}%";
		}

		public List<string> GetNoExamsLanguages(int year)
		{
			int yearIndex = year - 2009;
			var languagesWithNoExams = new List<string>();

			foreach (var language in successfulExams.Keys)
			{
				int successful = successfulExams[language][yearIndex];
				int failed = failedExams[language][yearIndex];

				if (successful == 0 && failed == 0)
				{
					languagesWithNoExams.Add(language);
				}
			}

			return languagesWithNoExams.Count == 0
				? new List<string> { "Minden nyelvből volt vizsgázó." }
				: languagesWithNoExams;
		}

		public void SaveSummary(string fileName)
		{
			using (var writer = new StreamWriter(fileName))
			{
				writer.WriteLine("Nyelv;OsszesVizsga;SikeresArany");

				foreach (var language in successfulExams.Keys)
				{
					int totalExams = successfulExams[language].Sum() + failedExams[language].Sum();
					double successfulRate = (double)successfulExams[language].Sum() / totalExams * 100;

					writer.WriteLine($"{language};{totalExams};{successfulRate:F2}");
				}
			}
		}
	}
}
