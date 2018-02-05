using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace Bot_Application.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            //working with diferent types of messages


            // calculate something for us to return
            //int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync("**Ola tudo bem?**");
            //await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            //HeroCard
            var message = activity.CreateReply();

            if (activity.Text.Equals("herocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var heroCard = new HeroCard();
                heroCard.Title = "Planeta";
                heroCard.Subtitle = "Universo";
                heroCard.Images = new List<CardImage>
                {
                    new CardImage("https://static.pexels.com/photos/160699/girl-dandelion-yellow-flowers-160699.jpeg", "Beautiful Girl",
                    new CardAction(ActionTypes.OpenUrl, title: "Google", value: "http://www.google.com"))
                };

                heroCard.Buttons = new List<CardAction>
                {
                    new CardAction
                    {
                        Title = "Botao 1",
                        Type = ActionTypes.PostBack,
                        Value = "Aqui vai um valor"
                    },
                    new CardAction
                    {
                        Title = "Botao 2",
                        Type = ActionTypes.PostBack,
                        Value = "Aqui vai outro valor"
                    }
                };

                message.Attachments.Add(heroCard.ToAttachment());

            }
            else if (activity.Text.Equals("videocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var videoCard = new VideoCard();
                videoCard.Title = "Um video qualquer";
                videoCard.Subtitle = "Subtítulo";
                videoCard.Autostart = true;
                videoCard.Autoloop = true;
                videoCard.Media = new List<MediaUrl>
                {
                    new MediaUrl("https://www.youtube.com/watch?v=Y_yJL4JfSvw")
                };
                message.Attachments.Add(videoCard.ToAttachment());
            }
            else if (activity.Text.Equals("audiocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var audioCard = CreateAudioCard();

                message.Attachments.Add(audioCard);
            }
            else if (activity.Text.Equals("animationcard", StringComparison.InvariantCultureIgnoreCase))
            {
                var animationCard = CreateAnimationCard();
                
                message.Attachments.Add(animationCard);
            }
            else if (activity.Text.Equals("carousel", StringComparison.InvariantCultureIgnoreCase))
            {
                message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                
                var audio = CreateAudioCard();
                var animation = CreateAnimationCard();

                message.Attachments.Add(animation);
                message.Attachments.Add(audio);
                message.Attachments.Add(animation);
                message.Attachments.Add(animation);
                message.Attachments.Add(animation);
            }

            await context.PostAsync(message);

            context.Wait(MessageReceivedAsync);
        }

        private Attachment CreateAnimationCard()
        {
            var animationCard = new AnimationCard();
            animationCard.Title = "Uma animacao qualquer";
            animationCard.Subtitle = "Subtítulo";
            animationCard.Autostart = true;
            animationCard.Autoloop = false;
            animationCard.Media = new List<MediaUrl>
                {
                    new MediaUrl("https://media1.tenor.com/images/efc9f6ce8923eadf351b676f29bf943c/tenor.gif")
                };

            return animationCard.ToAttachment();
        }

        private Attachment CreateAudioCard()
        {
            var audioCard = new AudioCard();
            audioCard.Title = "Um audio qualquer";
            audioCard.Image = new ThumbnailUrl("https://static.pexels.com/photos/160699/girl-dandelion-yellow-flowers-160699.jpeg", "Thumbmail");
            audioCard.Subtitle = "Subtítulo";
            audioCard.Autostart = true;
            audioCard.Autoloop = false;
            audioCard.Media = new List<MediaUrl>
            {
                new MediaUrl("http://www.kozco.com/tech/piano2-CoolEdit.mp3")
            };

            return audioCard.ToAttachment();
        }
    }
}