using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AhoraSi.Dialogs
{
    public class cajaAhorroDialog: ComponentDialog
    {
        public cajaAhorroDialog(string dialogId) : base(dialogId)
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
            AddDialog(new WaterfallDialog($"{nameof(cajaAhorroDialog)}.mainFlow", waterfallSteps));
            AddDialog(new TextPrompt(nameof(cajaAhorroDialog))); 

            InitialDialogId = $"{nameof(cajaAhorroDialog)}.mainFlow";
        }


        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync($"Para ver tu caja de ahorro ingresa al siguiente link: https://bepensa.csod.com/client/bepensa/default.aspx", cancellationToken: cancellationToken);
            return await stepContext.NextAsync(null, cancellationToken);
        }

        private Task<DialogTurnResult> Regresar(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
