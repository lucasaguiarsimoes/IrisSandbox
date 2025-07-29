using IrisSandbox.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisSandbox.Models
{
    public class Amostra : EntityDefaultVersioned<Amostra>
    {
        public string Identificacao { get; set; } = null!;
        public string? Pedido { get; set; }
        public string? Origem { get; set; }
        public decimal? Volume { get; set; }

        public List<AmostraExame> Exames { get; set; } = null!;
    }
}
