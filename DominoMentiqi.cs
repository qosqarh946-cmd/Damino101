using System;

namespace Domino_101_.az
{
    public class DominoMentiqi
    {
        public int BaslangicXali = 101;
        public int SilinmeXali = 13;
        public string IlkDas = "1-1";

        public bool XalSilinsinmi(int cariXal)
        {
            return cariXal > 0 && cariXal % SilinmeXali == 0;
        }

        public string OyunuBaslat()
        {
            return "Oyun bir-qoşa (1-1) ilə başlayır! Uğurlar!";
        }
    }
}
