//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biblioteka
{
    using System;
    using System.Collections.Generic;
    
    public partial class WypozyczenieKsiazki
    {
        public int IdWypozyczenieKsiazki { get; set; }
        public int PracownikKluczObcyId { get; set; }
        public int CzytelnikKluczObcyId { get; set; }
        public int KsiazkaKluczObcyId { get; set; }
        public bool CzyAktualnieWypozyczona { get; set; }
    
        public virtual Czytelnik Czytelnik { get; set; }
        public virtual Ksiazka Ksiazka { get; set; }
        public virtual Pracownik Pracownik { get; set; }
    }
}
