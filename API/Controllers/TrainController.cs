using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainController : ControllerBase
    {

        [HttpPost]

        public Response response(Request intput)
        {

            Response res = new Response();
            res.yerlesim = new List<YerlesimAyrıntı>();

            var doluOlmayanVagonlar = intput.train.Vagonlar.Where(x => x.Kapasite * 70 / 100 > x.DoluKoltuk).ToList();

            if (doluOlmayanVagonlar != null)
            {
                Vagon yerlesimAynı = doluOlmayanVagonlar.Where(x => x.Kapasite * 70 / 100 - x.DoluKoltuk > intput.RezervasonYapılacakKisiSayisi).FirstOrDefault();

                if (yerlesimAynı != null)
                {
                    res.RezervasonYapılır = true;
                    res.yerlesim.Add(new YerlesimAyrıntı
                    {
                        VagonAdı = yerlesimAynı.Ad,
                        KişiSayısı = intput.RezervasonYapılacakKisiSayisi
                    });

                    return res;
                }

                if (intput.KisilerFarklıVagonlaraYerlesir && yerlesimAynı == null)
                {
                    int yerlesen = 0;
                    foreach (var vagon in doluOlmayanVagonlar)
                    {
                        int yer = vagon.Kapasite * 7 / 10 - vagon.DoluKoltuk;

                        if (yer > intput.RezervasonYapılacakKisiSayisi)
                        {
                            res.yerlesim.Add(new YerlesimAyrıntı
                            {
                                VagonAdı = vagon.Ad,
                                KişiSayısı = intput.RezervasonYapılacakKisiSayisi
                            });

                            return res;
                        }
                        else
                        {
                            // int vagonkoltuksayısı =
                            for (int i = 1; i < intput.RezervasonYapılacakKisiSayisi; i++)
                            {
                                vagon.DoluKoltuk++;
                                int yer1 = vagon.Kapasite * 7 / 10 - vagon.DoluKoltuk;
                                int yer2 = yer - yer1;
                                yerlesen++;

                                if (yer2 == 0)
                                {
                                    res.yerlesim.Add(new YerlesimAyrıntı
                                    {
                                        VagonAdı = vagon.Ad,
                                        KişiSayısı = yerlesen
                                    });

                                    break;
                                }

                            }

                            intput.RezervasonYapılacakKisiSayisi -= yerlesen;
                            yerlesen = 0;

                        }

                    }
                }
            }
            else
            {
                res.RezervasonYapılır = false;
                res.yerlesim = new List<YerlesimAyrıntı>();
                return res;
            }



            return res;
        }





    }

}