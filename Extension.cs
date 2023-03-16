namespace ASCII
{
    public static class Extension
    {
        public static char GetRandomItem(string text, Random rng)
        {
            int index = rng.Next(text.Length);
            return text[index];
        }
        public static ImageMagick.MagickColor GetRandomItem(ImageMagick.MagickColor[] array, Random rng)
        {
            int index = rng.Next(array.Length);
            return array[index];
        }

        public static string GetRandomItem(string[] array, Random rng)
        {
            int index = rng.Next(array.Length);
            return array[index];
        }
    }
}