using IrisSandbox.Attributes;
using IrisSandbox.Contexts.Common;
using IrisSandbox.Converters;
using IrisSandbox.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace IrisSandbox.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder UseValueConverters(this ModelBuilder modelBuilder, DbContextBase dbContext)
        {
            // Varre todas as entidades da modelagem do EF
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Se o entityType não for do escopo de IEntity, pula as alterações seguintes
                // Ex.: Model.Analito -> ok
                // Ex.: System.Enum -> continue
                if (!typeof(IEntity).IsAssignableFrom(entityType.ClrType))
                {
                    continue;
                }

                // Aplica conversion em todas as propriedades de DateTime de cada entidade model
                foreach (PropertyInfo property in GetTypedProperties<DateTime>(entityType).Concat(GetTypedProperties<DateTime?>(entityType)))
                {
                    // Aplica o converter para o type
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(GetDateTimeConverter(property));
                }

                // Aplica conversion em todas as propriedades de DateTimeOffset de cada entidade model
                foreach (PropertyInfo property in GetTypedProperties<DateTimeOffset>(entityType).Concat(GetTypedProperties<DateTimeOffset?>(entityType)))
                {
                    // Aplica o converter para o type
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(GetDateTimeOffsetConverter(property));
                }
            }

            return modelBuilder;
        }

        private static IEnumerable<PropertyInfo> GetTypedProperties<T>(IMutableEntityType entityType)
        {
            return entityType.ClrType
                .GetProperties()
                .Where(p => p.PropertyType == typeof(T))
                .ToList();
        }

        private static ValueConverter<DateTime, DateTime> GetDateTimeConverter(PropertyInfo property)
        {
            // Exceção: Se tiver o atributo, deve sempre considerar a data como UTC em TODAS as camadas
            return property.GetAttribute<UtcDatetimeAttribute>() != null
                ? DateTimeConverterEF.DateTimeUniversalFixedConverter
                : DateTimeConverterEF.DateTimeUniversalLocalConverter;
        }

        private static ValueConverter<DateTimeOffset, DateTime> GetDateTimeOffsetConverter(PropertyInfo property)
        {
            // Exceção: Se tiver o atributo, deve sempre considerar a data como UTC em TODAS as camadas
            return property.GetAttribute<UtcDatetimeAttribute>() != null
                ? DateTimeOffsetConverterEF.DateTimeOffsetUniversalFixedConverter
                : DateTimeOffsetConverterEF.DateTimeOffsetUniversalLocalConverter;
        }
    }
}
