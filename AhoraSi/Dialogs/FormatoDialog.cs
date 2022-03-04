using AhoraSi.Commons;
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
    internal class FormatoDialog : ComponentDialog
    {
        public FormatoDialog(string dialogId = null) : base(dialogId)
        {
            InitializeWaterfallDialog();
        }

        private void InitializeWaterfallDialog()
        {
            //Creamos pasos de la cascada
            var waterfallSteps = new WaterfallStep[]
            {
                InitialStepAsync,
                DescargarFormatoStepAsync,
                ConfirmarRegreso,
               FinalStepAsync
            };

            //Agregamos subdialogos
            AddDialog(new WaterfallDialog($"{nameof(nominaDialog)}.mainFlow", waterfallSteps));
            AddDialog(new ChoicePrompt($"{nameof(nominaDialog)}.tipoFormato"));
            AddDialog(new TextPrompt(nameof(RootDialog)));
            AddDialog(new TextPrompt($"{nameof(nominaDialog)}.confirmarRegreso"));
            //Indicamos con cual subdialogo comenzar
        }
        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync($"{nameof(nominaDialog)}.tipoFormato",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("¿Qué formato necesitas?"),
                    Choices = ChoiceFactory.ToChoices(new List<string> { "Vacaciones", "Permiso", "Acceso", "Constancia laboral" }),
                    Style = ListStyle.HeroCard
                }, cancellationToken);
        }
        private async Task<DialogTurnResult> DescargarFormatoStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var option = stepContext.Context.Activity.Text;
            var documentoCard = new Attachment();
            await stepContext.Context.SendActivityAsync($"Aquí tienes, descarga tu formato de {option}", cancellationToken: cancellationToken);
            await stepContext.Context.SendActivityAsync(AttachmentCard.GetFile(option), cancellationToken);

            return await stepContext.NextAsync(null, cancellationToken);
        }
        private async Task<DialogTurnResult> ConfirmarRegreso(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            var options = await stepContext.PromptAsync($"{nameof(cajaAhorroDialog)}.confirmarRegreso",
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