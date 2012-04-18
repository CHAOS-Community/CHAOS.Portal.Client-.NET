using System;

namespace CHAOS.Portal.Client.Standard.Data
{
	public class ModuleAttribute : Attribute
	{
		private readonly string _Name;

		public ModuleAttribute(string name)
		{
			_Name = name;
		}

		public string Name
		{
			get { return _Name; }
		}
	}
}