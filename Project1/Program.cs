using Newtonsoft.Json;
using System.Collections;
using System.Xml.Serialization;


namespace Project1
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Program functionality\n");
            Console.WriteLine("- To exit program, enter 0 in either menu prompt");
            Console.WriteLine("- To start program from the begining enter \'restart\' at any prompt");
            Console.WriteLine("- You will be presented with a numbered list of options, Please enter a value, when prompted,");
            Console.WriteLine("to a coresponding file name, file type or data querying routine.");
            Console.WriteLine("Fetching list of available file names to be processed and queried...\n");

            Console.WriteLine("1) canadiancities-CSV");
            Console.WriteLine("2) canadiancities-JSON");
            Console.WriteLine("3) canadiancities-XML\n");
            Console.WriteLine("Select an option from the list above (e.g. 1, 2");

            bool condition = false;
            List<Root> data = new List<Root>();
            DataModeler dataModeler = new DataModeler();
            int choice;
            string fileChoice = "";
            do
            {
                dataModeler = new DataModeler();
                try
                {
                    // prompt user for input choice until a valid entry is entered
                    choice = Int32.Parse(Console.ReadLine()!);
                    switch (choice)
                    {
                        case 0:
                            System.Environment.Exit(1);
                            break;
                        case 1:
                            dataModeler.ParseCSV();
                            condition = true;
                            fileChoice = "canadiancities-CSV.csv";
                            break;
                        case 2:
                            dataModeler.ParseJSON();
                            condition = true;
                            fileChoice = "canadiancities-JSON.json";
                            break;
                        case 3:
                            dataModeler.ParseXML();
                            condition = true;
                            fileChoice = "canadiancities-XML.xml";
                            break;
                        default:
                            Console.WriteLine("Please enter 1, 2, or 3.");
                            condition = false;
                            break;
                    }
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Please enter 0, 1, 2, or 3.");
                }
            } while (!condition);

            Console.WriteLine($"A city catalogue has now been populated from the {fileChoice} file.\n");
            Console.WriteLine($"Fetching list of available data querying routines that can be run on the {fileChoice} file.\n");

            string cityName = "";
            string provinceName = "";
            condition = false;
            choice = 0;
            do {
                // print list of options after file choice has been made
                Console.WriteLine("1) Display City Information");
                Console.WriteLine("2) Display Province Cities");
                Console.WriteLine("3) Display Province Information");
                Console.WriteLine("4) Compare Cities");
                Console.WriteLine("5) Show City on Map");
                Console.WriteLine("6) Calculate Distance Between Two Cities");
                Console.WriteLine("7) Rank Provinces by Population");
                Console.WriteLine("8) Calculate Distance Between Two Cities\n");
                Console.WriteLine($"Select a data query routine from the list above for the {fileChoice} file (e.g. 1, 2)");
                try
                {
                    // get the user choice for which query they would like to run
                    choice = Int32.Parse(Console.ReadLine()!);
                    switch (choice)
                    {
                        case 0:
                            System.Environment.Exit(1);
                            break;
                        case 1:
                            // display city info
                            do
                            {
                                try
                                {
                                    // prompt user for city name
                                    Console.WriteLine("Please enter a city name:");
                                    cityName = Console.ReadLine()!;

                                    // if the city catalogue does not contain the city then throw an exception
                                    if (!dataModeler.statistics.CityCatalogue.ContainsKey(cityName))
                                    {
                                        throw new Exception();
                                    }
                                    // if the city is valid call display city information and set condition to true
                                    dataModeler.statistics.DisplayCityInformation(cityName);
                                    condition = true;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Please enter a valid city name");
                                }
                            } while (!condition);
                            // set condition to false to redisplay the options
                            condition = false;
                            break;
                        case 2:
                            // display province cities
                            do
                            {
                                try
                                {
                                    // prompt user for the province name
                                    Console.WriteLine("Please enter a province name:");
                                    provinceName = Console.ReadLine()!;

                                    if (provinceName == "Quebec") {
                                        provinceName = "Québec";
                                    }
                                    // get list that contains only cities in that province
                                    var provinceList = dataModeler.statistics.CityCatalogue.Where(x => x.Value.GetProvince() == provinceName);

                                    // the list count is less than one than the input was not a valid province
                                    if (provinceList.Count() < 1)
                                    {
                                        throw new Exception();
                                    }
                                    // province is valid so call display province cities and set condition to true
                                    dataModeler.statistics.DisplayProvinceCities(provinceName);
                                    condition = true;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Please enter a valid province name");
                                }
                            } while (!condition);
                            // set condition to false to redisplay the options
                            condition = false;
                            break;
                        case 3:
                            // display province information
                            do
                            {
                                try
                                {
                                    // prompt the user for a province 
                                    Console.WriteLine("Please enter a province name:");
                                    provinceName = Console.ReadLine()!;
                                    if (provinceName == "Quebec")
                                    {
                                        provinceName = "Québec";
                                    }
                                    // get list that contains only cities in that province
                                    var provinceList = dataModeler.statistics.CityCatalogue.Where(x => x.Value.GetProvince() == provinceName);
                                    // the list count is less than one than the input was not a valid province
                                    if (provinceList.Count() < 1) {
                                        throw new Exception();
                                    }
                                    // province is valid so call methods and set condition to true
                                    dataModeler.statistics.DisplayProvincePopulation(provinceName);
                                    dataModeler.statistics.DisplayLargestPopulationCity(provinceName);
                                    dataModeler.statistics.DisplaySmallestPopulationCity(provinceName);
                                    dataModeler.statistics.GetCapital(provinceName);
                                    condition = true;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Please enter a valid province name");
                                }
                            } while (!condition);
                            // set condition to false to redisplay the options
                            condition = false;
                            break;
                        case 4:
                            // compare cities
                            do
                            {
                                try
                                {
                                    // prompt user for 2 cities that are comma separated
                                    Console.WriteLine("Please enter two city names that are comma separated");
                                    string[] cities = Console.ReadLine()!.Split(',');

                                    // if either of the cities is not in the list throw an exception
                                    if (!dataModeler.statistics.CityCatalogue.ContainsKey(cities[0]) || !dataModeler.statistics.CityCatalogue.ContainsKey(cities[1]))
                                    {
                                        throw new Exception();
                                    }
                                    // both cities are valid so call compare and set condition to true
                                    dataModeler.statistics.CompareCitiesPopulation(cities[0], cities[1]);
                                    condition = true;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Please enter two valid cities");
                                }
                            } while (!condition);
                            // set condition to false to redisplay the options
                            condition = false;
                            break;
                        case 5:
                            // show city on map
                            do
                            {
                                try
                                {
                                    // prompt the user for a city name
                                    Console.WriteLine("Please enter a city name:");
                                    cityName = Console.ReadLine()!;
                                    // if the city catalogue does not contain the city then throw an exception
                                    if (!dataModeler.statistics.CityCatalogue.ContainsKey(cityName))
                                    {
                                        throw new Exception();
                                    }
                                    // the city is valid so call show city on map and set condition to true
                                    dataModeler.statistics.ShowCityOnMap(cityName);
                                    condition = true;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Please enter a valid city name");
                                }
                            } while (!condition);
                            // set condition to false to redisplay the options
                            condition = false;
                            break;
                        case 6:
                            // calc distance between cities
                            do
                            {
                                try
                                {
                                    // prompt the user for two cities separated by a comma
                                    Console.WriteLine("Please enter two city names that are comma separated");
                                    string[] cities = Console.ReadLine()!.Split(',');
                                    // if either of the cities is not in the list throw an exception
                                    if (!dataModeler.statistics.CityCatalogue.ContainsKey(cities[0]) || !dataModeler.statistics.CityCatalogue.ContainsKey(cities[1]))
                                    {
                                        throw new Exception();
                                    }
                                    // both cities are valid so call calc distance and set condition to true
                                    dataModeler.statistics.CalculateDistanceBetweenCities(cities[0], cities[1]);
                                    condition = true;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Please enter two valid cities");
                                }
                            } while (!condition);
                            // set condition to false to redisplay the options
                            condition = false;
                            break;
                        case 7:
                            // rank provinces by population
                            do
                            {
                                try
                                {
                                    dataModeler.statistics.RankProvincesByPopulation();
                                    condition = true;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Something went wrong, please try again.");
                                }
                            } while (!condition);
                            // set condition to false to redisplay the options
                            condition = false;
                            break;
                        case 8:
                            // rank provinces by number of cities
                            do
                            {
                                try
                                {
                                    dataModeler.statistics.RankProvincesByCities();
                                    condition = true;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Something went wrong, please try again.");
                                }
                            } while (!condition);
                            // set condition to false to redisplay the options
                            condition = false;
                            break;
                        default:
                            Console.WriteLine("Please enter 1, 2, or 3.");
                            condition = false;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter a number from 0 to 8");
                }
                Console.WriteLine();
            } while (!condition);
        } // end of main
    } // end of Program
} // end of Namespace