using System.Collections.Generic;
using CHAOS.Portal.Client.Data.MCM;

namespace CHAOS.Portal.Client.Data.Upload
{
	public class UploadToken
	{
		public uint NoOfChunks { get; set; }

        public string UploadID { get; set; }
        public string BucketName { get; set; }
        public string Key { get; set; }

        public uint ChunkSize { get; set; }

		public IList<ChunkState> ETags { get; set; }
	}
}