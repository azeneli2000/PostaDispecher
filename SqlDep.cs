using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FotoRealTime
{
    public class SqlDep 
    {
        private const  string CONNECTION_STRING = "Data Source = DESKTOP - H41N2OC\\SQLEXPRESS;Initial Catalog = studenti; Integrated Security = True";
        private const string DATABASE_NAME = "studenti";
        private const string TABLE_NAME = "Orders";
        private const string SCHEMA_NAME = "dbo";

        private SqlDependencyEx sqlDependency = new SqlDependencyEx(
                                                  CONNECTION_STRING,
                                                  DATABASE_NAME,
                                                  TABLE_NAME,
                                                  SCHEMA_NAME);

        // Something else is here....         

        public void RegisterNotification()
        {
            sqlDependency.TableChanged += OnDataChange;
            sqlDependency.Start();
        }

        private void UnregisterNotification()
        {
            sqlDependency.Stop();
            sqlDependency.TableChanged -= OnDataChange;
        }

        private void OnDataChange(object sender, SqlDependencyEx.TableChangedEventArgs e)
        {
           // MyHubCel.Show();
            // TODO: do stuff here
            // If you want to monitor changes of `ActivityDate` field only
            // you have to use the `TableChangedEventArgs` object.
            // It has the `NotificationType` field and the `Data` field.
            // The `Data` field contains information about the changes being made,
            // so you can filter it.
        }

        private void Dispose()
        {
            // Don't forget to clean up the resources!
            UnregisterNotification();
        }
    }
}