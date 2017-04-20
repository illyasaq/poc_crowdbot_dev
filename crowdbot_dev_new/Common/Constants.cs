namespace CrowdBot.Common
{
    public class Constants
    {        
        public const string LUIS_URI = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/3ee6a4af-0303-48d2-8d77-592e6cc4e27f?subscription-key=11713a4f2b134179a5d700aa73283563&verbose=true&timezoneOffset=0.0&spellCheck=true&q={0}";
        public const string QNAMAKER_URI = "https://westus.api.cognitive.microsoft.com/qnamaker/v1.0";
        public const string QNAMAKER_KNOWLEDGEBASE_ID = "1ecf0287-a27d-4469-84ce-9c5597ada66d";
        public const string QNAMAKER_SUBSCRIPTION_KEY = "33e87f38b9c247429124ccebcde25096";

        public const string ASKUSERFORNEXTQUESTION = "Is there anything else you want to know about?";
    }

    public class Prompts
    {
        public const string YES = "Yes";
        public const string NO = "No";
        public const string OK = "OK";
    }
}