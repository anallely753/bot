using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;


namespace AhoraSi.Commons
{
    public class OptionButton
    {
        public static async Task<DialogTurnResult> ShowOptions(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var option = await stepContext.PromptAsync(
                nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("¿Cómo puedo ayudarte?"),
                    Choices = ChoiceFactory.ToChoices(new List<string>
                    {
                        "Nómina", 
                        "Infonavit",
                        "Vacaciones",
                        "Caja de ahorro",
                        "Beneplus",
                        "Formatos"
                    }),
                    Style= ListStyle.HeroCard
                },
                cancellationToken
            );
            return option;
        }
    }
}
