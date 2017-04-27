using CrowdBot.Common;
using CrowdBot.Services;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;

namespace CrowdBot.Dialogs.Childs
{

    [Serializable]
    public class CrowdSourceDialog : IDialog<bool>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //await context.PostAsync("CrowdSourceDialog.StartAsync");
            PromptDialog.Choice(context, WhatIsCrowdsourcing, 
                new string[] { Prompts.OK, Prompts.NO }, "Sure. I will be able to help you. Let us start with What is crowdsourcing");
        }

        private async Task WhatIsCrowdsourcing(IDialogContext context, IAwaitable<string> result)
        {
            var res = await result;
            switch (res)
            {
                case Prompts.NO:
                    PromptDialog.Choice(context, CrowdSourcingInJH,
                        new string[] { Prompts.YES, Prompts.NO }, "Oh sorry. Do you mean about crowdsourcing at John Hancock?");
                    break;
                default:
                    // Get Answer from QnAMaker service
                    var answer = QNA.CallQnAService(QNAQuestions.ASKABOUTCROWDSOURCING);
                    await context.PostAsync(answer);

                    PromptDialog.Choice(context, CrowdSourcingInJH,
                        new string[] { Prompts.YES, Prompts.NO }, "Do you want to know about crowdsourcing in John Hancock?");
                    break;
            }
        }

        private async Task CrowdSourcingInJH(IDialogContext context, IAwaitable<string> result)
        {
            var res = await result;
            switch (res)
            {
                case Prompts.NO:
                    await context.PostAsync(Constants.IDONOTKNOWABOUTIT);
                    context.Done(true); //Finish this dialog
                    break;
                default:
                    // Get Answer from QnAMaker service
                    var answer = QNA.CallQnAService(QNAQuestions.ASKABOUTCROWDSOURCINGINJH);
                    await context.PostAsync(answer);
                    //await context.PostAsync(Constants.ASKUSERFORNEXTQUESTION);
                    context.Done(true); //Finish this dialog
                    break;
            }
        }

    }
}