﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class KsiazkaListaAutorow
    {
        public Ksiazka Ksiazka { get; set; }
        public List<AutorKsiazki> ListaAutorow { get; set; }
    }
}