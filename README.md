# NetLib

## ChunkEncoder

将数据编码成 http 协议中的 chunk 进行传输。使用例程如下

```csharp
app.MapGet("/ts.mp4", async (HttpContext context) =>
{
	try
	{
		Console.WriteLine("请求视频文件");
		context.Response.Headers.Add("Content-Disposition", "attachment; filename=\"ts.mp4\"");
		context.Response.Headers.Add("Transfer-Encoding", "chunked");
		context.Response.Headers.ContentType = "video/mp4";
		FileStream fileStream = File.OpenRead(_webRootPath + "/ts.mp4");
		// 在这里使用 ChunkEncoder 从文件流中读取数据，写到 http 响应流中
		await ChunkEncoder.WriteContentAsync(fileStream, context.Response.Body);
		// 所有数据都写完了需要调用 WriteTrailerAsync 写入结束标志从而结束本次传输
		await ChunkEncoder.WriteTrailerAsync(context.Response.Body);
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex.Message);
	}
});
```

## 为什么本库中没有提供 Chunk 的解码器？

因为不需要。C# 的 HttpClient 支持接收 Chunk 编码的数据，自动解码，对用户来说是无感的。浏览器也都支持 chunk 编码，在获取 chunk 编码的文件时能够自动进行解码。

ASP.NET Core 中我没有找到对数据进行 chunk 编码的官方库，所以自己写了一个。
