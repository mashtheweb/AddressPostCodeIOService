using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AddressPostCodeIOService.Configuration;
using AddressPostcodeIOService.Context;
using AddressPostcodeIOService.Models;

namespace AddressPostCodeIOService.Services
{
    public class AddressServices
    {
        // private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IConfiguration Configuration { get; set; }

        static string connectionString = Microsoft
                                        .Extensions
                                        .Configuration
                                        .ConfigurationExtensions
                                        .GetConnectionString(Configuration, "DBConnection");

        static string postCodeServiceString = Microsoft
                                .Extensions
                                .Configuration
                                .ConfigurationExtensions
                                .GetConnectionString(Configuration, "AddressPostcodeServiceURL");

        public static string GetAddress(int id)
        {
            using (AddressDB db = new AddressDB())
            {
                if (db.Address.Where(t => t.Id == id).Count() > 0)
                    return JsonConvert.SerializeObject(db.Address.First(t => t.Id == id));
                else
                    return "{}";
            }
        }

        public static void AddAddress(Address addressObject)
        {
            using (AddressDB db = new AddressDB())
            {
                db.Address.Add(addressObject);
                db.SaveChanges();
            }
        }

        public static void UpdateAddress(Address addressObject)
        {
            using (AddressDB db = new AddressDB())
            {
                db.Address.Update(addressObject);
                db.SaveChanges();
            }
        }

        public static void RemoveAddress(int id)
        {
            using (AddressDB db = new AddressDB())
            {
                if (db.Address.Where(t => t.Id == id).Count() > 0) // Check if element exists
                    db.Address.Remove(db.Address.First(t => t.Id == id));
                db.SaveChanges();
            }
        }
    }
}
