using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IrisSandbox.Converters
{
    /// <summary>
    /// Responsável por converter o tipo DateTimeOffset do .Net para Datetime no Banco de dados
    /// </summary>
    public static class DateTimeOffsetConverterEF
    {
        /// <summary>
        /// Aplicado para Data e Hora quando inserido ou recuperado alguma propriedade com DateTimeOffset
        /// </summary>
        public static ValueConverter<DateTimeOffset, DateTime> DateTimeOffsetUniversalLocalConverter =
            new ValueConverter<DateTimeOffset, DateTime>
            (
                // Converte a data recebida para Universal (UTC) para ser enviada ao banco de dados
                value => value.UtcDateTime,
                // Converte a data recebida do banco de dados para Data e hora local
                value => DateTime.SpecifyKind(value, DateTimeKind.Utc).ToLocalTime()
            );

        /// <summary>
        /// Aplicado somente para Data "fixa", ou seja, data que é absoluta, exemplo Data de Nascimento
        /// </summary>
        public static ValueConverter<DateTimeOffset, DateTime> DateTimeOffsetUniversalFixedConverter =
            new ValueConverter<DateTimeOffset, DateTime>
            (
                // Converte a data recebida para Universal (UTC) para ser enviada ao banco de dados
                value => value.UtcDateTime,
                // Converte a data recebida do banco de dados para Data e hora local
                value => DateTime.SpecifyKind(value, DateTimeKind.Utc)
            );
    }
}
