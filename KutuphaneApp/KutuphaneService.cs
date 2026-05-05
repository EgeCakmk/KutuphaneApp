using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace KutuphaneApp
{
    public static class KutuphaneService
    {
        static KitapDal dal = new KitapDal();

        //methods
        public static void KitapEkle()
        {
            Console.WriteLine("Secilen islem: Kitap Ekle");
            Console.Write("Eklemek istediginiz kitabin adini girin: ");
            string kitapAdi = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(kitapAdi))
            {
                Console.WriteLine("Gecersiz kitap adi. Bu bolum bos birakilamaz.\n");
                return;
            }
            dal.KitapEkle(kitapAdi);
            Console.WriteLine("Kitap basariyla eklendi.\n");
        }
        public static void KitapListele()
        {
            Console.WriteLine("Secilen islem: Kitaplari Listele");
            dal.Listele();
        }
        public static void KitapSil()
        {
            Console.WriteLine("Secilen islem: Kitap Sil");
            Console.Write("Silmek istediginiz kitabin id numarasini girin: ");
            var idSil = Console.ReadLine();
            if (int.TryParse(idSil, out int secilenId))
            {
                dal.KitapSil(secilenId);
                Console.WriteLine("Kitap basariyla silindi.\n");
                return;
            }
            Console.WriteLine("Gecersiz id.");
        }
        public static void KitapAra(string kelime)
        {
            dal.KitapAra(kelime);
        }
        public static string SecimAl()
        {
            string kullaniciSecimi = Console.ReadLine();
            return kullaniciSecimi;
        }
        public static void MenuGoster()
        {
            Console.WriteLine("Lutfen asagıdaki seceneklerden birisini secin: ");
            Console.WriteLine("1.Kitap Ekle");
            Console.WriteLine("2.Kitaplari Listele");
            Console.WriteLine("3.Kitap Sil");
            Console.WriteLine("4.Cikis Yap");
        }
        public static void HataliSecim()
        {
            Console.WriteLine("Gecersiz secim. Lutfen 1-4 arasinda bir sayi girin.\n");
        }
        public static bool CikisYap()
        {
            Console.WriteLine("Cikis yapiliyor... Hoscakalin.\n");
            Environment.Exit(0);
            return true;
        }
    }
}
