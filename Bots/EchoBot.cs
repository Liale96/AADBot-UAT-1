// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            //var replyText = $"Echo: {turnContext.Activity.Text}";
            //await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);

            // find the user's name
            var userName = turnContext.Activity.From.Name;

            //return user's name to user via adaptive card
            var reply = MessageFactory.Attachment(new List<Attachment>());
            var card = new AdaptiveCards.AdaptiveCard();
            card.Body.Add(new AdaptiveCards.AdaptiveTextBlock()
            {
                Text = $"Hello {userName}!",
                Size = AdaptiveCards.AdaptiveTextSize.Large,
                Weight = AdaptiveCards.AdaptiveTextWeight.Bolder
            });
            reply.Attachments.Add(new Attachment()
            {
                ContentType = AdaptiveCards.AdaptiveCard.ContentType,
                Content = card
            });
            await turnContext.SendActivityAsync(reply, cancellationToken);


            // find the user's id
            //var userId = turnContext.Activity.From.Id;

        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
