namespace CHAOS.Portal.Client.Data.GeoLocator
{
	//CHAOS.GeoLocator.Data.LocationInfo
	public class LocationInfo
	{
		public int ID { get; set; }
		public string Country { get; set; }
		public string CountryCode { get; set; }
		public string City { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public double GMT { get; set; }
		public double DST { get; set; }
		public string CurrentIP { get; set; }
	}
}