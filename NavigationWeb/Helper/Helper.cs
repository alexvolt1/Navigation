using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NavigationWeb.Utility;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace NavigationWeb.Helper
{
    public class ApiHelper
    {
        public HttpClient Initial()
        {
            //var Client = new HttpClient();

            var Client = new HttpClient(new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                MaxConnectionsPerServer = int.MaxValue,
                UseCookies = false,
                ServerCertificateCustomValidationCallback = ValidateLocalhostCertificate
            })
            {
                BaseAddress = new Uri(SD.ClientBaseAddress)
            };
            return Client;
        }

        private static bool ValidateLocalhostCertificate(HttpRequestMessage arg1, X509Certificate2 arg2, X509Chain arg3, SslPolicyErrors arg4)
        {
            if (arg1.RequestUri.Host == "127.0.0.1")
            {
                return true;
            }
            else
            {
                // default validation
                //temp hack
                return true;
            }
        }


    }
}
