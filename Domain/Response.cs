using System.Collections.Generic;

namespace Domain
{
    public class Response
    {
        public bool RezervasonYapılır { get; set; }

        public List<YerlesimAyrıntı> yerlesim { get; set; }
    }

    public class YerlesimAyrıntı
    {
        public string VagonAdı { get; set; }

        public int KişiSayısı { get; set; }
    }
}