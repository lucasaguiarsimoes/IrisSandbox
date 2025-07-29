using IrisSandbox.Enums;
using IrisSandbox.Models.Common;

namespace IrisSandbox.Models
{
    public class ConfiguradorGeral : EntityDefaultVersioned<ConfiguradorGeral>
    {
        public SystemConfigurationItemEnum Item { get; set; }
        public string? ValorString { get; set; }
        public bool? ValorBoolean { get; set; }
        public long? ValorLong { get; set; }
        public DateTimeOffset? ValorDateTime { get; set; }
        public byte[]? ValorBytes { get; set; }
        public decimal? ValorDecimal { get; set; }
    }
}
