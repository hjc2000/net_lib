using System.Text;

namespace NetLib.HttpLib;
public static class ChunkEncoder
{
	/// <summary>
	/// 以分块的方式向目标流写入数据
	/// </summary>
	/// <param name="sourceStream"></param>
	/// <param name="dstStream"></param>
	/// <returns></returns>
	public static async Task ChunkWriteContentToAsync(this Stream sourceStream, Stream dstStream)
	{
		byte[] readBuff = new byte[1500];
		while (true)
		{
			int readCount = await sourceStream.ReadAsync(readBuff);
			if (readCount == 0)
			{
				break;
			}

			await ChunkWriteContentToAsync(readBuff, 0, readCount, dstStream);
		}
	}

	/// <summary>
	/// 以分块的方式向目标流写入数据
	/// </summary>
	/// <param name="buff"></param>
	/// <param name="offset"></param>
	/// <param name="count"></param>
	/// <param name="dstStream"></param>
	/// <returns></returns>
	public static async Task ChunkWriteContentToAsync(this byte[] buff, int offset, int count, Stream dstStream)
	{
		string length = $"{count:x}\r\n";
		await dstStream.WriteAsync(Encoding.UTF8.GetBytes(length));
		await dstStream.WriteAsync(buff, offset, count);
		await dstStream.WriteAsync(CRLF);
	}

	public static async Task ChunkWriteTrailerAsync(this Stream dstStream)
	{
		await dstStream.WriteAsync(Trailer);
	}

	public static byte CR { get; } = 13;
	public static byte LF { get; } = 10;
	public static byte[] CRLF { get; } = new byte[2] { 13, 10 };

	/// <summary>
	/// 0\r\n\r\n
	/// </summary>
	public static byte[] Trailer { get; } = new byte[5] { 48, 13, 10, 13, 10 };

}
