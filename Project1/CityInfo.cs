using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class CityInfo
    {
        internal int CityID { get; set; }
        internal string? CityName { get; set; }
        internal string? CityAscii { get; set; }
        private double Population { get; set; }
        private string? Province { get; set; }
        private double? Latitude { get; set; }
        private double? Longitude { get; set; }
        public bool Capital { get; set; }
        public CityInfo(int CityID, string CityName, string CityAscii, double Population, string Province, double Latitude, double Lonitude, bool Capital)
        {
            this.CityID = CityID;
            this.CityName = CityName;
            this.CityAscii = CityAscii;
            this.Population = Population;
            this.Province = Province;
            this.Latitude = Latitude;
            this.Longitude = Lonitude;
            this.Capital = Capital;
        }
        internal string GetProvince()
        {
            return Province!;
        }
        public double GetPopulation()
        {
            return Population;
        }
        public string GetLocation()
        {
            return Longitude + ", " + Latitude;
        }

    }
}
