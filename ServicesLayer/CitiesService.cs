namespace ServicesLayer
{
    public class CitiesService
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

        public List<string> GetCitites() { 
        return _cities;
        }
    }
}
