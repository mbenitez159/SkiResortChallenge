using System.Configuration;

namespace SkiResortChallenge.Helper
{
    public static class Settings
    {
        public static string Matrix_4_4
        {
            get => ConfigurationManager.AppSettings["4x4"]?.ToString();
        }
        public static string Matrix_1000_1000
        {
            get => ConfigurationManager.AppSettings["1000*1000"]?.ToString();
        }
    }
}
