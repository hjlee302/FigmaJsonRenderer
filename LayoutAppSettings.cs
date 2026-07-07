using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FigmaJsonRenderer;

public sealed class LayoutAppSettings
{
    [JsonPropertyName("IsTest")]
    public bool IsTest { get; set; } = true;

    [JsonPropertyName("ServerIp")]
    public string ServerIp { get; set; } = "127.0.0.1:5173";

    [JsonPropertyName("DeviceCode")]
    public string DeviceCode { get; set; } = "";

    [JsonPropertyName("LayoutsFolder")]
    public string LayoutsFolder { get; set; } = "Layouts";

    [JsonPropertyName("MockDevicesFile")]
    public string MockDevicesFile { get; set; } = "Mocks/devices.json";

    public static async Task<LayoutAppSettings> LoadAsync(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("appsettings.json could not be found.", path);
        }

        await using FileStream stream = File.OpenRead(path);
        LayoutAppSettings? settings = await JsonSerializer.DeserializeAsync<LayoutAppSettings>(
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (settings is null)
        {
            throw new InvalidOperationException("appsettings.json could not be loaded.");
        }

        if (string.IsNullOrWhiteSpace(settings.DeviceCode))
        {
            throw new InvalidOperationException("appsettings.json DeviceCode value is required.");
        }

        return settings;
    }
}
