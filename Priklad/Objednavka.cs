using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Priklad
{
    class Objednavka
    {
        public string zakazka { get; set; }
        public List<Dodavka> dodavky { get; set; }

        public Objednavka(string zakazka, List<Dodavka> dodavky)
        {
            this.zakazka = zakazka;
            this.dodavky = dodavky;
        }
        public override string ToString()
        {
            string pom = "";
            foreach (Dodavka dodavka1 in dodavky)
            {
                pom = pom + dodavka1.ToString();
            }
            return (zakazka + " " + pom);
        }
    }
}
