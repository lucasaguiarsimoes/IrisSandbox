using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IrisSandbox.Converters
{
    public static class DateTimeConverterEF
    {
        /// <summary>
        /// Aplicado para Data e Hora quando inserido ou recuperado alguma propriedade com DateTime
        /// </summary>
        public static ValueConverter<DateTime, DateTime> DateTimeUniversalLocalConverter =
            new ValueConverter<DateTime, DateTime>
            (
                // Converte a data recebida para Universal (UTC) para ser enviada ao banco de dados
                value => value.ToUniversalTime(),
                // Converte a data recebida do banco de dados para Data e hora local
                value => DateTime.SpecifyKind(value, DateTimeKind.Utc).ToLocalTime()
            );

        /// <summary>
        /// Aplicado somente para Data "fixa", ou seja, data que é absoluta, exemplo Data de Nascimento
        /// </summary>
        public static ValueConverter<DateTime, DateTime> DateTimeUniversalFixedConverter =
            new ValueConverter<DateTime, DateTime>
            (
                // Converte a data recebida para Universal (UTC) para ser enviada ao banco de dados
                value => value.ToUniversalTime(),
                // Converte a data recebida do banco de dados para Data e hora local
                value => DateTime.SpecifyKind(value, DateTimeKind.Utc)
            );
    }
}
