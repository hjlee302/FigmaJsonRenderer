namespace FigmaJsonRenderer;

public sealed class LayoutBindingResolver
{
    private readonly Dictionary<string, Dictionary<string, string>> _slotValues = new(StringComparer.OrdinalIgnoreCase)
    {
        ["left"] = new(StringComparer.OrdinalIgnoreCase)
        {
            ["serviceArea.name"] = "입원",
            ["serviceArea.colorCode"] = "#28A630",
            ["serviceArea.color"] = "#28A630",
            ["device.counter.serviceArea.color"] = "#28A630",
            ["device.counter.boothNumber"] = "7"
        },
        ["right"] = new(StringComparer.OrdinalIgnoreCase)
        {
            ["serviceArea.name"] = "퇴원",
            ["serviceArea.colorCode"] = "#28A630",
            ["serviceArea.color"] = "#28A630",
            ["device.counter.serviceArea.color"] = "#28A630",
            ["device.counter.boothNumber"] = "8"
        }
    };

    public string? Resolve(string? slot, string path)
    {
        if (slot is not null &&
            _slotValues.TryGetValue(slot, out Dictionary<string, string>? slotValues) &&
            slotValues.TryGetValue(path, out string? slotValue))
        {
            return slotValue;
        }

        foreach (Dictionary<string, string> fallbackValues in _slotValues.Values)
        {
            if (fallbackValues.TryGetValue(path, out string? value))
            {
                return value;
            }
        }

        return null;
    }
}
