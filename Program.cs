using System.Diagnostics;
using System.Reflection;
using ASCII;
using System.Security.Cryptography;
using static ASCII.Randomness;
using static ASCII.Colors;

//
// Config
//
// Text to replacement
const string replacementLetters = "$%^*♥";

//Fonts Folder
//string fontsFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location) + @"\fonts\";
//var path = Directory.GetFiles(fontsFolder);

// Import template

for (int prints = 0; prints < 9; prints++) {
var seed = Guid.NewGuid().GetHashCode();
Random rand = new(seed);
    // Main scale
    const float scale = 2;
    const int textscale = 2;
    const int imageHeight = 512;
    const int imageWidth = 512;

    int dragonLineHeight = 48*textscale;
    int dragonFontPointSize = 40*textscale;

    int dragonStartCoordx = (int)(256*scale);
    int dragonStartCoordy = (int)(128*scale);

    int xCoordEmoji = (int)(224*scale);
    int yCoordEmoji = (int)(332*scale);

    string template = Templates.commonDragon;
    Loot treasure = Loot.Gift;
    // Get Rarity and Element
    var element = Randomness.GetElement(rand);
    var rarity = Randomness.GetRarity(rand);

    var colorTable = Colors.allColors;
    switch(element){
        case Element.Fire:
            colorTable = Colors.fireColors;
            break;
        case Element.Earth:
            colorTable = Colors.earthColors;
            break;
        case Element.Water:
            colorTable = Colors.waterColors;
            break;
        case Element.Air:
            colorTable = Colors.airColors;
            break;
    }
    switch(rarity){
        case Rarity.Common:
            treasure = Loot.Gift;
            template = Templates.commonDragon;
            dragonLineHeight = 48*textscale;
            dragonFontPointSize = 40*textscale;
            xCoordEmoji = (int)(224*scale);
            yCoordEmoji = (int)(330*scale);
            break;
        case Rarity.Rare:
            treasure = Loot.Coin;
            template = Templates.rareDragon;
            dragonLineHeight = 16*textscale;
            dragonFontPointSize = 16*textscale;
            xCoordEmoji = (int)(240*scale);
            yCoordEmoji = (int)(320*scale);
            break;
        case Rarity.Legendary:
            template = Templates.legendaryDragon;
            break;
        case Rarity.Mythic:
            template = Templates.mythicalDragon;
            break;
    }
    // Create image + base
    var image = new ImageMagick.MagickImage(ImageMagick.MagickColors.Black, (int)(imageWidth*scale), (int)(imageHeight*scale));

    template = template
        .Replace('#', Extension.GetRandomItem(replacementLetters, rand))
        .Replace('&', Extension.GetRandomItem(replacementLetters, rand));

    // Drawing Text
    var templateLines = template.Split('!');

    void drawToImage(int xCoord, int yCoord, string line, ImageMagick.MagickColor color, int fontSize) {
        new ImageMagick.Drawables()
            .TextAlignment(ImageMagick.TextAlignment.Center)
            .FontPointSize(fontSize)
            //.Gravity(ImageMagick.Gravity.Center)
            .Font("Consolas") //Bahnschrift
            .FillColor(color)
            .Text(xCoord, yCoord, line)
            .Draw(image);
    }

    int x = dragonStartCoordx;
    int lineHeight = dragonLineHeight;
    int y = (x / 2) - lineHeight;

    for (int i = 0; i < templateLines.Length; i++) {
        drawToImage(x, y + (i * lineHeight), templateLines[i], Extension.GetRandomItem(colorTable, rand), dragonFontPointSize);
    }
    drawToImage(x,y+(96*7), $"{Enum.GetName(element)}:{rarity}", ImageMagick.MagickColors.White, 80);
    
    // Assign Random Emoji
    // var emojiDirectory = Directory.GetFiles("dragon-loot-emojis");
    // var emojiFile = Extension.GetRandomItem(emojiDirectory, rand);

    // Assign Loot/Treasure
    string emojiFile = $@"\{Environment.CurrentDirectory.Replace(@"C:\", "")}\dragon-loot-emojis\{treasure.ToString()}.png";
    image.Composite(new ImageMagick.MagickImage(File.ReadAllBytes(emojiFile)), xCoordEmoji, yCoordEmoji);
    
    // Assign Element file
    // var elementsDirectory = Directory.GetFiles("elements");
    // const int xCoordElement = 24; //232
    // const int yCoordElement = 24; //224
    //var elementFile = Extension.GetRandomItem(elementsDirectory, rand);
    //image.Composite(new ImageMagick.MagickImage(File.ReadAllBytes(elementFile)), xCoordElement, yCoordElement);
    // Output
    image.Write("output.jpg");

/* Random MD5 Hash rename
    using var md5 = MD5.Create();
    using var stream = File.OpenRead("output.jpg");
    var md5hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
    using var savestream = File.Create($@"\{Environment.CurrentDirectory.Replace(@"C:\", "")}\output\{md5hash}.jpg");
    image.Write(savestream, ImageMagick.MagickFormat.Jpg);
*/
// Seed rename
    using var savestream = File.Create($@"\{Environment.CurrentDirectory.Replace(@"C:\", "")}\output\{seed}.jpg");
    image.Write(savestream, ImageMagick.MagickFormat.Jpg);
}
