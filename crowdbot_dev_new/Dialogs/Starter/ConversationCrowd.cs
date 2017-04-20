using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System.Threading;
using System.Threading.Tasks;

namespace CrowdBot.Dialogs.Starter
{
    public class ConversationCrowd
    {
        //Note: Of course you don't want this here. Eventually you will need to save this in some table
        //Having this here as static variable means we can only remember one user :)
        //public static string resumptionCookie;
        public static ConversationReference resumeReference;

        //This will interrupt the conversation and send the user to SurveyDialog, then wait until that's done 
        public static async Task Resume()
        {
            //var message = ResumptionCookie.GZipDeserialize(resumptionCookie).GetMessage();
            var message = resumeReference.GetPostToBotMessage();
            //var client = new ConnectorClient(new Uri(message.ServiceUrl));

            using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
            {
                var botData = scope.Resolve<IBotData>();
                await botData.LoadAsync(CancellationToken.None);
                var task = scope.Resolve<IDialogTask>();

                //interrupt the stack
                var dialog = new Childs.CrowdSourceDialog();
                task.Call(dialog.Void<object, IMessageActivity>(), null);

                await task.PollAsync(CancellationToken.None);

                //flush dialog stack
                await botData.FlushAsync(CancellationToken.None);
            }
        }
    }
}