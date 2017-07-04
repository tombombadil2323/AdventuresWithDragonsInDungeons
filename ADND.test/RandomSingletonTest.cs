using NUnit.Framework;
using System;
namespace ADND.test
{
    [TestFixture()]
    public class RandomSingletonTest
    {
        [Test()]
        public void ShouldNotBeInstantiatedMoreThanOnce()
        {
            RandomSingleton randomSingletonA = RandomSingleton.Instance();
            RandomSingleton randomSingletonB = RandomSingleton.Instance();

            Assert.AreEqual(randomSingletonA,randomSingletonB);

        }

		[Test()]
		public void ShouldBeBetween1And5()
		{
			RandomSingleton randomSingletonA = RandomSingleton.Instance();
			int firstRandom = randomSingletonA.Next(1, 5);

			Assert.GreaterOrEqual(firstRandom, 1);
			Assert.LessOrEqual(firstRandom, 5);
		}
    }
}
