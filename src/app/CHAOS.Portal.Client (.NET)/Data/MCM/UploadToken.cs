using System.Collections.Generic;

namespace CHAOS.Portal.Client.Data.MCM
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