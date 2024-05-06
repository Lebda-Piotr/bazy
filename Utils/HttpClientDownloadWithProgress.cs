using System.IO;
using System.Net.Http;

public class HttpClientDownloadWithProgress : IDisposable {
	private string _downloadUrl;
	private string _destinationFilePath;
	private bool disposedValue;
	private readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromDays(1) };

	public string DownloadUrl { get => _downloadUrl; set => _downloadUrl = value; }
	public string DestinationFilePath { get => _destinationFilePath; set => _destinationFilePath = value; }

	public delegate void ProgressChangedHandler(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage);

	public event ProgressChangedHandler ProgressChanged;

	public HttpClientDownloadWithProgress() {

	}

	public HttpClientDownloadWithProgress(string downloadUrl, string destinationFilePath) {
		DownloadUrl = downloadUrl;
		DestinationFilePath = destinationFilePath;
	}

	protected virtual void Dispose(bool disposing) {
		if (!disposedValue)
		{
			if (disposing)
			{
				_httpClient?.Dispose();
			}

			disposedValue = true;
		}
	}
	public void Dispose() {
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	public async Task StartDownload() {
		if (string.IsNullOrEmpty(_downloadUrl) || string.IsNullOrEmpty(_destinationFilePath))
			throw new ArgumentNullException($"{nameof(DownloadUrl)} 또는 {nameof(DestinationFilePath)}", $"값이 비어있습니다.");

		using (var response = await _httpClient.GetAsync(DownloadUrl, HttpCompletionOption.ResponseHeadersRead))
			await DownloadFileFromHttpResponseMessage(response);
	}

	private async Task DownloadFileFromHttpResponseMessage(HttpResponseMessage response) {
		response.EnsureSuccessStatusCode();

		var totalBytes = response.Content.Headers.ContentLength;

		using (var contentStream = await response.Content.ReadAsStreamAsync())
			await ProcessContentStream(totalBytes, contentStream);
	}

	private async Task ProcessContentStream(long? totalDownloadSize, Stream contentStream) {
		var totalBytesRead = 0L;
		var readCount = 0L;
		var buffer = new byte[8192];
		var isMoreToRead = true;

		using (var fileStream = new FileStream(DestinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
		{
			do
			{
				var bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length);
				if (bytesRead == 0)
				{
					isMoreToRead = false;
					TriggerProgressChanged(totalDownloadSize, totalBytesRead);
					continue;
				}

				await fileStream.WriteAsync(buffer, 0, bytesRead);

				totalBytesRead += bytesRead;
				readCount += 1;

				if (readCount % 100 == 0)
					TriggerProgressChanged(totalDownloadSize, totalBytesRead);
			}
			while (isMoreToRead);
		}
	}

	private void TriggerProgressChanged(long? totalDownloadSize, long totalBytesRead) {
		if (ProgressChanged == null)
			return;

		double? progressPercentage = null;
		if (totalDownloadSize.HasValue)
			progressPercentage = Math.Round((double)totalBytesRead / totalDownloadSize.Value * 100, 2);

		ProgressChanged(totalDownloadSize, totalBytesRead, progressPercentage);
	}
}