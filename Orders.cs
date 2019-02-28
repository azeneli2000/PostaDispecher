using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FotoRealTime
{
    public class Orders
    {
       
        public Int64 idOrder { get; set; }

       
        public string adresaMarresi { get; set; }

        //************************PICKUP

        public Boolean pickUp { get; set; }

        public Int64? DriverIdPickUp { get; set; }

        public DateTime? OraPickUp { get; set; }

        //************************KTHYER
        public Boolean KthyerMag { get; set; }

        public Int64? IdMagazina { get; set; }

        public Int64? DriverIdKthyerMag { get; set; }

        public DateTime? OraKthyerMag { get; set; }

        //************************DOREZUAR
      
        public Boolean Dorzuar { get; set; }

        public Boolean PareKlienti { get; set; }
        public Int64? DriverIdDorezimi { get; set; }
      
        public DateTime? OraDorezimi { get; set; }


        //************************TE DHENAT E DERGESES
    
        public string Paguan { get; set; }
        public Int64 IdKlienti { get; set; }       
        public Int64 IdLloji { get; set; }      
        public Int64 IdZona { get; set; }     
        public decimal Cmimi { get; set; }
        public decimal Vlera { get; set; }
        public string Valua { get; set; }       
        public DateTime DataOraOrder { get; set; }      
        public decimal Pesha { get; set; }      
        public string Shenime { get; set; }      
        public string Telefon { get; set; }      
        public string ZonaEmertimi { get; set; }        
        public string LlojiEmertimi { get; set; }
        public byte[] BarcodeImage { get; set; }
        public string Barcode { get; set; }
        public string ImageUrl { get; set; }     
        public string EmriMarresi { get; set; }
        public string EmroDerguesi { get; set; }
        public string EmriShoferi { get; set; }
        public string MbiemriShoferi { get; set; }
        public string ora { get; set; }

    }
}