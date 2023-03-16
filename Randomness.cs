namespace ASCII
{
    public static class Randomness {
        
        public enum Loot
        {
            Gift,
            Coin,
            Diamond,
            Scroll
        } 
        public enum Element
        {
            Fire,
            Water,
            Earth,
            Air
        };

        public enum Rarity : int
        {
            Mythic = 5,
            Legendary = 10,
            Rare = 50,
            Common = 55
        };

        public static Rarity GetRarity(Random rng) {
            int randomNumber = rng.Next(1, 100);
            Console.WriteLine($"Rarity Number: {randomNumber}");
            Rarity selectedEnum = Rarity.Common;

            foreach (Rarity value in Enum.GetValues(typeof(Rarity)))
            {
                Console.WriteLine($"{value}:{(int)value}");
                if (randomNumber <= (int)value)
                {
                    selectedEnum = value;
                    break;
                }
            }
            return selectedEnum;
        }

        public static Element GetElement(Random rng) {
            int randomNumber = rng.Next(Enum.GetNames(typeof(Element)).Length);
            Console.WriteLine($"Element Number: {randomNumber}");
            return (Element)randomNumber;
        }
    }
}
