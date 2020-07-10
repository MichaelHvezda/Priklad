using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Priklad
{
    class Zakazka
    {
        public string klient { get; set; }
        public string ic { get; set; }
        public List<Objednavka> objednavky { get; set; }

        public Zakazka(string klient, string ic, List<Objednavka> objednavky)
        {
            this.klient = klient;
            this.ic = ic;
            this.objednavky = objednavky;
        }

        public override string ToString()
        {
            string pom= "";
            foreach (Objednavka objednavka1 in objednavky)
            {
                pom = pom + objednavka1.ToString();
            }
            return (klient + " " + ic + " " + pom);
        }
    }
}
