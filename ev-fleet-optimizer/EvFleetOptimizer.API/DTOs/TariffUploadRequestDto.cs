namespace EvFleetOptimizer.API.DTOs;

public class TariffUploadRequestDto
{
    public string? CsvBase64 { get; set; } // For CSV uploads
    public List<TimeOfUseTariffDto>? TimeOfUseTariffs { get; set; } // For ToU schedule
}

public class TimeOfUseTariffDto
{
    public string Name { get; set; } = string.Empty;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public double PricePerKwh { get; set; }
}
