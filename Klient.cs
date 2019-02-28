using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FotoRealTime
{
    public class Klient
    {
        public Int64 id { get; set; }

     

        public String emri { get; set; }



       
        public string adresa { get; set; }

        //public Int64 idSkonto { get; set; }

  
        public String perdoruesi { get; set; }

       
        public string password { get; set; }

     
        public string rePasssword { get; set; }

    
        public bool agjensi { get; set; }

     
        public bool aktiv { get; set; }
     
        public string shenime { get; set; }

      

        public String emerSubjekti { get; set; }

       

        public String personKontakti { get; set; }

      

        public String llogBankare { get; set; }
    

        public String email { get; set; }
      

        public String emerMagazina { get; set; }

       

        public String emerDispecheri { get; set; }
       
        public Int64 idMagazina { get; set; }
       
        public Int64 idDispecheri { get; set; }
       
        public decimal Detyrimi { get; set; }
    }
}