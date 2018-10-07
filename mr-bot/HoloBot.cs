using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace MRBot
{
    public class HoloBot : IBot
    {
        public async Task OnTurn(ITurnContext context)
        {
            ConversationContext.userMsg = context.Activity.Text;

            if (context.Activity.Type is ActivityTypes.Message)
            {
                if (string.IsNullOrEmpty(ConversationContext.userName))
                {
                    ConversationContext.userName = ConversationContext.userMsg;
                    await context.SendActivityAsync($"Hello {ConversationContext.userName}. Looks like today it is going to rain. \nLuckily I have umbrellas and waterproof jackets to sell!");
                }
                else
                {
                    if (ConversationContext.userMsg.Contains("how much"))
                    {
                        if (ConversationContext.userMsg.Contains("umbrella")) await context.SendActivityAsync($"Umbrellas are $13.");
                        else if (ConversationContext.userMsg.Contains("jacket")) await context.SendActivityAsync($"Waterproof jackets are $30.");
                        else await context.SendActivityAsync($"Umbrellas are $13. \nWaterproof jackets are $30.");
                    }
                    else if (ConversationContext.userMsg.Contains("color") || ConversationContext.userMsg.Contains("colour"))
                    {
                        await context.SendActivityAsync($"Umbrellas are black. \nWaterproof jackets are yellow.");
                    }
                    else
                    {
                        await context.SendActivityAsync($"Sorry {ConversationContext.userName}. I did not understand the question");
                    }
                }
            }
            else
            {

                ConversationContext.userMsg = string.Empty;
                ConversationContext.userName = string.Empty;
                await context.SendActivityAsync($"Welcome! \nI am the Weather Shop Bot \nWhat is your name?");
            }

        }

        public Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
