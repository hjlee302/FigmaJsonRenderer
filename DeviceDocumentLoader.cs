using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FigmaJsonRenderer
{
    public static class DeviceDocumentLoader
    {
        private static readonly HttpClient HttpClient = new();

        public static async Task<DeviceResponse> LoadAsync(LayoutAppSettings settings)
        {
            Uri uri = BuildServerDeviceUri(settings.ServerIp, settings.DeviceCode);
            using HttpResponseMessage response = await HttpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    $"Server device request failed: {(int)response.StatusCode} {response.ReasonPhrase} ({uri})");
            }

            string json = await response.Content.ReadAsStringAsync();

            DeviceResponse? device = JsonSerializer.Deserialize<DeviceResponse>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return device ?? throw new InvalidOperationException("Device JSON could not be loaded.");
        }

        private static Uri BuildServerDeviceUri(string serverIp, string deviceCode)
        {
            string baseUrl = serverIp.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                             serverIp.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
                ? serverIp
                : $"http://{serverIp}";

            return new Uri($"{baseUrl.TrimEnd('/')}/api/Devices/{Uri.EscapeDataString(deviceCode.Trim())}");
        }
    }
}
