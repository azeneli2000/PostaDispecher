using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;

namespace FotoRealTime
{
    public class restSharpCaller
    {
        public RestClient client;
        public restSharpCaller(string baseUrl)
        {
            client = new RestClient(baseUrl);
        }
        public void get_notification()
        {
            var request = new RestRequest("", Method.GET);
            var response = client.Execute(request);
        }

    }
}