namespace DiscoZooAnimalFinder;



// info about an animal, its layout, and the computed pattern grid
internal class Animal
{
    public List<List<int>> Pattern { get; private set; } = new();
    public int Num;
    public int Width;
    public int Height;
    public AnimalType Type;
    public string Name => AnimalCatalog.GetName(Type);
    public string Layout;



    // constructor



    public Animal(AnimalType type, int num)
    {
        Type = type;
        Layout = AnimalCatalog.GetLayout(type);
        Num = num;

        Init();
    }



    // public methods



    // reads the layout string and fills in width, height, and the pattern grid
    public void Init()
    {
        Width = 1;
        Height = 1;

        for (int y = 0; y < 4; y++) // find the max width/height used by the pattern
        {
            for (int x = 0; x < 4; x++)
            {
                if (Layout[y * 5 + x] - 48 == 1)
                {
                    Width = Math.Max(Width, x + 1);
                    Height = Math.Max(Height, y + 1);
                }
            }
        }

        Pattern = [];

        for (int y = 0; y < Height; y++)
        {
            List<int> row = [];

            for (int x = 0; x < Width; x++)
                row.Add(Layout[y * 5 + x] - 48);

            Pattern.Add(row);
        }
    }
}
