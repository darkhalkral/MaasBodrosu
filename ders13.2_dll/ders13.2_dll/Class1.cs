using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace ders13._2_dll
{
    public class maashesapla
    {
        //Çok fazla detaya girmememin sebebi tahtaya yazılmış olan tabloda koşul olarak sadece evli/bekar, çocuk sayısı ve engellilik durumu belirtilmişti.
        //Göstergeleri Öğretmen'e ve 1 hizmet yıllığına göre aldım.
        public float kesintiler(int brut,string medeni,int cocuk,int engel)
        {
            float kesinti;
            float maaskatsayi = 0.333603f;
            float tabankatsayi = 0.221532f;
            float yankatsayi = 0.10579f;
            int yanpuanı = 750;
            int kidemaylikgos = 20;
            int tabanaylikgos = 1000;
            int gosterge = 1320;
            int ekgosterge = 3600;
            float gelirvergisi=0;
            switch (engel)
            {
                case 0:
                    gelirvergisi = ((gosterge * maaskatsayi) + (tabanaylikgos * tabankatsayi) + (ekgosterge * maaskatsayi) + (1 * kidemaylikgos + maaskatsayi) + (yanpuanı * yankatsayi)) - ((brut * 0.09f) + (brut * 0.05f) + 0 + 0) * 0.2f;
                    break;
                case 1:
                    gelirvergisi = ((gosterge * maaskatsayi) + (tabanaylikgos * tabankatsayi) + (ekgosterge * maaskatsayi) + (1 * kidemaylikgos + maaskatsayi) + (yanpuanı * yankatsayi)) - ((brut * 0.09f) + (brut * 0.05f) + 0 + 500) * 0.2f;
                    break;
                case 2:
                    gelirvergisi = ((gosterge * maaskatsayi) + (tabanaylikgos * tabankatsayi) + (ekgosterge * maaskatsayi) + (1 * kidemaylikgos + maaskatsayi) + (yanpuanı * yankatsayi)) - ((brut * 0.09f) + (brut * 0.05f) + 0 + 1170) * 0.2f;
                    break;
                case 3:
                    gelirvergisi = ((gosterge * maaskatsayi) + (tabanaylikgos * tabankatsayi) + (ekgosterge * maaskatsayi) + (1 * kidemaylikgos + maaskatsayi) + (yanpuanı * yankatsayi)) - ((brut * 0.09f) + (brut * 0.05f) + 0 + 2000) * 0.2f;
                    break;
            }
            float damgavergisi = brut * 0.00759f;
            float aileyardim = 0;
            if (medeni == "Evli")
                aileyardim = 1500 * maaskatsayi;
            float cocukyardim = cocuk * 250 * maaskatsayi;
            kesinti = gelirvergisi + damgavergisi - (aileyardim + cocukyardim);
            return kesinti;
        }

    }
}
