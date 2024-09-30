using Newtonsoft.Json;
using System.Net;
using Szfindel.Interface;
using Szfindel.Models;
using static System.Net.WebRequestMethods;

namespace Szfindel.Repo
{

    public class ApiRepo : IApi
    {
        private const string API_KEY = "7471dce1d71ebdc0329017d39de27d3b";


        /// <summary>
        /// Funkcja pobiera i konwertuje dane z zewnętrznego api pogodowego
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public WeatherApi Get(string city)
        {
            try
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API_KEY}";

                var web = new WebClient();
                var response = web.DownloadString(url);

                var myDeserializedClass = JsonConvert.DeserializeObject<WeatherApi>(response); //deserializowanie obiektu

                myDeserializedClass.main.temp = Math.Round(myDeserializedClass.main.temp - 273.15, 2);

                return myDeserializedClass;
            }
            catch (WebException ex) //Wyłapywanie wyjątku gdy jest problem z siecią
            {
                
                throw new Exception("Błąd podczas pobierania danych z API pogodowego.", ex);
            }
        }
    }
}
