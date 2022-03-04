﻿using AhoraSi.Commons;
using Microsoft.Bot.Builder;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AhoraSi.Dialogs
{
    public class nominaDialog: ComponentDialog
    {

        public nominaDialog(string dialogId) : base(dialogId)
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
            AddDialog(new WaterfallDialog($"{nameof(nominaDialog)}.mainFlow", waterfallSteps));
            AddDialog(new TextPrompt(nameof(nominaDialog)));

            InitialDialogId = $"{nameof(nominaDialog)}.mainFlow";
        }


        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync($"Para consultar información de tu nómina ingresa al siguiente link: https://bepensa.csod.com/client/bepensa/default.aspx", cancellationToken: cancellationToken);
            return await stepContext.NextAsync(null, cancellationToken);
        }
        private async Task<DialogTurnResult> Regresar(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.EndDialogAsync(null, cancellationToken);

        }

    }
}
