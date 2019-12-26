using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuDou.Grace.Web.Authentication.External;

namespace TuDou.Grace.Web.Models.External
{
    public class ExternalLoginProviderInfo
    {
        public string Name { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public Type ProviderApiType { get; set; }

        public Dictionary<string, string> AdditionalParams { get; set; }

        public List<JsonClaimMap> ClaimMappings { get; set; }
        public ExternalLoginProviderInfo(
          string name,
          string clientId,
          string clientSecret,
          Type providerApiType,
          Dictionary<string, string> additionalParams = null,
          List<JsonClaimMap> claimMappings = null)
        {
            this.Name = name;
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.ProviderApiType = providerApiType;
            this.AdditionalParams = additionalParams;
            this.ClaimMappings = claimMappings ?? new List<JsonClaimMap>();
        }
    }
}
