namespace SymphonySolutions.Core.Helpers
{
    public static class TestDataHelper
    {
        private static readonly Random random = new ();

        public static int GenerateRandomInt(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}
