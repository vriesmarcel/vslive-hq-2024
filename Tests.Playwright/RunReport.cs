using System.Collections.Concurrent;
using System.Net;
using System.Text;

namespace Tests.Playwright
{
    // Collects per-test results and publishes a minimal {runId}/index.html report next to the uploaded traces,
    // which is the blob the Playwright Workspaces portal (TestReport view) opens for a test run.
    // The .NET SDK registers the run itself; it just doesn't produce an HTML report like the JS reporter does.
    internal static class RunReport
    {
        private static readonly ConcurrentQueue<Entry> Entries = new();

        public record Entry(string Name, string Os, string Outcome, string? Message, string? TraceBlobName, TimeSpan Duration);

        public static void Add(Entry entry) => Entries.Enqueue(entry);

        public static async Task PublishAsync(string? storageAccountName)
        {
            if (Entries.IsEmpty)
                return;

            await TraceUploader.UploadContentAsync(storageAccountName, BuildHtml(), "index.html", "text/html; charset=utf-8");
        }

        private static string BuildHtml()
        {
            var items = Entries.OrderBy(e => e.Name).ThenBy(e => e.Os).ToList();
            var passed = items.Count(e => e.Outcome == "Passed");
            var failed = items.Count(e => e.Outcome == "Failed");

            var sb = new StringBuilder();
            sb.Append("""
                <!DOCTYPE html>
                <html lang="en">
                <head>
                <meta charset="utf-8">
                <title>Playwright test report</title>
                <style>
                  body { font-family: Segoe UI, sans-serif; margin: 24px; }
                  table { border-collapse: collapse; width: 100%; }
                  th, td { text-align: left; padding: 6px 10px; border-bottom: 1px solid #ddd; vertical-align: top; }
                  .Passed { color: #107c10; } .Failed { color: #d13438; } .Skipped, .Inconclusive { color: #8a8886; }
                  pre { white-space: pre-wrap; margin: 4px 0 0; font-size: 12px; color: #605e5c; }
                </style>
                </head>
                <body>
                """);
            sb.Append($"<h1>Playwright test report</h1><p>Run <code>{WebUtility.HtmlEncode(TraceUploader.GetRunId())}</code> &middot; ");
            sb.Append($"{items.Count} tests &middot; <span class=\"Passed\">{passed} passed</span> &middot; <span class=\"Failed\">{failed} failed</span> &middot; generated {DateTime.UtcNow:u}</p>");
            sb.Append("<table><thead><tr><th>Test</th><th>OS</th><th>Result</th><th>Duration</th><th>Trace</th></tr></thead><tbody>");

            foreach (var e in items)
            {
                sb.Append("<tr>");
                sb.Append($"<td>{WebUtility.HtmlEncode(e.Name)}");
                if (!string.IsNullOrWhiteSpace(e.Message))
                    sb.Append($"<pre>{WebUtility.HtmlEncode(e.Message)}</pre>");
                sb.Append("</td>");
                sb.Append($"<td>{WebUtility.HtmlEncode(e.Os)}</td>");
                sb.Append($"<td class=\"{WebUtility.HtmlEncode(e.Outcome)}\">{WebUtility.HtmlEncode(e.Outcome)}</td>");
                sb.Append($"<td>{e.Duration.TotalSeconds:0.0}s</td>");
                sb.Append(e.TraceBlobName is null
                    ? "<td></td>"
                    : $"<td><a class=\"trace\" data-trace=\"{WebUtility.HtmlEncode(Uri.EscapeDataString(e.TraceBlobName))}\" href=\"{WebUtility.HtmlEncode(Uri.EscapeDataString(e.TraceBlobName))}\" target=\"_blank\">View trace</a></td>");
                sb.Append("</tr>");
            }

            // Resolve each trace relative to this report's URL and keep any SAS query string the portal used to
            // load the report, then open it in the public trace viewer (same approach as the JS reporter).
            sb.Append("""
                </tbody></table>
                <script>
                  const base = new URL('.', location.href);
                  for (const a of document.querySelectorAll('a.trace')) {
                    const traceUrl = new URL(a.dataset.trace, base);
                    traceUrl.search = location.search;
                    a.href = 'https://trace.playwright.dev/?trace=' + encodeURIComponent(traceUrl.toString());
                  }
                </script>
                </body>
                </html>
                """);
            return sb.ToString();
        }
    }
}
