using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AddressPostCodeIOService.Configuration;
using AddressPostCodeIOService.Services;

namespace AddressPostcodeIOService.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        public static IConfiguration Configuration { get; set; }

        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static string conString = Microsoft
            .Extensions
            .Configuration
            .ConfigurationExtensions
            .GetConnectionString(Configuration, "ImporterDatabase");

        public IActionResult Index()
        {
            return View();
        }

        // GET api/Address/5
        [HttpGet("{id:int}")]
        public string Get(int id)
        {
            return AddressServices.GetAddress(id);
        }

        // GET api/Address/SE1 0RB
        [HttpGet("{postCode}")]
        public string Get(string postCode)
        {
            var webServiceResult = GetPostCodeResponse(postCode);
            return webServiceResult;
        }

        // POST api/Address
        [HttpPost]
        public void Post([FromBody] JObject value)
        {
            Models.Address posted = value.ToObject<Models.Address>();
            AddressServices.AddAddress(posted);
        }

        // PUT api/Address/5
        [HttpPut]
        public void Put(int id, [FromBody] JObject value)
        {
            Models.Address posted = value.ToObject<Models.Address>();
            posted.Id = id; // Ensure an id is attached
            AddressServices.UpdateAddress(posted);
        }

        // DELETE api/Address/5

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            AddressServices.RemoveAddress(id);
        }

        // GET api/Address/SE1 0RB
        [HttpGet("{postCode}, {checkWebService}")]
        public async Task<string> Get(string postCode, bool checkWebService)
        {
            if (checkWebService)
            {
                var webServiceResult = await GetPostCodeResponseAsTask(postCode);
                return webServiceResult;
            }
            else
            {
                using (AddressPostcodeIOService.Context.AddressDB db = new AddressPostcodeIOService.Context.AddressDB())
                {
                    return JsonConvert.SerializeObject(db.Address.Where(t => t.Postcode == postCode));
                }
            }
        }

        //// GET api/Address/SE1 0RB
        //[HttpGet("{postCode}")]
        //public async Task<string> Get(string postCode, bool setAsTask)
        //{
        //    var webServiceResult = "";
        //    if (setAsTask)
        //    {
        //        webServiceResult = await GetPostCodeResponseAsTask(postCode);
        //    }
        //    else
        //   {
        //        webServiceResult = GetPostCodeResponse(postCode);
        //    }
        //    return webServiceResult;
        //}

        private async Task<string> GetPostCodeResponseAsTask(string postCode)
        {
            //string serviceString = Microsoft
            //    .Extensions
            //    .Configuration
            //    .ConfigurationExtensions
            //    .GetConnectionString(Configuration, "AddressPostcodeServiceURL");

            //string apiKeyString = Microsoft
            //    .Extensions
            //    .Configuration
            //    .ConfigurationExtensions
            //    .GetConnectionString(Configuration, "AddressPostcodeServiceAPIKey");

            // string serviceString = AddressMicroservicesConfiguration.Current.AddressPostcodeServiceString;

            // string apiKeyString = AddressMicroservicesConfiguration.Current.AddressPostcodeServiceAPIKeyString;

            string serviceString = Startup.AddressPostcodeServiceString;

            string apiKeyString = Startup.AddressPostcodeServiceAPIKeyString;

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(serviceString + "postcodes/" + postCode + "?api_key=" + apiKeyString);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        private string GetPostCodeResponse(string postCode)
        {

            string serviceString = Startup.AddressPostcodeServiceString;

            string apiKeyString = Startup.AddressPostcodeServiceAPIKeyString;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceString + "postcodes/" + postCode + "?api_key=" + apiKeyString);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }

        }
    }
}