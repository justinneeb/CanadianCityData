using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Statistics
    {
        public Dictionary<string, CityInfo> CityCatalogue = new();
        public void DisplayCityInformation(string name)
        {
            Console.WriteLine($"{CityCatalogue[name].CityName}, {CityCatalogue[name].GetProvince()}");
            Console.WriteLine($"Population: {CityCatalogue[name].GetPopulation():n}");
            Console.WriteLine($"Longitude, Latitude: [{CityCatalogue[name].GetLocation()}]");
        }
        public void DisplayLargestPopulationCity(string province)
        {
            string city = "";
            foreach(KeyValuePair<string,CityInfo> cityInfo in CityCatalogue)
            {
               // compare cities province to user input
               if(cityInfo.Value.GetProvince() == province) {
                    if (city == "")
                    {
                        // set first city with matching province to city variable for comparison
                        city = cityInfo.Value.CityName!;
                    }
                    // compare cities population to the next city in dictionary
                    else if (CityCatalogue[city!].GetPopulation() < cityInfo.Value.GetPopulation())
                    {
                        // assign new city if the population is larger
                        city = cityInfo.Value.CityName!;
                    }
                }
            }
            Console.WriteLine($"{city} has the highest population with {CityCatalogue[city].GetPopulation():n}.");
        }
        public void DisplaySmallestPopulationCity(string province)
        {
            string city = "";
            foreach (KeyValuePair<string, CityInfo> cityInfo in CityCatalogue)
            {
                // compare cities province to user input
                if (cityInfo.Value.GetProvince() == province)
                {
                    if (city == "")
                    {
                        // set first city with matching province to city variable for comparison
                        city = cityInfo.Value.CityName!;
                    }
                    // compare cities population to the next city in dictionary
                    else if (CityCatalogue[city!].GetPopulation() > cityInfo.Value.GetPopulation())
                    {
                        // assign new city if the population is smaller
                        city = cityInfo.Value.CityName!;
                    }
                }
            }
            Console.WriteLine($"{city} has the smallest population with {CityCatalogue[city].GetPopulation():n0}.");
        }

        public void CompareCitiesPopulation(string firstCity, string secondCity)
        {
            if (CityCatalogue[firstCity].GetPopulation() > CityCatalogue[secondCity].GetPopulation())
            {
                Console.WriteLine($"{CityCatalogue[firstCity].CityName} has a larger population with {CityCatalogue[firstCity].GetPopulation():n0}, than {CityCatalogue[secondCity].CityName} with {CityCatalogue[secondCity].GetPopulation():n0}.");
            }
            else
            {
                Console.WriteLine($"{CityCatalogue[secondCity].CityName} has a larger population with {CityCatalogue[secondCity].GetPopulation():n0}, than {CityCatalogue[firstCity].CityName} with {CityCatalogue[firstCity].GetPopulation():n0}.");
            }
        }

        public void ShowCityOnMap(string city)
        {
            // get lat and long from city catalogue
            var location = CityCatalogue[city].GetLocation().Split(", ");
            // launch site if OS is windows
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo($"https://www.latlong.net/c/?lat={location[1]}&long={location[0]}") { UseShellExecute = true });
            }
            // launch site if OS is linux
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", $"https://www.latlong.net/c/?lat={location[1]}&long={location[0]}");
            }
        }

        public void CalculateDistanceBetweenCities(string firstCity, string secondCity)
        {
            // get both cities lat and long 
            var firstLocation = CityCatalogue[firstCity].GetLocation().Split(", ");
            var secondLocation = CityCatalogue[secondCity].GetLocation().Split(", ");
            // TODO
            const double R = 6371.0710;
            double firstLat = Double.Parse(firstLocation[1]) * (Math.PI / 180);
            double secondLat = Double.Parse(secondLocation[1]) * (Math.PI / 180);
            double diffLat = secondLat - firstLat;
            double diffLon = (Double.Parse(secondLocation[0]) - Double.Parse(firstLocation[0])) * (Math.PI / 180);
            double distance = 2 * R * Math.Asin(Math.Sqrt(Math.Sin(diffLat / 2) * Math.Sin(diffLat / 2) + Math.Cos(firstLat) * Math.Cos(secondLat) * Math.Sin(diffLon / 2) * Math.Sin(diffLon / 2)));
            Console.WriteLine($"The distance between {firstCity} and {secondCity} is {distance:n}.");
        }

        public void DisplayProvincePopulation(string province)
        {
            double provPop = 0;
            foreach (KeyValuePair<string, CityInfo> cityInfo in CityCatalogue)
            {
                // if province matches user input then add the population to counter
                if (cityInfo.Value.GetProvince() == province)
                {
                    provPop += cityInfo.Value.GetPopulation();
                }
            }
            Console.WriteLine($"The total population for {province} is {provPop:n0}");
        }

        public void DisplayProvinceCities(string province)
        {
            Console.WriteLine($"{province} has the following cities:");
            foreach (KeyValuePair<string, CityInfo> cityInfo in CityCatalogue)
            {
                // if province matches user input then print the city name
                if (cityInfo.Value.GetProvince() == province)
                {
                    Console.WriteLine($"\t- {cityInfo.Value.CityName}.");
                }

            }
        }

        public void RankProvincesByPopulation()
        {
            Dictionary<string, double> rankings = new();
            foreach (KeyValuePair<string, CityInfo> cityInfo in CityCatalogue)
            {
                // if rankings does not contain the province
                if (!rankings.ContainsKey(cityInfo.Value.GetProvince()))
                {
                    // add province key and population value
                    rankings[cityInfo.Value.GetProvince()] = cityInfo.Value.GetPopulation();
                }
                else
                {
                    // add population to existing province key 
                    rankings[cityInfo.Value.GetProvince()] += cityInfo.Value.GetPopulation();
                }
            }
            // sort list in descending order and print 
            foreach(KeyValuePair<string, double> province in rankings.OrderByDescending(key => key.Value))
            {
                Console.WriteLine($"{province.Key} with {province.Value:n}.");
            }
        }
        public void RankProvincesByCities()
        {
            Dictionary<string, double> rankings = new();
            foreach (KeyValuePair<string, CityInfo> cityInfo in CityCatalogue)
            {
                // if rankings does not contain the province
                if (!rankings.ContainsKey(cityInfo.Value.GetProvince()))
                {
                    // add province and set value to 1 
                    rankings[cityInfo.Value.GetProvince()] = 1;
                }
                else
                {
                    // add one to the city count at the current province key
                    rankings[cityInfo.Value.GetProvince()] += 1;
                }
            }
            // sort list in descending order and print 
            foreach (KeyValuePair<string, double> province in rankings.OrderByDescending(key => key.Value))
            {
                Console.WriteLine($"{province.Key} with {province.Value:n0}.");
            }
        }

        public void GetCapital(string province)
        {
            foreach (KeyValuePair<string, CityInfo> cityInfo in CityCatalogue)
            {
                // compare each Povince to the user input
                if (cityInfo.Value.GetProvince() == province)
                {
                    // print the province capital
                    if (cityInfo.Value.Capital)
                    {
                        Console.WriteLine(cityInfo.Value.CityName);
                    }
                }
            }
        }

        //public void DisplayTotalPopulation()
        //{
        //    double pop = 0;
        //    foreach (KeyValuePair<string, CityInfo> cityInfo in CityCatalogue)
        //    {
        //        pop += cityInfo.Value.GetPopulation();
        //    }
        //    Console.WriteLine(pop);
        //}
    }
}
