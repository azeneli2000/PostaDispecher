using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FotoRealTime
{
    public partial class index : System.Web.UI.Page
    {
        

     public static  List<Shofer> shoferet = new List<Shofer>();
        public static List<Klient> Klientet = new List<Klient>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)

            {
                Server.Transfer("~/Login.aspx");
            }
            else
            {
                usr.Text = "-1";
                //if (shoferet.Count == 0 )
                //{
                string connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Select * From Shoferet Where Aktiv = '" + "True" + "' AND Id_dispecher = " + Convert.ToInt64(Session["id"].ToString()) +" Order by EmriShoferi,MbiemriShoferi", conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    var itemF = new ListItem();
                    itemF.Text = "Zgjidh Shoferin";
                    itemF.Value = "-1";
                    DropDownList1.Items.Clear();
                    DropDownList1.Items.Add(itemF);
                    shoferet.Clear();
                    while (reader.Read())
                    {
                        Shofer sh = new Shofer();
                        sh.IdShoferi = Convert.ToInt64(reader["Id_shoferi"].ToString());
                        sh.EmriShoferi = reader["EmriShoferi"].ToString();
                        sh.MbiemriShoferi = reader["MbiemriShoferi"].ToString();
                        shoferet.Add(sh);
                        var item = new ListItem();
                        item.Text = reader["EmriShoferi"].ToString() + " " + reader["MbiemriShoferi"].ToString();
                        item.Value = reader["Id_shoferi"].ToString();

                        DropDownList1.Items.Add(item);
                    }
                    reader.Close();
                    SqlCommand cmdKlienti = new SqlCommand("Select * From Klienti Where Aktiv = '" + "True" + "' AND Id_dispecheri = " + Convert.ToInt64(Session["id"].ToString()), conn);
                    SqlDataReader readerKlienti = cmdKlienti.ExecuteReader();
                    Klientet.Clear();
                    while (readerKlienti.Read())
                    {
                        Klient k = new Klient();
                        k.id = Convert.ToInt64(readerKlienti["Id_klienti"].ToString());
                        Klientet.Add(k);
                    }
                    readerKlienti.Close();

                    conn.Close();

                }

                //}  
            }
        }
        //perdoret nga Ajaxi per te mbushur te dhenat e databases


            public static void  mbushShofera()
            {
           
         
        }






        [WebMethod]
        public static IEnumerable<Orders> GetData()
        {
            string datem = DateTime.Now.ToString();
            string datef = DateTime.Now.ToString();
            DateTime dateF;
            DateTime.TryParse(datef, out dateF);
            DateTime dataM;
            DateTime.TryParse(datef, out dataM);
            mbushShofera();
            List<Orders> ord = new List<Orders>();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
            {
                //  string sqlstring = "SELECT Orderid ,Adresa_marresi,Pickup,Driverid_pickup,Ora_pickup,Kthyer_mag,Driverid_kthyer,Ora_kthyer_mag,Magazinaid,Dorezuar,Driverid_dorezimi,Ora_dorezimi,Paguan,Klientiid,Llojiid,Zonaid,Cmimi,Valuta,Data,Pesha,Shenime,Telefon,Zonat.Emertimi As ZonatE,Llojet.Emertimi As LlojetE,Barcode,BarCodeImage,Emri_marresi FROM dbo.Orders,dbo.Llojet,dbo.Zonat Where Data >= '" + dateF.Year + "-" + dateF.Month + "-" + dateF.Day + "' AND Data <= '" + dataM.Year + "-" + dataM.Month + "-" + dataM.Day + " 23:59:59' AND Orders.Llojiid = Llojet.Id_lloji AND Orders.Zonaid = Zonat.Id_Zona";
                string sqlstring = "SELECT Orderid,Paguan,Cmimi,Vlera,Barcode,Orders.Shenime,Klientiid,Emri_marresi,Adresa_marresi,Data,Emri,Pesha,Telefon,Emertimi,Driverid_pickup,Pickup,Kthyer_mag,KonfirmimShoferi FROM dbo.Orders,dbo.Klienti,dbo.Zonat Where YEAR(Data) = " + DateTime.Now.Year + " AND MONTH(Data) = " + DateTime.Now.Month + " AND DAY(Data) = " + DateTime.Now.Day + " AND dbo.Orders.Klientiid = dbo.Klienti.Id_klienti AND dbo.Orders.Zonaid = dbo.Zonat.Id_Zona"; 
                SqlCommand cmd = new SqlCommand(sqlstring, connection);
                //sqldeperndency
                cmd.Notification = null;
                SqlDependency.Start(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
                SqlDependency dependency = new SqlDependency(cmd);
                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Orders o = new Orders();
                    o.idOrder = Convert.ToInt64(reader["Orderid"].ToString());
                    o.adresaMarresi = reader["Adresa_marresi"].ToString();
                    o.DataOraOrder = Convert.ToDateTime(reader["Data"].ToString());
                    o.Telefon = reader["Telefon"].ToString();
                    o.Pesha = Convert.ToDecimal(reader["Pesha"].ToString());
                    o.EmriMarresi = reader["Emri_marresi"].ToString();
                    o.EmroDerguesi = reader["Emri"].ToString();
                    o.ZonaEmertimi = reader["Emertimi"].ToString();

                    o.Cmimi = Convert.ToDecimal( reader["Cmimi"].ToString());
                    o.Vlera = Convert.ToDecimal(reader["Vlera"].ToString());
                    o.Barcode = reader["Barcode"].ToString();
                    o.Shenime = reader["Shenime"].ToString();
                    o.Paguan = reader["Paguan"].ToString();
                    Shofer sh = new Shofer();
                    if (reader["Driverid_pickup"].ToString() != "")
                    {
                        o.DriverIdPickUp = Convert.ToInt64(reader["Driverid_pickup"]);
                        sh = shoferet.SingleOrDefault(s => s.IdShoferi == Convert.ToInt64(o.DriverIdPickUp.ToString()));
                        if (sh != null)
                        {
                            o.EmriShoferi = sh.EmriShoferi;
                            o.MbiemriShoferi = sh.MbiemriShoferi;
                        }


                    }
                    o.pickUp = Convert.ToBoolean(reader["Pickup"]);
                    o.KthyerMag = Convert.ToBoolean(reader["Kthyer_mag"]);
                    o.PareKlienti = Convert.ToBoolean(reader["KonfirmimShoferi"]);
                    o.IdKlienti = Convert.ToInt64(reader["Klientiid"].ToString());

                    o.ora =o.DataOraOrder.ToString("dd/MM/yyyy") + "  " + o.DataOraOrder.ToShortTimeString();
                    bool has = Klientet.Any(kl => kl.id == o.IdKlienti);
                    if(has)                    
                     ord.Add(o);
                    
                }
                connection.Close();

                //string jsonString = JsonConvert.SerializeObject(ord);
                //return jsonString;
                return ord;
            }
            }


        [WebMethod]
        public static IEnumerable<Orders> GetData1(string dt)
        {
            string datem = DateTime.Now.ToString();
            string datef = DateTime.Now.ToString();
            DateTime dateF;
            DateTime.TryParse(datef, out dateF);
            DateTime dataM;
            DateTime.TryParse(dt, out dataM);
            mbushShofera();
            List<Orders> ord = new List<Orders>();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
            {
                //  string sqlstring = "SELECT Orderid ,Adresa_marresi,Pickup,Driverid_pickup,Ora_pickup,Kthyer_mag,Driverid_kthyer,Ora_kthyer_mag,Magazinaid,Dorezuar,Driverid_dorezimi,Ora_dorezimi,Paguan,Klientiid,Llojiid,Zonaid,Cmimi,Valuta,Data,Pesha,Shenime,Telefon,Zonat.Emertimi As ZonatE,Llojet.Emertimi As LlojetE,Barcode,BarCodeImage,Emri_marresi FROM dbo.Orders,dbo.Llojet,dbo.Zonat Where Data >= '" + dateF.Year + "-" + dateF.Month + "-" + dateF.Day + "' AND Data <= '" + dataM.Year + "-" + dataM.Month + "-" + dataM.Day + " 23:59:59' AND Orders.Llojiid = Llojet.Id_lloji AND Orders.Zonaid = Zonat.Id_Zona";
                string sqlstring = "SELECT Orderid,Emri_marresi,Adresa_marresi,Data,Emri,Pesha,Telefon,Emertimi,Driverid_pickup,Pickup FROM dbo.Orders,dbo.Klienti,dbo.Zonat Where YEAR(Data) = " + dataM.Year + " AND MONTH(Data) = " + dataM.Month + " AND DAY(Data) = " + dataM.Day + " AND dbo.Orders.Klientiid = dbo.Klienti.Id_klienti AND dbo.Orders.Zonaid = dbo.Zonat.Id_Zona";
                SqlCommand cmd = new SqlCommand(sqlstring, connection);
                //sqldeperndency
                cmd.Notification = null;
                SqlDependency.Start(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
                SqlDependency dependency = new SqlDependency(cmd);
                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Orders o = new Orders();
                    o.idOrder = Convert.ToInt64(reader["Orderid"].ToString());
                    o.adresaMarresi = reader["Adresa_marresi"].ToString();
                    o.DataOraOrder = Convert.ToDateTime(reader["Data"].ToString());
                    o.Telefon = reader["Telefon"].ToString();
                    o.Pesha = Convert.ToDecimal(reader["Pesha"].ToString());
                    o.EmriMarresi = reader["Emri_marresi"].ToString();
                    o.EmroDerguesi = reader["Emri"].ToString();
                    o.ZonaEmertimi = reader["Emertimi"].ToString();
                    Shofer sh = new Shofer();
                    if (reader["Driverid_pickup"].ToString() != "")
                    {
                        o.DriverIdPickUp = Convert.ToInt64(reader["Driverid_pickup"]);
                        sh = shoferet.SingleOrDefault(s => s.IdShoferi == Convert.ToInt64(o.DriverIdPickUp.ToString()));
                        if (sh != null)
                        {
                            o.EmriShoferi = sh.EmriShoferi;
                            o.MbiemriShoferi = sh.MbiemriShoferi;
                        }


                    }
                    o.pickUp = Convert.ToBoolean(reader["Pickup"]);
                  
                    ord.Add(o);

                }
                connection.Close();

                //string jsonString = JsonConvert.SerializeObject(ord);
                //return jsonString;
                return ord;
            }
        }





        [WebMethod]
        public static  void Update(int id,int driver)
        {
            string emriKlienti = "";
            //ben modifikimin e shoferit
            string connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmdSelect = new SqlCommand("select Emri from Orders, Klienti where Orders.Klientiid = Klienti.Id_klienti and orders.Orderid = " + id,conn);
               
                
                SqlCommand cmd = new SqlCommand("Update Orders set Driverid_pickup = " + driver + " where OrderID = " + id, conn);
                cmd.Notification = null;
               
                conn.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmdSelect.ExecuteReader();
                while (reader.Read())
                {
                  emriKlienti=  reader["Emri"].ToString();
                }

             

                conn.Close();
            }
            var caller5 = new restSharpCaller("http://webapi320171012080657.azurewebsites.net/api/Notifications/?message=" + "Porosi e re : " + "  " + emriKlienti + "&perdoruesi=" + driver.ToString());
            caller5.get_notification();
        }
         [WebMethod(EnableSession = true)]
        public static  IEnumerable<ShoferiDP>  getShoferet()
        {

            Int64 dispecherId =Convert.ToInt64( HttpContext.Current.Session["id"].ToString());
            string connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
               // SqlCommand cmd = new SqlCommand("SELECT distinct  ( SELECT  COUNT(Driverid_pickup)  FROM Orders WHERE (Pickup = 'True') ) AS Pickup,(SELECT COUNT(Driverid_dorezimi)  FROM Orders WHERE (Dorezuar = 'True')  ) AS Deliver, EmriShoferi + ' ' + MbiemriShoferi As Emri  from Shoferet,Dispecher,Orders where  Orders.Driverid_pickup = Shoferet.Id_shoferi  and Shoferet.Id_dispecher = " + dispecherId + "and Shoferet.Id_dispecher = Dispecher.Id_dispecheri And YEAR(Data) = " + DateTime.Now.Year + " And MONTH(Data) = " + DateTime.Now.Month + " And DAY(Data) = " + DateTime.Now.Day, conn);
                SqlCommand cmd = new SqlCommand("SELECT distinct  ( SELECT  COUNT(Driverid_pickup)  FROM Orders WHERE (Pickup = 'True' and  Orders.Driverid_pickup = Shoferet.Id_shoferi And YEAR(Data) = 2018 And MONTH(Data) = 6 And DAY(Data) = 5) ) AS Pickup,(SELECT COUNT(Driverid_dorezimi)  FROM Orders WHERE (Dorezuar = 'True' and  Orders.Driverid_dorezimi = Shoferet.Id_shoferi And YEAR(Data) = 2018 And MONTH(Data) = 6 And DAY(Data) = 5))   AS Deliver, EmriShoferi + ' ' + MbiemriShoferi As Emri  from Shoferet,Dispecher,Orders where  Orders.Driverid_pickup = Shoferet.Id_shoferi  and Shoferet.Id_dispecher = " + dispecherId + "and Shoferet.Id_dispecher = Dispecher.Id_dispecheri And YEAR(Data) = 2018 And MONTH(Data) = 6 And DAY(Data) = 5", conn);

                cmd.Notification = null;
                
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<ShoferiDP> shof = new List<ShoferiDP>();
                while (reader.Read())
                {
                    ShoferiDP dp = new ShoferiDP();
                    dp.emri = reader["Emri"].ToString();
                    dp.dorezuar = Convert.ToInt16(reader["Deliver"].ToString());
                    dp.pickup = Convert.ToInt16(reader["Pickup"].ToString());
                    shof.Add(dp);
                }

                    conn.Close();
                return shof;
            }
        }

        [WebMethod]
        public static void Update11(int id)
        {

            //ben modifikimin e shoferit
            string connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Update Orders set Driverid_pickup = NULL where OrderID = " + id, conn);
                cmd.Notification = null;
                //SqlDependency.Start(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
                //SqlDependency dependency = new SqlDependency(cmd);
                //dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        //kur ndryshon database dergo mesazh gjithe klienteve
        static int i = 0;
        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
           
         //if (e.Type == SqlNotificationType.Change)
                MyHubCel.Show();
            i++;
        }


    }
}