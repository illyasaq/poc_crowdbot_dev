using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

using CrowdBot.Services;
using CrowdBot.Common;
using CrowdBot.Dialogs.Childs;

namespace CrowdBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //await context.PostAsync("Hello.");
            context.Wait(this.MessageReceivedAsync);            
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            // Get LUIS Intent
            var msg = await result;
            var LUISResp = await LUIS.PUserInput(msg.Text);
            
            switch (LUISResp.topScoringIntent.intent)
            {
                case LUISIntents.CROWD_GREETINGS:
                    // User send greetings
                    await context.PostAsync(Constants.GREETINGS);
                    break;
                case LUISIntents.CROWD_CROWDSOURCE:
                    // User ask about crowd sourcing information
                    context.Call(new CrowdSourceDialog(), ResumeAfterCSDialog);                    
                    break;
                case LUISIntents.CROWD_DEVELOPER:
                    await context.PostAsync(LUISIntents.CROWD_DEVELOPER);
                    break;
                case LUISIntents.CROWD_NEWIDEAS:
                    context.Call(new IdeaDialog(), ResumeAfterCSDialog);
                    break;
                case LUISIntents.CROWD_HELP:
                    await context.PostAsync(LUISIntents.CROWD_HELP);
                    break;
                default:
                    await context.PostAsync(Constants.IDONOTKNOWABOUTIT);
                    break;
            }       
        }

        private async Task ResumeAfterCSDialog(IDialogContext context, IAwaitable<bool> result)
        {
            var success = await result;
            if (success)
            {
                await context.PostAsync(Constants.ASKUSERFORNEXTQUESTION);
                context.Wait(MessageReceivedAsync);
            }
        }
    }
}