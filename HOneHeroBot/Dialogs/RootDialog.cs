using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace HOneHeroBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public string LoginId { get; private set; }

        

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        
        private static Attachment GetHeroCard()
        {
            var heroCard = new HeroCard
            {
                Title = "HOne Hero Bot",
                Subtitle = "H|One pvt Ltd",                
                Text = "This is a ResQBot for H|One",
                Images = new List<CardImage> { new CardImage("https://honeherobotk.azurewebsites.net/Images/awardanimated.gif")},              
            };
            return heroCard.ToAttachment();
        }
        



        private static Attachment GetAnimationCard()
          {
            var animationCard = new AnimationCard
            {
                Title = "Microsoft Bot Framework",
                Subtitle = "Animation Card",
               // Image = new ThumbnailUrl
                //{
                 //   Url = "https://docs.microsoft.com/en-us/bot-framework/media/how-it-works/architecture-resize.png"
               // },
                Media = new List<MediaUrl>
                {
                    new MediaUrl()
                    {
                        Url = "https://honeherobotk.azurewebsites.net/Images/awardanimated.gif"
                    }
                }
            };

            return animationCard.ToAttachment();
        }


        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            // calculate something for us to return
            //int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            //await context.PostAsync($"You sented {activity.Text} which was {length} characters");


            if (activity.Text.Contains("hi"))
            {
                LoginId = activity.From.Name;
                await context.PostAsync("hi.." + LoginId);
           
                var message = context.MakeMessage();
                var attachment = GetHeroCard();
                message.Attachments.Add(attachment);
                await context.PostAsync(message);

                //await Conversation.SendAsync(activity, () => new Dialogs.SimpleQnADialog());
            }
            
           
            else if (activity.Text.Contains("hello"))
            {
                var message1 = context.MakeMessage();
                var attachment = GetAnimationCard();
                message1.Attachments.Add(attachment);
                await context.PostAsync(message1);
            }
            else if (activity.Text.Contains("fine"))
            {
                await context.PostAsync("Oh good.Nice to chat you..");
            }
            else if (activity.Text.Contains("who are you"))
            {
                await context.PostAsync("I am H-One Bot");
            }
            else if (activity.Text.Contains("website"))
            {
                await context.PostAsync("You can get clear idea about us via our website http://www.h-oneonline.com/");
            }
            else if (activity.Text.Contains("technology"))
            {
                await context.PostAsync("ERP, Cloud ");
            }
            else if (activity.Text.Contains("open time"))
            {
                await context.PostAsync("8.30 - 5.30");
            }
            else if (activity.Text.Contains("lunch"))
            {
                await context.PostAsync("Lunch is ready, you can eat now..");
            }
            else if (activity.Text.Contains("date"))
            {
                await context.PostAsync(DateTime.Now.ToString());
            }
            else if (activity.Text.Contains("night"))
            {
                await context.PostAsync(" Good night and Sweetest Dreams..");
            }
            else if (activity.Text.Contains("bye"))
            {
                await context.PostAsync("Bye.Have a nice day with H-One Bot..");
            }
            else
            {
                await context.PostAsync($"I cann't understand * {activity.Text} *  more clarify..");
            }
            context.Wait(MessageReceivedAsync);
        }
    }
}