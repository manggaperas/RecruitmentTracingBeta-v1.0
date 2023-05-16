namespace Server.Models;

public interface IEmailTemplate
{
    string? EmailHR { get; set; }
    string? EmailUser { get; set; }
    string? EmailOffering { get; set; }
}
