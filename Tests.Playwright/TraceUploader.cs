using System.Text.RegularExpressions;
using Azure.Identity;
using Azure.Storage.Blobs;

namespace Tests.Playwright
{
    // Uploads local Playwright trace files to the Storage account linked to the Playwright Workspace,
    // using the same container ({workspaceId}) and folder ({runId}) convention as the Playwright Workspaces
    // portal's Test Run viewer, so the file is at least found under the run it was produced by.
    internal static class TraceUploader
    {
        public static async Task UploadAsync(string? storageAccountName, string localFilePath, string blobName)
        {
            if (string.IsNullOrWhiteSpace(storageAccountName) || !File.Exists(localFilePath))
                return;

            var containerName = GetWorkspaceContainerName();
            if (containerName is null)
                return;

            var runId = Environment.GetEnvironmentVariable("PLAYWRIGHT_SERVICE_RUN_ID") ?? "local";

            var containerClient = new BlobContainerClient(
                new Uri($"https://{storageAccountName}.blob.core.windows.net/{containerName}"),
                new DefaultAzureCredential());

            await containerClient.CreateIfNotExistsAsync();

            await using var stream = File.OpenRead(localFilePath);
            await containerClient.UploadBlobAsync($"{runId}/{blobName}", stream);
        }

        // Workspace container name = the workspace id from PLAYWRIGHT_SERVICE_URL, lowercased and sanitized.
        private static string? GetWorkspaceContainerName()
        {
            var serviceUrl = Environment.GetEnvironmentVariable("PLAYWRIGHT_SERVICE_URL");
            if (string.IsNullOrWhiteSpace(serviceUrl))
                return null;

            var match = Regex.Match(serviceUrl, "/playwrightworkspaces/([^/]+)/");
            if (!match.Success)
                return null;

            return Regex.Replace(match.Groups[1].Value.ToLowerInvariant(), "[^a-z0-9-]", "-");
        }
    }
}
