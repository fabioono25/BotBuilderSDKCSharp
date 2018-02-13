using Bot_Application.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bot_Application.Dialogs
{
    [Serializable]
    [LuisModel("17f274ff-9fd9-4bd4-9034-95b68efc4ddb", "a57b480758474c09a807e094f7974217")]
    public class CotacaoDialog : LuisDialog<object>
    {
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Desculpe, não consegui entender a frase { result.Query}");
        }

        [LuisIntent("Sobre")]
        public async Task Sobre(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Eu sou apenas um bot, tenha paciência comigo.");
        }

        [LuisIntent("Cumprimento")]
        public async Task Cumprimento(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Olá eu sou um bot que faz cotação de moedas.");
        }

        [LuisIntent("Cotacao")]
        public async Task Cotacao(IDialogContext context, LuisResult result)
        {
            var moedas = result.Entities?.Select(_ => _.Entity);
            var filtro = string.Join(",", moedas.ToArray());

            var endpoint = $"http://api-cotacoes-maratona-bots.azurewebsites.net/api/cotacoes/{filtro}";

            //await context.PostAsync("Aguarde um momento enquanto obtenho os valores...");
            await context.PostAsync($"Aguarde um momento enquanto eu obtenho as cotações para as moedas {string.Join(",", moedas.ToArray())}...");

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    await context.PostAsync("Ocorreu um erro... tente mais tarde.");
                    return;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<Cotacao[]>(json);
                    var cotacoes = resultado.Select(_ => $"{_.Nome} : {_.Valor}");
                    await context.PostAsync($"{string.Join(",", cotacoes.ToArray())}");
                }
            }
        }
    }
}