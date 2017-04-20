using CrowdBot.Common;
using CrowdBot.Services;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;

namespace CrowdBot.Dialogs.Childs
{

    [Serializable]
    public class CrowdSourceDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //await context.PostAsync("CrowdSourceDialog.StartAsync");
            PromptDialog.Choice(context, WhatIsCrowdsourcing, 
                new string[] { Prompts.OK, Prompts.NO }, "Sure. I will be able to help you. Let us start with What is crowdsourcing");
        }

        private async Task WhatIsCrowdsourcing(IDialogContext context, IAwaitable<string> result)
        {
            //await context.PostAsync("CrowdSourceDialog.AfterSelectOption");
            var res = await result;
            switch (res)
            {
                case Prompts.NO:
                    PromptDialog.Choice(context, CrowdSourcingInJH, 
                        new string[] { Prompts.YES, Prompts.NO }, "do you mean about crowdsourcing at JH?");
                    break;
                default:
                    // Get Answer from QnAMaker service
                    var answer = QNA.CallQnAService(QNAQuestions.ASKABOUTCROWDSOURCING);
                    await context.PostAsync(answer);
                    await context.PostAsync(Constants.ASKUSERFORNEXTQUESTION);
                    context.Done(this); //Finish this dialog
                    break;
            }
        }

        private async Task CrowdSourcingInJH(IDialogContext context, IAwaitable<string> result)
        {
            var res = await result;
            switch (res)
            {
                case Prompts.NO:
                    await context.PostAsync($"I am sorry. That is the extend of my knowledge for now. {Constants.ASKUSERFORNEXTQUESTION}");
                    context.Done(this); //Finish this dialog
                    break;
                default:
                    // Get Answer from QnAMaker service
                    var answer = QNA.CallQnAService(QNAQuestions.ASKABOUTCROWDSOURCINGINJH);
                    await context.PostAsync(answer);
                    await context.PostAsync(Constants.ASKUSERFORNEXTQUESTION);
                    context.Done(this); //Finish this dialog
                    break;
            }
        }

    }
}