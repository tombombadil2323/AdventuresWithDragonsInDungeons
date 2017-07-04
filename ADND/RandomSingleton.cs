using System;
namespace ADND
{
    public class RandomSingleton
    {
        private static RandomSingleton checkInstance;
        private Random randomGenerator;

        protected RandomSingleton()
        {
            randomGenerator = new Random();
        }
        public static RandomSingleton Instance()
        {
            if (checkInstance == null)
            {
                checkInstance = new RandomSingleton();
            }
            return checkInstance;
        }

        public int Next(int min, int max)
        {
            return randomGenerator.Next(min, max);
        }
    }
}
