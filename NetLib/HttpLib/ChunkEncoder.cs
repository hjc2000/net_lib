﻿using System.Text;

namespace NetLib.HttpLib;
public class ChunkEncoder
{
	public static async Task WriteContentAsync(Stream sourceStream, Stream dstStream)
	{
		byte[] readBuff = new byte[1500];
		while (true)
		{
			int readCount = await sourceStream.ReadAsync(readBuff, 0, readBuff.Length);
			if (readCount == 0) break;
			await WriteContentAsync(readBuff, 0, readCount, dstStream);
		}
	}

	public static async Task WriteContentAsync(byte[] buff, int offset, int count, Stream dstStream)
	{
		string length = $"{count:x}\r\n";
		await dstStream.WriteAsync(Encoding.UTF8.GetBytes(length));
		await dstStream.WriteAsync(buff, offset, count);
		await dstStream.WriteAsync(CRLF);
	}

	public static async Task WriteTrailerAsync(Stream dstStream)
	{
		await dstStream.WriteAsync(Trailer);
	}

	public static byte CR { get; } = 13;
	public static byte LF { get; } = 10;
	public static byte[] CRLF { get; } = new byte[2] { 13, 10 };
	public static byte[] Trailer => Encoding.ASCII.GetBytes("0\r\n\r\n");

}
