using System;
using System.Collections.Generic;
using System.Text;

namespace Priklad
{
    class Dodavka
    {
        public DateTime datumVyroby { get; set; }
        public string pocetVyrobenychKusu { get; set; }

        public Dodavka(DateTime datum, string pocetKusu)
        {
            this.datumVyroby = datum;
            this.pocetVyrobenychKusu = pocetKusu;
        }

        public override string ToString()
        {
            return (datumVyroby + " " + pocetVyrobenychKusu + " ");
        }

    }
}
