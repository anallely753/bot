// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AhoraSi
{
    public class AhoraSi<T> : ActivityHandler where T: Dialog
    {
        protected readonly Dialog _dialog;
        protected readonly BotState _conversationState;
        protected readonly ILogger _logger;

        public AhoraSi(T Dialog, ConversationState conversationState, ILogger<AhoraSi<T>> logger )
        {
            _dialog = Dialog;
            _conversationState = conversationState;
            _logger = logger;
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    //await turnContext.SendActivityAsync(MessageFactory.Text($"Hello world!"), cancellationToken);
                    await _dialog.RunAsync(
                       turnContext,
                       _conversationState.CreateProperty<DialogState>(nameof(DialogState)),
                       cancellationToken
                   );
                }
            }
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await _dialog.RunAsync(
                turnContext,
                _conversationState.CreateProperty<DialogState>(nameof(DialogState)),
                cancellationToken
            );
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            await base.OnTurnAsync(turnContext, cancellationToken);
            await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
        }
    }
}
