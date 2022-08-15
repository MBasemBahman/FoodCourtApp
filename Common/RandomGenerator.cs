using System.Text;

namespace Common
{
    public static class RandomGenerator
    {
        // Generate a random number between two numbers    
        public static int RandomNumber(int min, int max)
        {
            Random random = new();
            return random.Next(min, max);
        }

        // Generate a random string with a given size    
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new();
            Random random = new();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor((26 * random.NextDouble()) + 65)));
                _ = builder.Append(ch);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        // Generate a random key    
        public static string RandomKey()
        {
            StringBuilder builder = new();
            _ = builder.Append(RandomString(4, true));
            _ = builder.Append(RandomNumber(1000, 9999));
            _ = builder.Append(RandomString(2, true));
            return builder.ToString();
        }

        public static List<T> Randomize<T>(this IEnumerable<T> source)
        {
            List<T> array = source.OrderBy(a => Guid.NewGuid()).ToList();
            // randomize indexes (several approaches are possible)
            return array;
        }
    }
}
