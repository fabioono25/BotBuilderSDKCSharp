using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot_Application.Formulario
{
    [Serializable]
    [Template(TemplateUsage.NotUnderstood,"Desculpe, não entendi \"{0}\".")]
    public class Pedido
    {
        public Salgadinhos Salgadinhos { get; set; }
        public Bebidas Bebidas { get; set; }
        public TipoEntrega TipoEntrega { get; set; }
        public CPFNaNota CPFNaNota { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

        public static IForm<Pedido> BuildForm()
        {
            var form = new FormBuilder<Pedido>();
            form.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Buttons;
            form.Configuration.Yes = new string[] { "sim","yes","s","yep","(y)" };
            form.Configuration.No = new string[] { "não", "nao", "no", "not", "n" };
            form.Message("Bem vindo! Será um prazer atendê-lo!");
            form.OnCompletion(async (context, Pedido) =>
            {
                //salvar base
                //gerar pedido
                //integracoes
                await context.PostAsync("Seu pedido número 123 foi gerado. Em instantes será entregue");
            });

            return form.Build();
        }
    }

    [Describe("Tipo de Entrega")]
    public enum TipoEntrega
    {
        [Terms("Retirar no local", "passo aí", "eu pego", "retiro aí")]
        [Describe("Retirar no local")]
        RetirarNoLocal = 1,
        [Terms("motoboy", "entrega", "em casa")]
        [Describe("Motoboy")]
        Motoboy
    }

    public enum Salgadinhos
    {
        Esfiha = 1,
        Kibe,
        Coxinha
    }

    [Describe("Bebidas")]
    public enum Bebidas
    {
        [Terms("Água", "agua", "h20", "aga")]
        [Describe("(1) Água")]
        Agua = 1,
        [Describe("(2) Refrigerante")]
        Refrigerante,
        [Describe("(3) Suco")]
        Suco
    }

    public enum CPFNaNota
    {
        Sim = 1,
        Nao
    }
}