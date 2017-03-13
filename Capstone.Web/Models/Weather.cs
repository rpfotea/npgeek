using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }
        public string Recomendation { get; set; }
        public string TempUnit { get; set; }
        
        
        public string GetRecomendation(string forcast, int min, int max)
        {
            string output = "";
            if (forcast=="snow")
            {
                output =output + "Pack snowshoes.";
            }
            if (forcast == "rain")
            {
                output = output + "Pack rain gear and wear waterproof shoes.";
            }
            if (forcast == "thunderstorms")
            {
                output = output + "Seek shelter and avoid hiking on exposed ridges.";
            }
            if (forcast == "sun")
            {
                output = output + "Pack sunblock.";
            }
            if(min<20)
            {
                output = output + "Dangers of exposure to frigid temperatures.";
            }
            if (min > 75)
            {
                output = output + "Bring an extra gallon of water.";
            }
            if (max-min>20)
            {
                output = output + "Wear breathable layers.";
            }

            return output;
        }

        public int GetDisplayTemperature(int temp)
        {
            if(TempUnit== "fahrenheit")
            {
                return temp;
            }
            else
            {
                return (int)((temp - 32) / 1.8);
            }
            
        }


        public int ConverterFToC(int temp)
        {
            return  (int)((temp - 32) / 1.8);
        }
                      
    }
}