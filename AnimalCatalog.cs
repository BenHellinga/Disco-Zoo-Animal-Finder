namespace DiscoZooAnimalFinder;



// static lookup that associates each AnimalType with its display name and its shape
internal static class AnimalCatalog
{
    // layout of each animal, read as a flat 4x4 grid
    private static readonly Dictionary<AnimalType, string> Layouts = new()
    {
        // all
        [AnimalType.Bux] = "1000 0000 0000 0000",

        // farm
        [AnimalType.Sheep] = "1111 0000 0000 0000",
        [AnimalType.Pig] = "1100 1100 0000 0000",
        [AnimalType.Rabbit] = "1000 1000 1000 1000",
        [AnimalType.Horse] = "1000 1000 1000 0000",
        [AnimalType.Cow] = "1110 0000 0000 0000",
        [AnimalType.Unicorn] = "1000 0110 0000 0000",
        [AnimalType.Chicken] = "1000 0100 0010 0000",

        // outback
        [AnimalType.Kangaroo] = "1000 0100 0010 0001",
        [AnimalType.Platypus] = "1100 0110 0000 0000",
        [AnimalType.Crocodile] = "1111 0000 0000 0000",
        [AnimalType.Koala] = "1100 0100 0000 0000",
        [AnimalType.Cockatoo] = "1000 0100 0100 0000",
        [AnimalType.Tiddalik] = "0100 1010 0000 0000",
        [AnimalType.Echidna] = "0010 1100 0000 0000",

        // savanna
        [AnimalType.Zebra] = "0100 1010 0100 0000",
        [AnimalType.Hippo] = "1010 0000 1010 0000",
        [AnimalType.Giraffe] = "1000 1000 1000 1000",
        [AnimalType.Lion] = "1110 0000 0000 0000",
        [AnimalType.Elephant] = "1100 1000 0000 0000",
        [AnimalType.Gryphon] = "1010 0100 0000 0000",
        [AnimalType.Rhinoceros] = "0100 1000 0100 0000",

        // northern
        [AnimalType.Bear] = "1100 0100 0100 0000",
        [AnimalType.Skunk] = "0110 1100 0000 0000",
        [AnimalType.Beaver] = "0010 1100 0010 0000",
        [AnimalType.Moose] = "1010 0100 0000 0000",
        [AnimalType.Fox] = "1100 0010 0000 0000",
        [AnimalType.Sasquatch] = "1000 1000 0000 0000",
        [AnimalType.Otter] = "1000 1100 0000 0000",

        // polar
        [AnimalType.Penguin] = "0100 0100 1010 0000",
        [AnimalType.Seal] = "1000 0101 0010 0000",
        [AnimalType.Muskox] = "1100 1010 0000 0000",
        [AnimalType.PolarBear] = "1010 0010 0000 0000",
        [AnimalType.Walrus] = "1000 0110 0000 0000",
        [AnimalType.Yeti] = "1000 0000 1000 0000",
        [AnimalType.SnowyOwl] = "1100 0100 0000 0000",

        // jungle
        [AnimalType.Monkey] = "1010 0101 0000 0000",
        [AnimalType.Toucan] = "0100 1000 0100 0100",
        [AnimalType.Gorilla] = "1010 1010 0000 0000",
        [AnimalType.Panda] = "0010 1000 0010 0000",
        [AnimalType.Tiger] = "1011 0000 0000 0000",
        [AnimalType.Phoenix] = "1000 0000 0010 0000",
        [AnimalType.Lemur] = "1000 0100 1000 0000",

        // jurassic
        [AnimalType.Diplodocus] = "1000 0110 0100 0000",
        [AnimalType.Stegosaurus] = "0110 1001 0000 0000",
        [AnimalType.Raptor] = "1100 0100 0010 0000",
        [AnimalType.TRex] = "1000 0000 1100 0000",
        [AnimalType.Triceratops] = "1000 0010 1000 0000",
        [AnimalType.Dragon] = "1000 0010 0000 0000",
        [AnimalType.Ankylosaurus] = "0010 1010 0000 0000",

        // ice age
        [AnimalType.WoolyRhino] = "0010 1001 0100 0000",
        [AnimalType.GiantSloth] = "1000 0010 1010 0000",
        [AnimalType.DireWolf] = "0100 1001 0100 0000",
        [AnimalType.SaberTooth] = "1000 0010 0100 0000",
        [AnimalType.Mammoth] = "0100 1000 0010 0000",
        [AnimalType.Akhlut] = "0010 1000 0010 0000",
        [AnimalType.YukonCamel] = "0010 1000 0001 0000",

        // city
        [AnimalType.Raccoon] = "1010 1001 0000 0000",
        [AnimalType.Pigeon] = "1000 0100 0110 0000",
        [AnimalType.Rat] = "1100 0101 0000 0000",
        [AnimalType.Squirrel] = "0010 1000 0100 0000",
        [AnimalType.Opossum] = "1000 1010 0000 0000",
        [AnimalType.SewerTurtle] = "1100 0000 0000 0000",
        [AnimalType.Chipmunk] = "0100 1001 0000 0000",

        // mountain
        [AnimalType.Goat] = "1000 1110 0000 0000",
        [AnimalType.Cougar] = "1000 0100 1010 0000",
        [AnimalType.Elk] = "1010 0110 0000 0000",
        [AnimalType.Eagle] = "1000 1000 0100 0000",
        [AnimalType.Coyote] = "1100 0010 0000 0000",
        [AnimalType.Aatxe] = "0010 1000 0000 0000",
        [AnimalType.Pika] = "1010 0010 0000 0000",

        // moon
        [AnimalType.Moonkey] = "1000 1010 0010 0000",
        [AnimalType.LunarTick] = "0100 0000 0100 1010",
        [AnimalType.Tribble] = "0100 1110 0000 0000",
        [AnimalType.Moonicorn] = "1000 1100 0000 0000",
        [AnimalType.LunaMoth] = "1010 0000 0100 0000",
        [AnimalType.JadeRabbit] = "1000 0000 0100 0000",
        [AnimalType.Babmoon] = "0100 0010 1000 0000",

        // mars
        [AnimalType.Marsmot] = "0100 0100 1100 0000",
        [AnimalType.Marsmoset] = "1010 0010 0100 0000",
        [AnimalType.Rock] = "1100 1100 0000 0000",
        [AnimalType.Rover] = "0100 1010 0000 0000",
        [AnimalType.Martian] = "1010 0100 0000 0000",
        [AnimalType.Marsmallow] = "1000 0000 1000 0000",
        [AnimalType.Marsten] = "1010 0001 0000 0000",
    };



    // display names, used both for parsing user input and for printed output.
    // only listed here when it differs from the lowercased enum name (eg multi-word animals)
    private static readonly Dictionary<AnimalType, string> DisplayNameOverrides = new()
    {
        [AnimalType.PolarBear] = "polar bear",
        [AnimalType.SnowyOwl] = "snowy owl",
        [AnimalType.TRex] = "t rex",
        [AnimalType.WoolyRhino] = "wooly rhino",
        [AnimalType.GiantSloth] = "giant sloth",
        [AnimalType.DireWolf] = "dire wolf",
        [AnimalType.SaberTooth] = "saber tooth",
        [AnimalType.YukonCamel] = "yukon camel",
        [AnimalType.SewerTurtle] = "sewer turtle",
        [AnimalType.LunarTick] = "lunar tick",
        [AnimalType.LunaMoth] = "luna moth",
        [AnimalType.JadeRabbit] = "jade rabbit",
    };



    // lazily built reverse lookup for parsing (display name -> type)
    private static readonly Dictionary<string, AnimalType> NameLookup = BuildNameLookup();



    private static Dictionary<string, AnimalType> BuildNameLookup()
    {
        Dictionary<string, AnimalType> lookup = new();

        foreach (AnimalType type in Enum.GetValues<AnimalType>())
            lookup[GetName(type)] = type;

        return lookup;
    }



    // the 4x4 pattern layout string for this animal
    public static string GetLayout(AnimalType type) => Layouts[type];



    // the human readable, lowercase name for this animal (eg "polar bear", "sheep")
    public static string GetName(AnimalType type)
    {
        if (DisplayNameOverrides.TryGetValue(type, out string? overrideName))
            return overrideName;

        return type.ToString().ToLowerInvariant();
    }



    // tries to parse user-typed text (case-insensitive, whitespace trimmed) into a known animal
    public static bool TryParse(string input, out AnimalType type)
    {
        return NameLookup.TryGetValue(input.Trim().ToLowerInvariant(), out type);
    }
}
