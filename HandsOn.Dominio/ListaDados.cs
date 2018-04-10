using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOn.Dominio
{
    public class ListaDados
    {
        public int IdSuspeito { get; set; }
        public IEnumerable<Suspeito> Suspeitos { get; set; }

        public int IdLocal { get; set; }
        public IEnumerable<Local> Locais { get; set; }

        public int IdArma { get; set; }
        public IEnumerable<Arma> Armas { get; set; }
    }
}
