using System;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;

namespace OpenWeatherMap
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = File.ReadAllText("appsettings.json");
            var apikey = JObject.Parse(key).GetValue("APIKEY").ToString();
            Console.WriteLine("What city would you like to know the weather?");
            var input = Console.ReadLine();
            var weatherUrl = "https://api.openweathermap.org/data/2.5/weather?q="+ input +"&units=imperial&appid=" + apikey;
            var weatherClient = new HttpClient();
            //send a get request to the url and get the json as a string and the result returns it to us as a string
            try
            {
                var weatherResponse = weatherClient.GetStringAsync(weatherUrl).Result;
                var temp = double.Parse(JObject.Parse(weatherResponse)["main"]["temp"].ToString());
                Console.WriteLine($"The current weather in {input.ToUpper()} is {temp} degrees fahrenheit");
            }
            catch
            {
                Console.WriteLine("City entered is not valid");
            }

        }
    }
}
