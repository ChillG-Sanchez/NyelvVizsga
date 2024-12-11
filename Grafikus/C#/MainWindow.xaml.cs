using System;
using System.Linq;
using System.Windows;

namespace Nyelvvizsga
{
	public partial class MainWindow : Window
	{
		private DataProcessor dataProcessor;

		public MainWindow()
		{
			InitializeComponent();
			dataProcessor = new DataProcessor();
			dataProcessor.LoadData("sikeres.csv", "sikertelen.csv");
		}

		private void ShowTopLanguages(object sender, RoutedEventArgs e)
		{
			var topLanguages = dataProcessor.GetTopLanguages();
			txtResult.Text = string.Join(Environment.NewLine, topLanguages);
		}

		private void ShowHighestFailureRate(object sender, RoutedEventArgs e)
		{
			if (int.TryParse(txtEv.Text, out int year) && year >= 2009 && year <= 2017)
			{
				var result = dataProcessor.GetHighestFailureRate(year);
				txtResult.Text = result;
			}
			else
			{
				MessageBox.Show("Kérjük, adj meg egy évet 2009 és 2017 között.");
			}
		}

		private void ShowNoExams(object sender, RoutedEventArgs e)
		{
			if (int.TryParse(txtEv.Text, out int year) && year >= 2009 && year <= 2017)
			{
				var result = dataProcessor.GetNoExamsLanguages(year);
				txtResult.Text = string.Join(Environment.NewLine, result);
			}
			else
			{
				MessageBox.Show("Kérjük, adj meg egy évet 2009 és 2017 között.");
			}
		}

		private void SaveResults(object sender, RoutedEventArgs e)
		{
			dataProcessor.SaveSummary("osszesites.csv");
			MessageBox.Show("A fájl sikeresen mentésre került.");
		}
	}
}
