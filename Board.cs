namespace DiscoZooAnimalFinder;



// the board holding placed animals, does collision detection against known/unknown tiles
internal class Board
{
    public List<List<int>> Tiles { get; private set; } = new();
    public List<int> Counts { get; private set; } = new();
    public int Count;
    public int Width;
    public int Height;



    // constructor



    public Board(int count)
    {
        Width = 5;
        Height = 5;
        Count = count;

        MakeBoard();
        MakeCounts();
    }



    // public methods



    // updates a tile with what was found there (-1 = known empty, otherwise an animal's number).
    // returns false (and leaves the board unchanged) if the tile value isn't valid
    public bool UpdateTile(int x, int y, int tile)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height) return false;
        if (tile != -1 && (tile < 0 || tile >= Count)) return false;

        Tiles[y][x] = tile;

        if (tile != -1)
            Counts[tile]++;

        return true;
    }



    // adds an animal to the board after it's been found to be a valid position
    public void AddAnimal(int x, int y, Animal animal)
    {
        for (int i = 0; i < animal.Height; i++)
            for (int j = 0; j < animal.Width; j++)
                if (animal.Pattern[i][j] == 1)
                    Tiles[y + i][x + j] = animal.Num;
    }



    // checks if the animal fits at the given x/y by comparing its pattern against the board
    public bool ValidSpot(int x, int y, Animal animal)
    {
        if (x + animal.Width - 1 >= Width) return false; // out of bounds
        if (y + animal.Height - 1 >= Height) return false;

        int count = 0;
        for (int i = 0; i < animal.Height; i++) // loop through every tile of the animal pattern
        {
            for (int j = 0; j < animal.Width; j++)
            {
                if (animal.Pattern[i][j] == 0) continue; // empty part of the pattern, doesn't matter

                if (Tiles[y + i][x + j] == -2) continue; // unknown tile, animal could be here
                if (Tiles[y + i][x + j] == -1) return false; // known empty tile, animal can't be here

                // known tile belongs to a different animal, this spot is invalid
                if (Tiles[y + i][x + j] != animal.Num) return false;
                count++;
            }
        }

        // known tiles found don't match the known tile count for this animal, spot is invalid
        if (count != Counts[animal.Num]) return false;

        return true;
    }



    // returns a copy of this board
    public Board Copy()
    {
        Board copy = new(Count);

        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                copy.Tiles[y][x] = Tiles[y][x];

        for (int i = 0; i < Count; i++)
            copy.Counts[i] = Counts[i];

        return copy;
    }



    // prints the board to the console
    public void Print()
    {
        Console.WriteLine("      1 2 3 4 5");
        Console.WriteLine("    +-----------+");

        for (int y = 0; y < Height; y++)
        {
            Console.Write("  " + y + " | ");

            for (int x = 0; x < Width; x++)
            {
                switch (Tiles[y][x])
                {
                    case -2: Console.Write("  "); break; // unknown
                    case -1: Console.Write("x "); break; // known empty
                    default: Console.Write(Tiles[y][x] + " "); break; // animal number
                }
            }

            Console.WriteLine("|");
        }

        Console.WriteLine("    +-----------+");
    }



    // private methods



    // sets up an empty board of unknown tiles
    private void MakeBoard()
    {
        Tiles = [];

        for (int i = 0; i < Height; i++)
        {
            List<int> row = [];

            for (int j = 0; j < Width; j++)
                row.Add(-2);

            Tiles.Add(row);
        }
    }



    // sets up the tile counts for each animal
    private void MakeCounts()
    {
        Counts = [];

        for (int i = 0; i < Count; i++)
            Counts.Add(0);
    }
}
