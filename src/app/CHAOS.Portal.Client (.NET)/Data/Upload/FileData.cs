using System.IO;
using CHAOS.Extensions;

namespace CHAOS.Portal.Client.Data.Upload
{
	public class FileData
	{
		public string Name { get; private set; }
		public Stream Data { get; private set; }
		public long Position { get; private set; }
		public long Length { get; private set; }

		public FileData(string name, Stream data, long position, long length)
		{
			Name = name.ValidateIsNotNull("name");
			Data = data.ValidateIsNotNull("data");
			Position = position;
			Length = length;
		}
	}
}