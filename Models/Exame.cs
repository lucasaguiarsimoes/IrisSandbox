using IrisSandbox.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisSandbox.Models
{
    public class Exame : EntityDefaultVersioned<Exame>
    {
        public string Codigo { get; set; } = null!;
        public string Descricao { get; set; } = null!;

        public List<AmostraExame> Amostras { get; set; } = null!;
    }
}
