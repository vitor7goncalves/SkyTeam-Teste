using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SKYTEAM.Models;

namespace SKYTEAM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ViewResult Index()
        {
            return View();
        }
        //[HttpPost]
        public ViewResult Login(UserData user)
        {
            return View();
        }

        public ViewResult UserArea()
        {
            ViewBag.token = 1700;
            return View();
        }

       public ViewResult GetLogin()
        {
            UserData user = new UserData();
            
            WebRequest request = WebRequest.Create("https://testapi.tdesk.com.br/api/V1/PortalLogin");
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            string auth = user.ToString();
            byte[] byteArray = Encoding.UTF8.GetBytes(auth);
            request.ContentLength = byteArray.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
            }
                response.Close();
            return View();
        }
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

