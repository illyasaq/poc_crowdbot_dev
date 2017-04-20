using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

using CrowdBot.Common;

namespace CrowdBot.Services
{
    public class CrowdLUIS
    {
        public string query { get; set; }
        public Topscoringintent topScoringIntent { get; set; }
        public Intent[] intents { get; set; }
        public Entity[] entities { get; set; }
    }

    public class Topscoringintent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }

    public class Intent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }

    public class Entity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public float score { get; set; }
    }


    class LUIS
    {
        public static async Task<CrowdLUIS> PUserInput(string strInput)
        {
            string strRet = string.Empty;
            string strEscape = Uri.EscapeDataString(strInput);

            using (var client = new HttpClient())
            {
                string curi = string.Format(Constants.LUIS_URI, strInput);
                HttpResponseMessage msg = await client.GetAsync(curi);

                if (msg.IsSuccessStatusCode)
                {
                    var jsonResponse = await msg.Content.ReadAsStringAsync();
                    var _Data = JsonConvert.DeserializeObject<CrowdLUIS>(jsonResponse);

                    return _Data;
                }
            }

            return null;
        }
    }

}