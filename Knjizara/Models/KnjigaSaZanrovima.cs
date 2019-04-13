using Knjizara.Models;
using System.Collections.Generic;
using System.Linq;

namespace Knjizara.ViewModels
{
    public class KnjigaSaZanrovima
    {
        private static KnjizaraDBEntities db = new KnjizaraDBEntities();

        public Knjige knjiga { get; set; }
        public List<Zanr> zanrovi { get; set; }

        // pomocni metod koji za jednu knjiga nadje i vrati tu knjiga sa zanrovima
        public static KnjigaSaZanrovima TransformSingle(Knjige Knjiga)
        {
            List<Knjige_Zanr> knjigeZanrovi = db.Knjige_Zanr
                    .Where(kz => kz.id_knjige == Knjiga.id_knjige)
                    .ToList();

            List<Zanr> zanrovi = new List<Zanr>();
            foreach (Knjige_Zanr kz in knjigeZanrovi)
            {
                Zanr zanr = db.Zanrs.Find(kz.id_zanr);
                zanrovi.Add(zanr);
            }

            KnjigaSaZanrovima knjigaSaZanrovima = new KnjigaSaZanrovima
            {
                knjiga = Knjiga,
                zanrovi = zanrovi
            };
            return knjigaSaZanrovima;
        }

        // pomocni metod koji za listu knjiga nadje i vrati listu knjiga sa zanrovima
        public static List<KnjigaSaZanrovima> TransformList(List<Knjige> Knjige)
        {
            List<KnjigaSaZanrovima> knjigeSaZanrovima = new List<KnjigaSaZanrovima>();

            foreach (Knjige knjiga in Knjige)
            {
                List<Knjige_Zanr> knjigeZanrovi = db.Knjige_Zanr
                    .Where(kz => kz.id_knjige == knjiga.id_knjige)
                    .ToList();

                List<Zanr> zanrovi = new List<Zanr>();
                foreach (Knjige_Zanr kz in knjigeZanrovi)
                {
                    Zanr zanr = db.Zanrs.Find(kz.id_zanr);
                    zanrovi.Add(zanr);
                }

                KnjigaSaZanrovima knjigaSaZanrovima = new KnjigaSaZanrovima
                {
                    knjiga = knjiga,
                    zanrovi = zanrovi
                };

                knjigeSaZanrovima.Add(knjigaSaZanrovima);
            }

            return knjigeSaZanrovima;
        }

        // isto kao prethodno samo sa fitriranjem po odabranom zanru
        public static List<KnjigaSaZanrovima> TransformList(List<Knjige> Knjige, Zanr odabraniZanr)
        {
            List<KnjigaSaZanrovima> knjigeSaZanrovima = new List<KnjigaSaZanrovima>();

            foreach (Knjige knjiga in Knjige)
            {
                List<Knjige_Zanr> knjigeZanrovi = db.Knjige_Zanr
                    .Where(kz => kz.id_knjige == knjiga.id_knjige)
                    .ToList();

                List<Zanr> zanrovi = new List<Zanr>();
                foreach (Knjige_Zanr kz in knjigeZanrovi)
                {
                    Zanr zanr = db.Zanrs.Find(kz.id_zanr);
                    zanrovi.Add(zanr);
                }

                KnjigaSaZanrovima knjigaSaZanrovima = new KnjigaSaZanrovima
                {
                    knjiga = knjiga,
                    zanrovi = zanrovi
                };

                if (zanrovi.Exists(z => z.id_zanr == odabraniZanr.id_zanr)) knjigeSaZanrovima.Add(knjigaSaZanrovima);
            }

            return knjigeSaZanrovima;
        }
    }
}