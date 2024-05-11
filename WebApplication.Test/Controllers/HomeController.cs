using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;
using WebApplication.Test.Models;

namespace WebApplication.Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<State> model = new();
            try
            {
                // Define the API endpoint URL
                string apiUrl = "https://localhost:44324/api/shipping";

                // Create a WebRequest
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "GET";

                // Get the response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    
                        // Read the response content
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string responseBody = reader.ReadToEnd();
                        model = JsonConvert.DeserializeObject<List<State>>(responseBody);
                        }
                    
                }
            }
            catch (Exception ex)
            {
                
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult GetPrice(int source,int destination,int weight)
        {
            PostPriceResponseApiModel res = new PostPriceResponseApiModel();
            try
            {
                // Define the API endpoint URL
                string apiUrl = "https://localhost:44324/api/shipping";

                // Create an HttpWebRequest for POST
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "POST";
                request.ContentType = "application/json"; // Set content type to JSON

                // Create your JSON data (replace with your actual data)
                string jsonData = "{\"ApiCode\": \"6b4f1695-cdd4-46cc-8d45-36ce46c76e18\"," +
                    " \"DestinationCityId\":" + $" \"{destination}\"" +
                    ", \"SourceCityId\":"+$" \"{source}\", \"Weight\": "+$" \"{weight}\""+"}";

                // Convert JSON string to bytes
                byte[] dataBytes = Encoding.UTF8.GetBytes(jsonData);

                // Write data to the request stream
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(dataBytes, 0, dataBytes.Length);
                }

                // Get the response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // Read the response content
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string responseBody = reader.ReadToEnd();
                             res = JsonConvert.DeserializeObject<PostPriceResponseApiModel>(responseBody);
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(JsonConvert.SerializeObject(res));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
