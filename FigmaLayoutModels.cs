using System.Text.Json.Serialization;

namespace FigmaJsonRenderer;

public sealed class FigmaLayout
{
    [JsonPropertyName("canvas")]
    public FigmaCanvas Canvas { get; set; } = new();

    [JsonPropertyName("components")]
    public List<FigmaComponent> Components { get; set; } = [];
}

public sealed class FigmaCanvas
{
    [JsonPropertyName("width")]
    public double Width { get; set; }

    [JsonPropertyName("height")]
    public double Height { get; set; }

    [JsonPropertyName("background")]
    public string Background { get; set; } = "#FFFFFF";
}

public sealed class FigmaComponent
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("x")]
    public double X { get; set; }

    [JsonPropertyName("y")]
    public double Y { get; set; }

    [JsonPropertyName("width")]
    public double Width { get; set; }

    [JsonPropertyName("height")]
    public double Height { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("fontSize")]
    public double? FontSize { get; set; }

    [JsonPropertyName("lineHeight")]
    public double? LineHeight { get; set; }

    [JsonPropertyName("color")]
    public string? Color { get; set; }

    [JsonPropertyName("textAlign")]
    public string? TextAlign { get; set; }

    [JsonPropertyName("verticalAlign")]
    public string? VerticalAlign { get; set; }

    [JsonPropertyName("fontWeight")]
    public string? FontWeight { get; set; }

    [JsonPropertyName("background")]
    public string? Background { get; set; }

    [JsonPropertyName("borderRadius")]
    public double? BorderRadius { get; set; }

    [JsonPropertyName("strokeColor")]
    public string? StrokeColor { get; set; }

    [JsonPropertyName("strokeWidth")]
    public double? StrokeWidth { get; set; }

    [JsonPropertyName("iconName")]
    public string? IconName { get; set; }

    [JsonPropertyName("src")]
    public string? Src { get; set; }

    [JsonPropertyName("imageName")]
    public string? ImageName { get; set; }

    [JsonPropertyName("thickness")]
    public double? Thickness { get; set; }

    [JsonPropertyName("colorA")]
    public string? ColorA { get; set; }

    [JsonPropertyName("colorB")]
    public string? ColorB { get; set; }

    [JsonPropertyName("intervalMs")]
    public double? IntervalMs { get; set; }

    [JsonPropertyName("durationMs")]
    public double? DurationMs { get; set; }

    [JsonPropertyName("role")]
    public string? Role { get; set; }

    [JsonPropertyName("slot")]
    public string? Slot { get; set; }

    [JsonPropertyName("bindings")]
    public Dictionary<string, string>? Bindings { get; set; }
}
