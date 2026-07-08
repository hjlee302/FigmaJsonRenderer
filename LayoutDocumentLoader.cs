using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace FigmaJsonRenderer;

public static class LayoutDocumentLoader
{
    private static readonly HttpClient HttpClient = new();

    // 서버 로컬 구분
    public static async Task<FigmaLayout> LoadAsync(LayoutAppSettings settings)
    {
        string json = settings.IsTest
            ? await LoadLocalJsonAsync(settings)
            : await LoadServerJsonAsync(settings);

        FigmaLayout? layout = JsonSerializer.Deserialize<FigmaLayout>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return layout ?? throw new InvalidOperationException("Layout JSON could not be loaded.");
    }

    private static async Task<string> LoadLocalJsonAsync(LayoutAppSettings settings)
    {
        string layoutCode = await ResolveMockLayoutCodeAsync(settings);
        string layoutPath = Path.Combine(
            AppContext.BaseDirectory,
            settings.LayoutsFolder,
            EnsureJsonFileName(layoutCode));

        if (!File.Exists(layoutPath))
        {
            throw new FileNotFoundException($"Local layout JSON could not be found: {layoutPath}", layoutPath);
        }

        return await File.ReadAllTextAsync(layoutPath);
    }

    private static async Task<string> ResolveMockLayoutCodeAsync(LayoutAppSettings settings)
    {
        string mockDevicesPath = Path.Combine(AppContext.BaseDirectory, settings.MockDevicesFile);

        if (!File.Exists(mockDevicesPath))
        {
            throw new FileNotFoundException($"Mock devices JSON could not be found: {mockDevicesPath}", mockDevicesPath);
        }

        await using FileStream stream = File.OpenRead(mockDevicesPath);
        List<MockDevice>? devices = await JsonSerializer.DeserializeAsync<List<MockDevice>>(
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        MockDevice? device = devices?.FirstOrDefault(x =>
            string.Equals(x.Code, settings.DeviceCode, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(x.DeviceCode, settings.DeviceCode, StringComparison.OrdinalIgnoreCase));

        if (device is null)
        {
            throw new InvalidOperationException($"Mock device could not be found for DeviceCode: {settings.DeviceCode}");
        }

        string? layoutCode = device.Layout?.Code ?? device.LayoutCode;

        if (string.IsNullOrWhiteSpace(layoutCode))
        {
            throw new InvalidOperationException($"Mock device layout code is empty for DeviceCode: {settings.DeviceCode}");
        }

        return layoutCode;
    }

    private static async Task<string> LoadServerJsonAsync(LayoutAppSettings settings)
    {
        Uri uri = BuildServerLayoutUri(settings.ServerIp, settings.DeviceCode);
        using HttpResponseMessage response = await HttpClient.GetAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Server layout JSON request failed: {(int)response.StatusCode} {response.ReasonPhrase} ({uri})");
        }

        return await response.Content.ReadAsStringAsync();
    }

    private static Uri BuildServerLayoutUri(string serverIp, string deviceCode)
    {
        if (string.IsNullOrWhiteSpace(serverIp))
        {
            throw new InvalidOperationException("appsettings.json ServerIp value is required when IsTest is false.");
        }

        string baseUrl = serverIp.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                         serverIp.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
            ? serverIp
            : $"http://{serverIp}";

        return new Uri($"{baseUrl.TrimEnd('/')}/api/Devices/{Uri.EscapeDataString(deviceCode.Trim())}/layout");
    }

    private static string EnsureJsonFileName(string layout)
    {
        string layoutName = EnsureLayoutName(layout);
        return layoutName.EndsWith(".json", StringComparison.OrdinalIgnoreCase)
            ? layoutName
            : $"{layoutName}.json";
    }

    private static string EnsureLayoutName(string layout)
    {
        return Path.GetFileNameWithoutExtension(layout.Trim());
    }

    private sealed class MockDevice
    {
        public string? Code { get; set; }

        public string? DeviceCode { get; set; }

        public string? LayoutCode { get; set; }

        public MockLayout? Layout { get; set; }
    }

    private sealed class MockLayout
    {
        public string? Code { get; set; }
    }
}
