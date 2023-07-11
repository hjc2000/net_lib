using System.Text;

namespace NetLib.HttpLib;
public class ChunkEncoder
{
	public ChunkEncoder(Stream srcStream, Stream dstStream, int bufferSize = 1500)
	{
		_srcStream = srcStream;
		_dstStream = dstStream;
		_readBuffer = new byte[bufferSize];
	}

	private Stream _srcStream;
	private Stream _dstStream;
	private byte[] _readBuffer;

	public async Task WriteToDstStreamAsync(CancellationToken token)
	{
		int count = 0;
		while (!token.IsCancellationRequested)
		{
			int readCount = await _srcStream.ReadAsync(_readBuffer);
			if (readCount == 0)
			{
				break;
			}
			else
			{
				await ChunkWriteContentToAsync(0, readCount);
			}

			Console.WriteLine($"第{count++}次分块");
		}
	}

	private async Task ChunkWriteContentToAsync(int offset, int count)
	{
		string length = $"{count:x}\r\n";
		await _dstStream.WriteAsync(Encoding.ASCII.GetBytes(length));
		await _dstStream.WriteAsync(_readBuffer, offset, count);
		await _dstStream.WriteAsync(CRLF);
	}

	public async Task WriteTrailerAsync()
	{
		await _dstStream.WriteAsync(Trailer);
	}

	public static byte CR { get; } = 13;
	public static byte LF { get; } = 10;
	public static byte[] CRLF { get; } = new byte[2] { 13, 10 };

	/// <summary>
	/// 0\r\n\r\n
	/// </summary>
	public static byte[] Trailer { get; } = new byte[5] { 48, 13, 10, 13, 10 };

}
