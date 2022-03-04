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
    }
}