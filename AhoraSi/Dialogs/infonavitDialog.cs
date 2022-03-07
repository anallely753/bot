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
    public class infonavitDialog : ComponentDialog
    {

        public infonavitDialog(string dialogId) : base(dialogId)
        {
            InitializeWaterfallDialog();
        }

        private void InitializeWaterfallDialog()
        {
            //Creamos pasos de la cascada
            var waterfallSteps = new WaterfallStep[]
            {
               InitialStepAsync,
                ConfirmarRegreso,
               FinalStepAsync
            };

            //Agregamos subdialogos
            AddDialog(new WaterfallDialog($"{nameof(infonavitDialog)}.mainFlow", waterfallSteps));
            AddDialog(new TextPrompt(nameof(infonavitDialog)));
            AddDialog(new TextPrompt($"{nameof(infonavitDialog)}.confirmarRegreso"));
            InitialDialogId = $"{nameof(infonavitDialog)}.mainFlow";
        }


        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync($"Ve tu información de Infonavit aquí: https://bepensa.csod.com/client/bepensa/default.aspx", cancellationToken: cancellationToken);
            return await stepContext.NextAsync(null, cancellationToken);
        }
        private async Task<DialogTurnResult> ConfirmarRegreso(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            var options = await stepContext.PromptAsync($"{nameof(infonavitDialog)}.confirmarRegreso",
                new PromptOptions
                {
                    Prompt = CreateSuggestedActions(stepContext)
                }, cancellationToken);
            return options;
        }
        private Activity CreateSuggestedActions(WaterfallStepContext stepContext)
        {
            var reply = MessageFactory.Text($"¿Hay algo más en lo que pueda ayudarte?");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction(){Title="Sí", Value="SI", Type= ActionTypes.ImBack},
                    new CardAction(){Title="No", Value="NO", Type= ActionTypes.ImBack},
                }
            };
            return reply;
        }
        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["confirmarRegreso"] = (string)stepContext.Result;
            if ((string)stepContext.Values["confirmarRegreso"] == "SI")
            {
                return await stepContext.BeginDialogAsync($"{nameof(RootDialog)}.mainFlow", null, cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync($"¡Hasta pronto!", cancellationToken: cancellationToken);
                return await stepContext.CancelAllDialogsAsync();
            }
        }


    }
}
