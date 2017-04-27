namespace CrowdBot.Common
{
    public class Constants
    {
        public const string LUIS_URI = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/{0}?subscription-key={1}&verbose=true&timezoneOffset=0.0&spellCheck=true&q={2}";
        public const string LUIS_KNOWLEDGEBASE_ID = "3ee6a4af-0303-48d2-8d77-592e6cc4e27f";
        public const string LUIS_SUBSCRIPTION_KEY = "11713a4f2b134179a5d700aa73283563";

        public const string QNAMAKER_URI = "https://westus.api.cognitive.microsoft.com/qnamaker/v1.0";
        public const string QNAMAKER_KNOWLEDGEBASE_ID = "1ecf0287-a27d-4469-84ce-9c5597ada66d";
        public const string QNAMAKER_SUBSCRIPTION_KEY = "33e87f38b9c247429124ccebcde25096";

        public const string IDONOTKNOWABOUTIT = "I am sorry. That is the extend of my knowledge for now.";
        public const string ASKUSERFORNEXTQUESTION = "How else can I help you?";
        public const string GREETINGS = "Hello, how can I help you?";
    }

    public class Prompts
    {
        public const string YES = "Yes";
        public const string NO = "No";
        public const string OK = "OK";
    }
    
    public class Idea
    {
        public const string DESIGN = "Design";
        public const string DEVELOPMENT = "Development";
        public const string DESIGNANDDEVELOPMENT = "Design and Development";
        public const string TESTRELATED = "Test related";
        public const string DATASCIENCE = "Data Science";
    }
}