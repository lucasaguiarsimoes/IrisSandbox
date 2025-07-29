using System.Reflection;

namespace IrisSandbox.Attributes
{
    /// <summary>
    /// Classe para extensões de string
    /// </summary>
    public static class AttributeExtensions
    {
        /// <summary>
        /// Obtém o atributo customizado de um tipo
        /// </summary>
        public static A? GetAttribute<A>(this ICustomAttributeProvider attributeProvider) where A : Attribute
        {
            // Procura o atributo especificado
            IEnumerable<A> atributos = attributeProvider.GetAttributes<A>();

            // Procura o primeiro atributo válido
            A? atributo = atributos?.FirstOrDefault();

            // Sai se não tiver encontrado nenhum atributo do tipo esperado
            if (atributo == null)
            {
                return null;
            }

            // Retorna o primeiro atributo, caso mais de um tenha sido encontrado
            return atributo;
        }

        /// <summary>
        /// Obtém os atributos customizados de um tipo
        /// </summary>
        public static IEnumerable<A> GetAttributes<A>(this ICustomAttributeProvider attributeProvider) where A : Attribute
        {
            // Procura o atributo especificado
            return attributeProvider.GetCustomAttributes(typeof(A), true).Cast<A>();
        }
    }
}
