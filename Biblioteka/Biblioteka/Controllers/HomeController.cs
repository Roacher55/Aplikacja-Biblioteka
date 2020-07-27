using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace Biblioteka.Controllers
{
    public class HomeController : Controller
    {
        BibliotekaBazaEntities db = new BibliotekaBazaEntities();
        
        // GET: Home
        public ActionResult ListaWypozyczen()
        {
         //   db.Configuration.ProxyCreationEnabled = false;
            return View(db.WypozyczenieKsiazki.ToList());
        }

        public ActionResult ListaWypozyczenCzytelnik(int czytelnik)
        {

            List<WypozyczenieKsiazki> czytelnikLista = new List<WypozyczenieKsiazki>();
            foreach(var c in db.WypozyczenieKsiazki.ToList())
            {
                if(czytelnik == c.CzytelnikKluczObcyId)
                {
                    czytelnikLista.Add(c);
                }
            }
            return View("ListaWypozyczen",czytelnikLista);
        }

        public ActionResult ListaWypozyczenKsiazka(int ksiazka)
        {

            List<WypozyczenieKsiazki> ksiazkaLista = new List<WypozyczenieKsiazki>();
            foreach (var k in db.WypozyczenieKsiazki.ToList())
            {
                if (ksiazka == k.KsiazkaKluczObcyId)
                {
                    ksiazkaLista.Add(k);
                }
            }
            return View("ListaWypozyczen", ksiazkaLista);
        }

        public ActionResult ListaWypozyczenPracownik(int pracownik)
        {

            List<WypozyczenieKsiazki> pracownikLista = new List<WypozyczenieKsiazki>();
            foreach (var p in db.WypozyczenieKsiazki.ToList())
            {
                if (pracownik == p.PracownikKluczObcyId)
                {
                    pracownikLista.Add(p);
                }
            }
            return View("ListaWypozyczen", pracownikLista);
        }



        //[HttpPost]
        public ActionResult ListaWypozyczenAktualizacja(int idWypozyczenieKsiazki, int ksiazkaKluczObcyId)
        {
            
            foreach (var w in db.WypozyczenieKsiazki.ToList())
            {
                if (w.IdWypozyczenieKsiazki == idWypozyczenieKsiazki)
                {
                    w.CzyAktualnieWypozyczona = false;
                }
            }

            foreach (var k in db.Ksiazka.ToList())
            {
                if (k.IdKsiazki == ksiazkaKluczObcyId)
                {
                    k.CZyKsiazkaWypozyczona = false;
                }
            }

            db.SaveChanges();
            
            MessageBox.Show("Książka oddana");
            return RedirectToAction("ListaWypozyczen");
        }


        public ActionResult DodajWypozyczenie()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var wypozyczenie = new Wypozyczenie { ListaKsiazka = db.Ksiazka.ToList(), ListaCzytelnik = db.Czytelnik.ToList(),
                WypozyczenieKsiazki = new WypozyczenieKsiazki(), ListaPracownik = db.Pracownik.ToList()};

            return View(wypozyczenie);


        }
        [HttpPost]
        public ActionResult DodajWypozyczenie(Wypozyczenie w)
        {
            db.WypozyczenieKsiazki.Add(w.WypozyczenieKsiazki);
            foreach (var k in db.Ksiazka.ToList())
            {
                if (k.IdKsiazki == w.WypozyczenieKsiazki.KsiazkaKluczObcyId)
                {
                    k.CZyKsiazkaWypozyczona = true;
                }
            }

            db.SaveChanges();
            MessageBox.Show("Dodano nowe wypozyczenie");
            return View();


        }

        public ActionResult Glowna()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            
            return View();

        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            MessageBox.Show("Wylogowanie zakończone pomyślnie");
            return View("Login");

        }

        public ActionResult ZmienHaslo()
        {

            return View();

        
        }
        [HttpPost]
        public ActionResult ZmienHaslo(Pracownik p)
        {
            foreach( Pracownik pracownik in db.Pracownik.ToList())
            {
                if (pracownik.IdPracownika.ToString() == Session["IdPracownika"].ToString())
                {
                    pracownik.HasloPracownika = p.HasloPracownika;
                    db.SaveChanges();
                    MessageBox.Show("Zmiana hasła udana");
                    return View();
                }
            }
            MessageBox.Show("Zmiana hasła nie udana");
            return View();


        }

        [HttpPost]
        public ActionResult Login(Pracownik p)
        {
            foreach( var pracownik in db.Pracownik.ToList())
            {
                if(p.ImiePracownika == pracownik.ImiePracownika && p.NazwiskoPracownika == pracownik.NazwiskoPracownika && p.HasloPracownika == pracownik.HasloPracownika)
                {


                    Session["IdPracownika"] = pracownik.IdPracownika;
                    Session["Zalogowany"] = "Zalogowany/a " + pracownik.ImiePracownika.ToString() +  " " + pracownik.NazwiskoPracownika.ToString();
                    Session["Uprawnienia"] = pracownik.CzyZalogowanyPracownik;
                    return View("Glowna");

                    
                }

            }

            return View();

        }

        public ActionResult ListaCzytelnikow()
        {

            return View(db.Czytelnik.ToList());


        }

        public ActionResult ListaAutorow()
        {
            //var autorzy = db.AutorKsiazki.Include("Ksiazka").ToList();
            var autorzy = db.AutorKsiazki.ToList();
            return View(autorzy);


        }

        public ActionResult ListaPracownikow()
        {
            //var autorzy = db.AutorKsiazki.Include("Ksiazka").ToList();
            
            return View(db.Pracownik.ToList());


        }

        //   [HttpPost]
        public ActionResult KsiazkiAutora(AutorKsiazki a)
        {

            List<Ksiazka> k = db.Ksiazka.Where(x => x.AutorKsiazki.IdAutora == a.IdAutora).ToList();


            return View(k);


        }

        public ActionResult ListaKsiazek()
        {
            
            return View(db.Ksiazka.ToList());


        }

        public ActionResult DodajCzytelnika()
        {

            return View();


        }
        [HttpPost]
        public ActionResult DodajCzytelnika(Czytelnik c)
        {
            db.Czytelnik.Add(c);
            db.SaveChanges();
            MessageBox.Show("Dodanie nowego czytelnika zakończyło się pomyślnie");
            return View();


        }

        public ActionResult DodajAutora()
        {

            return View();


        }
        [HttpPost]
        public ActionResult DodajAutora(AutorKsiazki a)
        {
            db.AutorKsiazki.Add(a);
            db.SaveChanges();
            MessageBox.Show("Dodanie nowego autora zakończyło się pomyślnie");
            return View();


        }
        
        public ActionResult DodajKsiazka()
        {
            //  context.Configuration.ProxyCreationEnabled
            db.Configuration.ProxyCreationEnabled = false;
            var ksiazkaListaAutorow = new KsiazkaListaAutorow() { Ksiazka = new Ksiazka(), ListaAutorow = db.AutorKsiazki.ToList() };
            
            return View(ksiazkaListaAutorow);


        }
        [HttpPost]
        public ActionResult DodajKsiazka(KsiazkaListaAutorow k)
        {
            db.Ksiazka.Add(k.Ksiazka);
            db.SaveChanges();
            MessageBox.Show("Dodanie nowej książki zakończone pomyślnie");
            return View();


        }


        public ActionResult DodajPracownika()
        {
             

            return View();


        }
        [HttpPost]
        public ActionResult DodajPracownika(Pracownik p)
        {
            db.Pracownik.Add(p);
            db.SaveChanges();
            MessageBox.Show("Dodanie nowego pracownika zakończone zostało pomyślnie");
            return View();


        }



    }
}