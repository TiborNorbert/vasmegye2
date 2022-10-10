using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace vasmegye2
{
    class Adat
    {
        readonly string szm;

        public string Szm => szm;

        public Adat(string szm)
        {
            this.szm = szm;
        }
        public int evSzam()
        {
            int ev = int.Parse(szm.Substring(2, 2));
            ev = Szm[0] == '1' || Szm[0] == '2' ? 1900 + ev : 2000 + ev;
            return ev;
        }
    }
}
