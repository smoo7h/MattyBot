using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace MattyBot
{
    [LuisModel("", "")]
    [Serializable]
    public class MattyLuisDialog : LuisDialog<object>
    {
        [LuisIntent("Weather")]
        public async Task GetWeather(IDialogContext context, LuisResult result)
        {
            string reply = "";
            using (WebClient webClient = new System.Net.WebClient())
            {
                WebClient n = new WebClient();
                var json = n.DownloadString("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22toronto%2C%20on%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
                string valueOriginal = Convert.ToString(json);
                var weather = JsonConvert.DeserializeObject(json);

                //  string name = weather
                // Bad Boys



                reply = weather.ToString();

            }
          
            //https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22toronto%2C%20on%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys
            await context.PostAsync(reply);
            context.Wait(MessageReceived);

        }



    }
}