using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class Wypozyczenie
    {

        public List<Ksiazka> ListaKsiazka { get; set; }
        public List<Czytelnik> ListaCzytelnik { get; set; }
        public WypozyczenieKsiazki WypozyczenieKsiazki { get; set; }
        public List<Pracownik> ListaPracownik { get; set; }
    }
}