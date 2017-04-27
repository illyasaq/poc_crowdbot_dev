using CrowdBot.Common;
using CrowdBot.Services;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace CrowdBot.Dialogs.Childs
{
    [Serializable]
    public class IdeaDialog : IDialog<bool>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //await context.PostAsync("");
            PromptDialog.Choice(context, UserIdea,
               new string[] { Idea.DESIGN, Idea.DEVELOPMENT, Idea.DESIGNANDDEVELOPMENT, Idea.TESTRELATED, Idea.DATASCIENCE },
               "What is your idea? You can choose one of the following:");
        }

        private async Task UserIdea(IDialogContext context, IAwaitable<string> result)
        {
            var res = await result;
            switch (res)
            {
                case Idea.DESIGN:
                    await context.PostAsync($"You have an idea for a new {Idea.DESIGN}");
                    break;
                case Idea.DEVELOPMENT:
                    await context.PostAsync($"You have an idea for a new {Idea.DEVELOPMENT}");
                    break;
                case Idea.DESIGNANDDEVELOPMENT:
                    await context.PostAsync($"You have an idea for a new {Idea.DESIGNANDDEVELOPMENT}");
                    break;
                case Idea.TESTRELATED:
                    await context.PostAsync($"You have an idea about {Idea.TESTRELATED}");
                    break;
                case Idea.DATASCIENCE:
                    await context.PostAsync($"You have an idea about {Idea.DATASCIENCE}");
                    break;
                default:
                    context.Done(true);
                    break;
            }

            // Get Answer from QnAMaker service
            var answer = QNA.CallQnAService(QNAQuestions.USERHAVENEWIDEA);
            await context.PostAsync(answer);

            // Prompt for a new question sequence
            PromptDialog.Choice(context, UserTechnology,
               new string[] { Prompts.YES, Prompts.NO },
               "Do you have any technology in mind?");
        }

        private async Task UserTechnology(IDialogContext context, IAwaitable<string> result)
        {
            var res = await result;
            switch (res)
            {
                case Prompts.YES:
                    await context.PostAsync("Please enter the technology.");
                    context.Wait(IdeaMessageAsync);
                    break;                
                default:
                    await context.PostAsync("Sorry. Ending this process.");
                    context.Done(true);
                    break;
            }            
        }

        public virtual async Task IdeaMessageAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var res = await result;
            if (res.Text == "done")
            {
                context.Done(true);
            }
            else
            {
                await context.PostAsync($"You've entered '{res.Text}' as technology. Enter again or type 'done' to end this.");
                context.Wait(IdeaMessageAsync);
            }
        }
    }
}