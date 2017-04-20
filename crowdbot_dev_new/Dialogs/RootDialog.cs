using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

using CrowdBot.Dialogs.Starter;
using CrowdBot.Services;
using CrowdBot.Common;

namespace CrowdBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {        
        public async Task StartAsync(IDialogContext context)
        {
            //await context.PostAsync("RootDialog.StartAsync");
            context.Wait(this.MessageReceivedAsync);            
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            // Get LUIS Intent
            var msg = await result;
            var LUISResp = await LUIS.PUserInput(msg.Text);

            switch (LUISResp.topScoringIntent.intent)
            {
                case LUISIntents.USERASKABOUTCROWDSOURCE:
                    ConversationCrowd.resumeReference = new ConversationReference(context.Activity.Id, context.Activity.From,
                                                                                  context.Activity.Recipient, context.Activity.Conversation, 
                                                                                  context.Activity.ChannelId, context.Activity.ServiceUrl);
                    ConversationCrowd.Resume().GetAwaiter();
                    break;
                case LUISIntents.USERASKABOUTDEVELOPER:
                    await context.PostAsync(LUISIntents.USERASKABOUTDEVELOPER);
                    break;
                case LUISIntents.USERHAVENEWIDEA:
                    await context.PostAsync(LUISIntents.USERHAVENEWIDEA);
                    break;
                default:
                    await context.PostAsync("Null intent received");
                    break;
            }       
        }
    }
}