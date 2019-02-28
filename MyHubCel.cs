using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Data.SqlClient;

namespace FotoRealTime
{
    public class MyHubCel : Hub
    {
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MyHubCel>();
            context.Clients.All.displayStatus();//therret funksionin displayStatus te gjithe klientet
        }


        //public void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        //{
        //    MyHubCel.Show();

        //}
    }
}