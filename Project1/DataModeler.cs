using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Project1
{
    public class Root
    {
        public string? city { get; set; }
        public string? city_ascii { get; set; }
        public string? lat { get; set; }
        public string? lng { get; set; }
        public string? country { get; set; }
        public string? admin_name { get; set; }
        public string? capital { get; set; }
        public string? population { get; set; }
        public string? id { get; set; }
    }

    [XmlRoot(ElementName = "CanadaCity")]
    public class CanadaCity
    {

        [XmlElement(ElementName = "city")]
        public string? City { get; set; }

        [XmlElement(ElementName = "city_ascii")]
        public string? CityAscii { get; set; }

        [XmlElement(ElementName = "lat")]
        public double Lat { get; set; }

        [XmlElement(ElementName = "lng")]
        public double Lng { get; set; }

        [XmlElement(ElementName = "country")]
        public string? Country { get; set; }

        [XmlElement(ElementName = "admin_name")]
        public string? AdminName { get; set; }

        [XmlElement(ElementName = "capital")]
        public string? Capital { get; set; }

        [XmlElement(ElementName = "population")]
        public int Population { get; set; }

        [XmlElement(ElementName = "id")]
        public int Id { get; set; }
    }
    [XmlRoot(ElementName = "CanadaCities")]
    public class CanadaCities
    {

        [XmlElement(ElementName = "CanadaCity")]
        public List<CanadaCity>? CanadaCity { get; set; }

        public int Count()
        {
            return CanadaCity!.Count();
        }

    }
    public class DataModeler
    {
        public Statistics statistics = new();
        public void ParseXML()
        {
            CanadaCities cities;
            // read in all text
            string response = System.IO.File.ReadAllText("./Data/Canadacities-XML.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(CanadaCities));
            // serialize the data using the CanadaCities class
            using (StringReader reader = new StringReader(response))
            {
                cities = (CanadaCities)serializer.Deserialize(reader)!;
            }
            //data = new List<Root>();
            for (int i = 0; i < cities.Count(); i++)
            {
                if (cities.CanadaCity![i].City != null)
                {
                    bool capital = cities.CanadaCity![i].Capital! == "" ? false : true;
                    CityInfo city = new(cities.CanadaCity![i].Id, cities.CanadaCity![i].City!, cities.CanadaCity![i].CityAscii!, cities.CanadaCity![i].Population, cities.CanadaCity![i].AdminName!, cities.CanadaCity![i].Lat, cities.CanadaCity![i].Lng, capital);
                    try
                    {
                        if (statistics.CityCatalogue.ContainsKey(cities.CanadaCity[i].City!))
                        {
                            statistics.CityCatalogue[cities.CanadaCity[i].City!.ToLower()] = city;
                        }
                        else
                        {
                            statistics.CityCatalogue[cities.CanadaCity[i].City!] = city;
                        }
                    } catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                
            }
        }

        public void ParseJSON()
        {
            string response = System.IO.File.ReadAllText("./Data/Canadacities-JSON.json");
            List<Root> json = JsonConvert.DeserializeObject<List<Root>>(response)!;
            foreach (Root city in json)
            {

                CityInfo info;
                if (city.city == null || city.id == null || city.city_ascii == null || city.admin_name == null || city.population == null || city.lat == null || city.lng == null)
                {
                    continue;
                }
                else
                {
                    bool capital = city.capital!.ToString() == "" ? false : true;
                    info = new(Int32.Parse(city.id!), city.city!, city.city_ascii!, Double.Parse(city.population!), city.admin_name!, Double.Parse(city.lat!), Double.Parse(city.lng!), capital);
                }
                try
                {
                    if (statistics.CityCatalogue.ContainsKey(city.city!))
                    {
                        statistics.CityCatalogue[city.city!.ToLower()] = info;
                    }
                    else
                    {
                        statistics.CityCatalogue[city.city!] = info;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void ParseCSV()
        {
            string response = System.IO.File.ReadAllText("./Data/Canadacities.csv");
            var cities = response.Split('\n');
            //data = new List<Root>();
            for (int i = 1; i < cities.Count() - 1; i++)
            {
                var city = cities[i].Split('\u002c');
                bool capital = city[6] == "" ? false : true;
                CityInfo info = new(Int32.Parse(city[8]), city[0], city[1], Double.Parse(city[7]), city[5], Double.Parse(city[2]), Double.Parse(city[3]), capital);
                try
                {
                    if (statistics.CityCatalogue.ContainsKey(city[0]!))
                    {
                        statistics.CityCatalogue[city[0]!.ToLower()] = info;
                    }
                    else
                    {
                        statistics.CityCatalogue[city[0]!] = info;
                    }
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
