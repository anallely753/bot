using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AhoraSi.Dialogs
{
    public class beneplusDialog: ComponentDialog
    {
        public beneplusDialog(string dialogId) : base(dialogId)
        {
            InitializeWaterfallDialog();
        }

        private void InitializeWaterfallDialog()
        {
            //Creamos pasos de la cascada
            var waterfallSteps = new WaterfallStep[]
            {
                InitialStepAsync,
                Regresar
            };

            //Agregamos subdialogos
            AddDialog(new WaterfallDialog($"{nameof(beneplusDialog)}.mainFlow", waterfallSteps));
            AddDialog(new TextPrompt(nameof(beneplusDialog)));

            InitialDialogId = $"{nameof(beneplusDialog)}.mainFlow";
        }


        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync($"Para ver tu información de beneplus: https://bepensa.csod.com/client/bepensa/default.aspx", cancellationToken: cancellationToken);
            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> Regresar(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.EndDialogAsync(null, cancellationToken);

        }
    }
}
