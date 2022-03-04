using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System.Threading;
using System.Threading.Tasks;

namespace AhoraSi.Commons
{
    public class ConfirmButtonDialog
    {
        public static async Task<DialogTurnResult> ShowOption(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var options = await stepContext.PromptAsync(
                nameof(ConfirmPrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("¿Hay algo más en lo que te pueda ayudar?"),
                    Style = ListStyle.SuggestedAction
                }
            );
            return options;
        }   

    }
}
