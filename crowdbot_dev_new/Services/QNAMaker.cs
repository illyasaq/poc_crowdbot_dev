using CrowdBot.Common;
using Newtonsoft.Json;
using System;
using System.Net;

namespace CrowdBot.Services
{
    public class CrowdQNA
    {
        public string Answer { get; set; }
        public string Score { get; set; }
    }

    public class QNA
    {
        //Call QNAService to get answer from Knowledge Base
        public static string CallQnAService(string Question)
        {
            var answerStr = QNA.GetAnswerFromQuestion($"{Question}").Answer;
            return answerStr;
        }

        private static CrowdQNA GetAnswerFromQuestion(string query)
        {
            string responseString = string.Empty;
                        
            //Build the URI
            Uri qnamakerUriBase = new Uri(Constants.QNAMAKER_URI);
            var builder = new UriBuilder($"{qnamakerUriBase}/knowledgebases/{Constants.QNAMAKER_KNOWLEDGEBASE_ID}/generateAnswer");

            //Add the question as part of the body
            var postBody = $"{{\"question\": \"{query}\"}}";

            //Send the POST request
            using (WebClient client = new WebClient())
            {
                //Set the encoding to UTF8
                client.Encoding = System.Text.Encoding.UTF8;

                //Add the subscription key header
                client.Headers.Add("Ocp-Apim-Subscription-Key", Constants.QNAMAKER_SUBSCRIPTION_KEY);
                client.Headers.Add("Content-Type", "application/json");
                responseString = client.UploadString(builder.Uri, postBody);
                var _Data = JsonConvert.DeserializeObject<CrowdQNA>(responseString);

                return _Data;
            }
        }

    }

}