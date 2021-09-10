
namespace Domain
{
    public class Request
    {
        public tren train { get; set; }

        public int RezervasonYapılacakKisiSayisi { get; set; }

        public bool KisilerFarklıVagonlaraYerlesir { get; set; }
    }

    public class tren
    {
        public string Ad { get; set; }

        public Vagon[] Vagonlar { get; set; }

    }

    public class Vagon
    {
        public string Ad { get; set; }

        public int Kapasite { get; set; }

        public int DoluKoltuk { get; set; }
    }
}