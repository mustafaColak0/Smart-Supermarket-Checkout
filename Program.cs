using System;
using System.Collections.Generic;

namespace Akilli_Supermarket_Kasasi
{
    internal class Program
    {
        //  Etiket Basma işçisi
        static void EtiketBas(string urunAdi, double fiyat)
        {
            Console.WriteLine("****Urun Adı**** -> Fiyat : " + urunAdi + " " + fiyat + " TL");
        }

        //  KDV hesaplayıcı
        static double kdvHesapla(double anafiyat)
        {
            double sonuc = anafiyat * 1.10;
            return sonuc;
        }

        //  Son Tüketim Tarihi hesaplayıcı
        static int SttHesapla(int uretimYili, int rafOmru)
        {
            int stt = uretimYili + rafOmru;
            return stt;
        }

        // Müşteri sınıfı
        class Musteri
        {
            public string Ad;
            public double Bakiye;

            public Musteri(string gelenAd, double gelenBakiye)
            {
                Ad = gelenAd;
                Bakiye = gelenBakiye;
            }

            public void OdemeYap(double toplamTutar)
            {
                if (Bakiye >= toplamTutar)
                {
                    Bakiye = Bakiye - toplamTutar;
                    Console.WriteLine("Ödeme Başarılı! kalan bakiye:" + Bakiye);
                }
                else
                {
                    Console.WriteLine("Maalesef bakiyeniz yetersiz! Eksik Miktar: " + (toplamTutar - Bakiye) + "TL");
                }
            }
        }

        static void Main(string[] args)
        {
            // Müşteri oluşturma
            Musteri musteri1 = new Musteri("Mustafa", 100);

            // Ürün listesi
            Dictionary<string, double> urunler = new Dictionary<string, double>();
            urunler.Add("Elma", 20);
            urunler.Add("Kiraz", 35);
            urunler.Add("Muz", 25);

            Console.WriteLine("================================");
            Console.WriteLine("Dükkan açıldı.");
            Console.WriteLine();

            // Etiket Basma
            EtiketBas("Elma", 20);
            EtiketBas("Kiraz", 35);
            EtiketBas("Muz", 25);

            Console.WriteLine();

            double toplamBorc = 0;
           
            //  alışveriş 
            while (true)
            {
                Console.WriteLine("lütfen alacağınız ürünü girin(ödemeye geçmek için 'Bitir' yazın)");
                string urun = Console.ReadLine();

                // Yazım Düzeltici(Elma, elma, ELMA fark etmesin diye)
                urun = urun.Substring(0, 1).ToUpper() + urun.Substring(1).ToLower();

                if (urun == "Bitir")
                {
                    break;
                }
                else
                {
                    if (urunler.TryGetValue(urun, out double fiyat))
                    {
                        Console.WriteLine("istenen ürün bulundu");
                        
                        toplamBorc = toplamBorc + fiyat;
                        Console.WriteLine();

                        Console.WriteLine("Ara toplam:" + toplamBorc + "TL");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Maalesef bu ürün mağazımızda yok!");
                    }
                }
            }

            // Hesap kitap vakti
            Console.WriteLine("Toplam Borcunuz: " + toplamBorc + " TL");
            Console.WriteLine();

            // KDV'yi Hesaplayıcı
            double kdvliTutar = kdvHesapla(toplamBorc);
            Console.WriteLine("KDV (%10) dahil toplam: " + kdvliTutar);
            Console.WriteLine();


            // Ödeme Aşaması
            musteri1.OdemeYap(kdvliTutar);
            Console.WriteLine("--------------------------------");


            // Tüketim Tarihi
            try
            {
                Console.WriteLine("Ürünün üretim yılını giriniz:");
                int uretim = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                Console.WriteLine("Raf ömrünü (yıl) giriniz:");
                int omur = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                int sonYil = SttHesapla(uretim, omur);
                Console.WriteLine("Dikkat: Bu ürünün son tüketim yılı " + sonYil + "!");

                if (sonYil < 2026)
                {
                    Console.WriteLine("EYVAH! Bu ürünün tarihi geçmiş, satılamaz!");
                }
                else
                {
                    Console.WriteLine("Ürün taze, rafa koyabilirsiniz.");
                }
            }
            catch
            {
                Console.WriteLine("Hata: Lütfen geçerli bir sayı giriniz!");
            }

            Console.WriteLine("İyi günler dileriz...");
            Console.WriteLine("================================");

            // Çıktıyı görebilmek için bekletme
            Console.ReadLine();
        }
    }
}