using ServicesContract;

namespace ServicesLayer
{
    public class CitiesService:ICitiesService
    {
        private List<string> _cities;
        public CitiesService()
        {
            _cities = new List<string>()
            {
                "Cairo",
                "Alexandria",
                "Qalybia",
                "Luxor",
                "Sinaa"
            };
        }

        public List<string> GetCities() { 
        return _cities;
        }
    }
}
