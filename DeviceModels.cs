namespace FigmaJsonRenderer
{
    public sealed class DeviceResponse
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public DeviceServiceAreaGroup? ServiceAreaGroup { get; set; }
        public DeviceCounter? Counter { get; set; }
        public List<DeviceCounter> Counters { get; set; } = [];
    }

    public sealed class DeviceServiceAreaGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<DeviceServiceArea> ServiceAreas { get; set; } = [];
    }

    public sealed class DeviceServiceArea
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public int BaseTicketNumber { get; set; }
        public int RemainingTicketCount { get; set; }
        public string ColorCode { get; set; } = "";
        public string TicketTitle { get; set; } = "";
    }

    public sealed class DeviceCounter
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public int BoothNumber { get; set; }
        public string? DefaultServiceAreaCode { get; set; }
        public string? LastCalledServiceAreaCode { get; set; }
    }
}
