using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class AdminNarudzbina
    {
        private static KnjizaraDBEntities db = new KnjizaraDBEntities();

        public static string AKTIVNA = "Aktivna";
        public static string POSLATA = "Poslata";

        public int IDNarudzbine { get; set; }
        public string Status { get; set; }
        public DateTime DatumNarucivanja { get; set; }
        public Korisnici Narucilac { get; set; }
        public List<KnjigaNarudzbenica> Narudzbenice { get; set; }

        public static List<AdminNarudzbina> TransformList(List<Narudzbina> narudzbine)
        {
            List<AdminNarudzbina> adminNarudzbine = new List<AdminNarudzbina>();
            foreach(var n in narudzbine)
            {
                Korisnici narucilac = db.Korisnicis.Find(n.id_osoba);
                List<KnjigaNarudzbenica> knjigaNarudzbenicas = new List<KnjigaNarudzbenica>();

                List<Narudzbina_Knjiga> narudzbina_Knjigas = db.Narudzbina_Knjiga
                    .Where(nk => nk.id_narudzbine == n.id_narudzbine)
                    .ToList();
                foreach (var nk in narudzbina_Knjigas)
                {
                    Knjige knjiga = db.Knjiges.Find(nk.id_knjige);
                    KnjigaNarudzbenica knjigaNarudzbenica = new KnjigaNarudzbenica
                    {
                        Naziv = knjiga.naziv,
                        Cena = knjiga.cena,
                        Kolicina = nk.kolicina
                    };
                    knjigaNarudzbenicas.Add(knjigaNarudzbenica);
                }
                AdminNarudzbina adminNarudzbina = new AdminNarudzbina
                {
                    IDNarudzbine = n.id_narudzbine,
                    Status = n.status,
                    DatumNarucivanja = n.datum,
                    Narucilac = narucilac,
                    Narudzbenice = knjigaNarudzbenicas
                };
                adminNarudzbine.Add(adminNarudzbina);
            }
            return adminNarudzbine;
        }

        public static AdminNarudzbina TransformSingle(Narudzbina narudzbina)
        {
            Korisnici narucilac = db.Korisnicis.Find(narudzbina.id_osoba);
            List<KnjigaNarudzbenica> knjigaNarudzbenicas = new List<KnjigaNarudzbenica>();

            List<Narudzbina_Knjiga> narudzbina_Knjigas = db.Narudzbina_Knjiga
                .Where(nk => nk.id_narudzbine == narudzbina.id_narudzbine)
                .ToList();
            foreach (var nk in narudzbina_Knjigas)
            {
                Knjige knjiga = db.Knjiges.Find(nk.id_knjige);
                KnjigaNarudzbenica knjigaNarudzbenica = new KnjigaNarudzbenica
                {
                    Naziv = knjiga.naziv,
                    Cena = knjiga.cena,
                    Kolicina = nk.kolicina
                };
                knjigaNarudzbenicas.Add(knjigaNarudzbenica);
            }
            AdminNarudzbina adminNarudzbina = new AdminNarudzbina
            {
                IDNarudzbine = narudzbina.id_narudzbine,
                Status = narudzbina.status,
                DatumNarucivanja = narudzbina.datum,
                Narucilac = narucilac,
                Narudzbenice = knjigaNarudzbenicas
            };

            return adminNarudzbina;
        }
    }
}