namespace FigmaJsonRenderer;

public sealed class LayoutBindingResolver
{
    private readonly Dictionary<string, Dictionary<string, string>> _slotValues;

    public LayoutBindingResolver()
    {
        _slotValues = new(StringComparer.OrdinalIgnoreCase)
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
    }

    public LayoutBindingResolver(DeviceResponse device)
    {
        _slotValues = new(StringComparer.OrdinalIgnoreCase);

        if (device.Counters.Count > 0)
        {
            AddCounterSlot("left", device, device.Counters[0]);
        }

        if (device.Counters.Count > 1)
        {
            AddCounterSlot("right", device, device.Counters[1]);
        }

        if (_slotValues.Count == 0)
        {
            DeviceCounter? counter = device.Counter ?? device.Counters.FirstOrDefault();

            if (counter is not null)
            {
                AddCounterSlot("default", device, counter);
            }
        }
    }

    private void AddCounterSlot(string slot, DeviceResponse device, DeviceCounter counter)
    {
        string? serviceAreaCode = counter.DefaultServiceAreaCode;

        DeviceServiceArea? serviceArea = device.ServiceAreaGroup?.ServiceAreas
            .FirstOrDefault(x => string.Equals(x.Code, serviceAreaCode, StringComparison.OrdinalIgnoreCase));

        string colorCode = serviceArea?.ColorCode ?? "";

        _slotValues[slot] = new(StringComparer.OrdinalIgnoreCase)
        {
            ["device.counter.boothNumber"] = counter.BoothNumber.ToString(),
            ["serviceArea.name"] = serviceArea?.Name ?? "",
            ["serviceArea.ticketTitle"] = serviceArea?.TicketTitle ?? "",
            ["serviceArea.colorCode"] = colorCode,
            ["serviceArea.color"] = colorCode,
            ["device.counter.serviceArea.color"] = colorCode
        };
    }

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
