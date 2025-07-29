using IrisSandbox.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisSandbox.Models
{
    public class AmostraExame : EntityDefaultVersioned<AmostraExame>
    {
        public long? AmostraID { get; set; }
        public long? ExameID { get; set; }
        public DateTimeOffset? DataHoraProducao { get; set; }

        public Amostra Amostra { get; set; } = null!;
        public Exame Exame { get; set; } = null!;
    }
}
