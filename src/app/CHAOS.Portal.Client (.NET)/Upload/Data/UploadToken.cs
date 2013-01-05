using System.Collections.Generic;
using CHAOS.Portal.Client.MCM.Data;

namespace CHAOS.Portal.Client.Upload.Data
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