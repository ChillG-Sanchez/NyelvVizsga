namespace Nyelvvizsga
{
	public class Nyelvvizsga
	{
		public string Nyelv { get; set; }
		public int[] Sikeres { get; set; }
		public int[] Sikertelen { get; set; }

		public Nyelvvizsga(string nyelv, int[] sikeres, int[] sikertelen)
		{
			Nyelv = nyelv;
			Sikeres = sikeres;
			Sikertelen = sikertelen;
		}
	}
}
