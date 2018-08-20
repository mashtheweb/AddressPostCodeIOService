using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressPostCodeIOService.Configuration
{
    public class AddressMicroservicesConfiguration
    {
        public static AddressMicroservicesConfiguration Current { get; set; }

        /// <summary>
        /// Display name for this application/blog
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Sql Server ConnectionString for this application
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Address Postcode Web Service API String for this application
        /// </summary>
        public string AddressPostcodeServiceString { get; set; }

        /// <summary>
        /// Address Postcode Web Service API Key String for this application
        /// </summary>
        public string AddressPostcodeServiceAPIKeyString { get; set; }

        /// <summary>
        /// The server relative root path for this application
        /// </summary>
        public string ApplicationBasePath { get; set; } = "/";

        public string ApplicationHomeUrl { get; set; } = "/";
    }
}
