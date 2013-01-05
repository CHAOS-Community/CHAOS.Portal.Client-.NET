namespace CHAOS.Portal.Client.MCM.Data
{
	public class File
	{
		public int ID { get; set; }
		public string Filename { get; set; }
		public string OriginalFilename { get; set; }
		public string Token { get; set; }
		public string URL { get; set; }
		public int FormatID { get; set; }
		public string Format { get; set; }
		public string FormatCategory { get; set; }
		public string FormatType { get; set; }
	}
}