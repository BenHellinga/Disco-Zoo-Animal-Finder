namespace DiscoZooAnimalFinder;



// automates a mobile game called 'disco zoo'
//
// in the game you find hidden animal patterns on a 5x5 grid, there can be 1 to 3 animals each
// with their own shape. this tries every possible arrangement of the given animals, counts how
// often each tile is covered, and prints the odds an animal is on each tile
//
// also shows the total solution count, per tile counts, and a heatmap
internal class Solver
{
    public List<Animal> Animals { get; private set; } = new();
    public List<List<int>> Counts { get; private set; } = new();

    public int Total;
    public int max;
    public int maxX;
    public int maxY;
    public string End;



    // main



    // asks for input, then parses it and finds the probability of an animal being on each tile
    static void Main()
    {
        while (true)
        {
            Solver solver = new();
            Board board = new(solver.Animals.Count);

            while (true)
            {
                solver.Total = 0;
                solver.InitCounts();

                solver.Solve(board, 0);

                Console.WriteLine();
                solver.PrintInfo(board);

                Console.WriteLine();
                solver.PromptUpdate(board);

                if (solver.End == "done") return;
                if (solver.End == "reset") break;
            }
        }
    }



    // constructor



    public Solver()
    {
        while (true)
        {
            // names must match a known animal (see AnimalCatalog)
            Console.WriteLine("Please enter the animals (name, name, name)");
            string input = Console.ReadLine() ?? "";

            string[] names = input.Split(',');

            for (int i = 0; i < names.Length; i++)
                names[i] = names[i].Trim();

            bool correct = true;
            List<Animal> animals = [];

            for (int i = 0; i < names.Length; i++) // build an animal for each name
            {
                if (!AnimalCatalog.TryParse(names[i], out AnimalType type))
                {
                    correct = false;
                    break;
                }

                animals.Add(new Animal(type, i));
            }

            if (!correct || animals.Count == 0)
            {
                Console.WriteLine("There was a problem with one of the inputted names");
                continue;
            }

            Animals = animals;
            break;
        }

        End = "";
    }



    // public methods



    // recursively tries every combination of the animals
    public void Solve(Board board, int animal)
    {
        // end of the recursion, tally this arrangement
        if (animal == Animals.Count)
        {
            for (int y = 0; y < board.Height; y++)
                for (int x = 0; x < board.Width; x++)
                    if (board.Tiles[y][x] >= 0)
                        Counts[y][x]++;

            Total++;
            return;
        }

        // try every location for this animal
        for (int y = 0; y < board.Height; y++)
        {
            for (int x = 0; x < board.Width; x++)
            {
                if (board.ValidSpot(x, y, Animals[animal]))
                {
                    Board newBoard = board.Copy();
                    newBoard.AddAnimal(x, y, Animals[animal]);
                    Solve(newBoard, animal + 1);
                }
            }
        }
    }



    // prints how often an animal appears on each tile, the total solutions, and a heatmap
    public void PrintInfo(Board board)
    {
        if (Total <= 0)
        {
            Console.WriteLine("No solution found");
            return;
        }

        max = 0;
        maxX = 0;
        maxY = 0;

        for (int y = 0; y < board.Height; y++)
        for (int x = 0; x < board.Width; x++)
        {
            if (board.Tiles[y][x] != -2) continue; // already known, not a candidate

            if (Counts[y][x] > max)
            {
                max = Counts[y][x];
                maxX = x;
                maxY = y;
            }
        }

        string Header(int columnWidth, int paddingWidth)
        {
            string header = "  ";

            for (int i = 0; i < board.Width; i++)
            {
                header += new string(' ', columnWidth - 1);
                header += $"{i + 1}";

                if (i != board.Width - 1)
                    header += new string(' ', paddingWidth);
            }

            header += ' ';
            return header;
        }

        string Border(int columnWidth, int paddingWidth)
        {
            string header = "+";
            header += new string('-', board.Width * columnWidth + (board.Width - 1) * paddingWidth + 2);
            return header;
        }

        string BoardRow(int r)
        {
            string row = " ";

            for (int i = 0; i < board.Width; i++)
            {
                row += board.Tiles[r][i] switch
                {
                    -2 => "  ",
                    -1 => ". ",
                    int t => $"{t} ",
                };
            }

            return row;
        }

        string HeatRow(int r)
        {
            const string SHADING = " ░▒▓█";;
            string row = " ";

            for (int i = 0; i < board.Width; i++)
            {
                char c = SHADING[(int)Math.Ceiling(Counts[r][i] / (double)Total * (SHADING.Length - 1))];
                row += new string(c, 2);
            }
            row += " ";

            return row;
        }

        string CountRow(int r, int maxDigits)
        {
            string row = " ";

            for (int i = 0; i < board.Width; i++)
            {
                if (board.Tiles[r][i] == -2)
                    row += Counts[r][i].ToString().PadLeft(maxDigits) + " ";
                else
                    row += new string(' ', maxDigits + 1);
            }

            return row;
        }

        string SuggestRow(int r)
        {
            string row = " ";

            for (int i = 0; i < board.Width; i++)
            {
                if (r == maxY && i == maxX)
                    row += "x ";
                else if (board.Tiles[r][i] == -2)
                    row += ". ";
                else
                    row += "  ";
            }

            return row;
        }


        int maxDigits = $"{max}".Length;
        Console.WriteLine($"    {Header(1, 1)}{Header(2, 0)}{Header(maxDigits, 1)}{Header(1, 1)}");
        Console.WriteLine($"    {Border(1, 1)}{Border(2, 0)}{Border(maxDigits, 1)}{Border(1, 1)}+");

        for (int i = 0; i < board.Height; i++)
            Console.WriteLine($" {i + 1, 2} |{BoardRow(i)}|{HeatRow(i)}|{CountRow(i, maxDigits)}|{SuggestRow(i)}|");

        Console.WriteLine($"    {Border(1, 1)}{Border(2, 0)}{Border(maxDigits, 1)}{Border(1, 1)}+");
        Console.WriteLine();

        Console.WriteLine("  Best position: (" + (maxX + 1) + ", " + (maxY + 1) + ")");
        Console.WriteLine("  Chance: " + max + " / " + Total +
                          " (" + ((int)(max / (double)Total * 1000) / 10.0) + "%)");
    }



    // prompts for what was found at the recommended tile and updates the board.
    // keeps re-prompting until it gets a value it understands, so bad input can't crash the app
    public void PromptUpdate(Board board)
    {
        while (true)
        {
            Console.Write("Please enter the discovered tile (nothing = x): ");
            string input = (Console.ReadLine() ?? "").Trim();

            if (input.Length == 0) continue; // ignore blank input, ask again

            string lower = input.ToLowerInvariant();

            if (lower == "done" || lower == "reset") // exits the current board
            {
                End = lower;
                return;
            }

            if (lower == "x" || lower == "nothing") // nothing was found at that location
            {
                board.UpdateTile(maxX, maxY, -1);
                return;
            }

            // typing the animal's number directly (its position in the animals list)
            if (input.Length == 1 && char.IsDigit(input[0]))
            {
                int index = input[0] - '0';

                if (board.UpdateTile(maxX, maxY, index))
                    return;

                Console.WriteLine("That number doesn't match any of the animals, try again");
                continue;
            }

            // or type the animal's name directly
            if (AnimalCatalog.TryParse(input, out AnimalType type))
            {
                int animalIndex = Animals.FindIndex(a => a.Type == type);

                if (animalIndex != -1)
                {
                    board.UpdateTile(maxX, maxY, Animals[animalIndex].Num);
                    return;
                }

                Console.WriteLine("That animal isn't part of this stage, try again");
                continue;
            }

            Console.WriteLine("Invalid input, enter x, an animal name/number, or done/reset");
        }
    }



    // sets up the tile counts for each animal
    public void InitCounts()
    {
        Counts = [];

        for (int i = 0; i < 5; i++)
        {
            List<int> row = [];

            for (int j = 0; j < 5; j++)
                row.Add(0);

            Counts.Add(row);
        }
    }
}
