using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections;

namespace Priklad
{
    class ExcelReader
    {
        private string cesta;
        private ExcelPackage p;
        FileInfo fileInfo;
        ExcelWorkbook wb;
        ExcelWorksheet ws;

        //označeni mista umisteni souboru
        public ExcelReader(string cesta)
        {
            this.cesta = cesta;
        }

        //otevreni a cteni souboru
        public List<Zakazka> openAndReadData()
        {
            var fi = new FileInfo(cesta);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            List<Zakazka> zakazky = new List<Zakazka>();
            string klient;
            string ic;
            string zakazka;
            bool prulez = true;
            int radek = 1;

            Console.WriteLine("Start");

            using (p = new ExcelPackage(fi))
            {
                //otevreni souboru
                wb = p.Workbook;
                ws = wb.Worksheets["List1"];
                
                //prolezani souboru dokud se nenarazi na prazdny radek
                while (prulez)
                {
                    radek++;
                   
                    //nacteni informaci o klientovy
                    klient = ws.GetValue<string>(radek, 1);
                    
                    //ukonceni cteni pokud je pole pro klienta prazdne
                    if (klient== null)
                    {
                        prulez = false;
                        break;
                        
                    }

                    //nacteni cisla klienta a cisla zakazky
                    ic = ws.GetValue<string>(radek, 2);
                    zakazka = ws.GetValue<string>(radek, 3);

                    // vytvoreni pomocnych promenych pro moznost vkladat do seznamu dalsi objednavky od stejne firmy
                    var dalsiObjednavkaOdJedneFirmy = true;
                    var adresaVListuFirmy = 0;

                    //prohledani jiz vytvoreneho seznamu
                    for (int i = 0; i <= zakazky.Count - 1; i++)
                    {
                        //nalezeni jestli jiz firma je zahrnuta v seznamu
                        if (zakazky[i].ic.Equals(ic))
                        {
                            dalsiObjednavkaOdJedneFirmy = false;
                            adresaVListuFirmy= i;
                        }
                        
                    }

                    //pridani zakazky do seznamu, pokud jeste neni firma v seznamu zahrnuta nebo pokud je seznam prazdny
                    if (zakazky.Count == 0 || dalsiObjednavkaOdJedneFirmy)
                    {
                        //pridani prvku do seznamu
                        zakazky.Add(
                            new Zakazka(klient, ic, 
                            new List<Objednavka> { 
                                new Objednavka(zakazka, getDodavky(radek)) }));

                    }
                    else
                    {
                        //pridani dalsi objednavky od firmy, ktera jiz v seznamu figuruje 
                        zakazky[adresaVListuFirmy].objednavky.Add(
                            new Objednavka(zakazka, getDodavky(radek)));

                    }

                }

                //ulozeni a zavreni souboru
                fileInfo = new FileInfo(cesta);
                p.SaveAs(fileInfo);
            }

            Console.WriteLine("Seznam vytvoren");

            //vraceni vyslednemu seznamu 
            return zakazky;
        }

        //ziskani dodavek v danemu radku
        private List<Dodavka> getDodavky(int radek)
        {
            //pomocna vysledna promena
            List<Dodavka> vysledek = new List<Dodavka>();

            //projiti celeho radku v oblasti obsahujici informace o dodavkach
            for (int i = 4; i < 28; i++)
            {
                string mnozstviKusu;
                DateTime datum;

                //ziskani dat
                long dateNum = long.Parse(ws.GetValue<string>(1, i));
                datum = DateTime.FromOADate(dateNum);
                mnozstviKusu = ws.GetValue<string>(radek, i);

                //ulozeni do seznamu poud neni promena s informaci o mnozstvi kusu prazdna
                if (mnozstviKusu != null)
                {
                    vysledek.Add(new Dodavka(datum, mnozstviKusu));
                }
            }

            //vraceni vysledku
            return vysledek;
        }


    }
}
