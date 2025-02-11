using System.Text;

namespace SymphonySolutions.Core.Helpers
{
    public static class TestDataHelper
    {
        private static readonly Random _random = new ();

        private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const string Numbers = "0123456789";

        public static int GenerateRandomInt(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }

        public static string GenerateRandomName(int length = 10)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                
                if (_random.Next(2) > 0)
                {
                    sb.Append(Letters[_random.Next(Letters.Length)]);
                }
                else
                {
                    sb.Append(Numbers[_random.Next(Numbers.Length)]);
                }
            }
            return sb.ToString();
        }
    }
}
