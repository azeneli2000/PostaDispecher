using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FotoRealTime
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpContext CurrContext = HttpContext.Current;
            string connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Dispecher where PerdoruesiDispecheri = '" + TextBox1.Text + "' AND PasswordDispecheri = '" + TextBox2.Text + "'", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                string perd= "";
                string pass = "";
                Int64 id = 0;
                while (reader.Read())
                {
                    perd = reader["PerdoruesiDispecheri"].ToString(); ;
                    pass = reader["PasswordDispecheri"].ToString();
                    id = Convert.ToInt64(reader["Id_dispecheri"]);
                }
                if (reader.HasRows)
                    {
                    //krijo sessionin 
                    CurrContext.Items.Add("user", perd);
                    CurrContext.Items.Add("pass", pass);
                    CurrContext.Items.Add("id", id);
                    Session["user"] = perd;
                    Session["pass"] = pass;
                    Session["id"] = id;


                    //redirect te index
                    Server.Transfer("~/index.aspx", true);
                }
            }
           
        }
    }
}
