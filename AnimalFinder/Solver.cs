using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AnimalFinder
{
    /*
    * This program is to automate a mobile game I played called 'disco zoo'
    * 
    * in the game, you need to find certain patterns or 'animals' in a 5x5 grid
    * there can be anywhere from 1 to 3 animals, and they all have varying shapes
    * 
    * this just lets you input what animals are in the current stage, and then tries
    * every possible arrangment of those two animals and counts the number of times
    * and animal appears on a certain one of the 5x5 tiles, and then prints the results
    * as the probabilty that an animal is there
    * 
    * It also provides other things like the total count, the count on each tile, and a heatmap
    * 
    */

    internal class Solver
    {
        public List<Animal> animals;
        public List<List<int>> counts;

        public int total;
        public int max;
        public int maxX;
        public int maxY;
        public String end;

        // main

        // asks for input, then parses input and finds probability of an animal existing on each tile
        static void Main(string[] args)
        {
            while (true)
            {
                Solver solver = new Solver();

                Board board = new Board(solver.animals.Count);

                while (true)
                {
                    solver.total = 0;
                    solver.initCounts();

                    solver.solve(board, 0);

                    Console.WriteLine();
                    board.print();

                    Console.WriteLine();
                    solver.printCounts(board);

                    Console.WriteLine();
                    solver.promptUpdate(board);

                    if (solver.end == "done") return;
                    if (solver.end == "reset") break;
                }
            }
        }

        // constuctor

        public Solver()
        {
            bool correct;

            while (true)
            {
                // asks for names seperated by a comma. these must match the switch statement in animals
                Console.WriteLine("please enter the animals (name, name, name)"); 
                String input = Console.ReadLine();

                String[] names = input.Split(',');

                for (int i = 1; i < names.Length; i++)
                    names[i] = names[i].Trim();

                correct = true;
                this.animals = new List<Animal>();
                for (int i = 0; i < names.Length; i++) // make new animal objects based off the name and put them into an array
                {
                    this.animals.Add(new Animal(names[i], i));
                    if (this.animals[i].layout == "") { correct = false; break; }
                }

                if (!correct) {
                    Console.WriteLine("there was a problem with one of the inputted names");
                    continue;
                }
                break;
            }

            this.end = "";
        }

        // public methods

        // recursivly attempt every combination of the animals
        public void solve(Board board, int animal)
        {
            // end of the recursion
            if (animal == this.animals.Count)
            {
                for (int y = 0; y < board.height; y++)
                    for (int x = 0; x < board.width; x++)
                        if (board.board[y][x] >= 0)
                            this.counts[y][x]++; // increment the number of times an animal appears at each tile

                total++; // incrememnt total number of solutions
                return;
            }

            // loop through every loocation of for this animal
            for (int y = 0; y < board.height; y++)
            {
                for (int x = 0; x < board.width; x++)
                {
                    if (board.validSpot(x, y, this.animals[animal])) // check validity of the spot
                    {
                        Board newBoard = board.copy(); // make a new copy of the board with this animal on it
                        newBoard.addAnimal(x, y, this.animals[animal]);
                        solve(newBoard, animal + 1); // recursively add the next animal
                    }
                }
            }

            return;
        }

        // prints the counts of how many times an animal appears on a tile, the total number of solutions found
        // and other information
        public void printCounts(Board board)
        {
            if (this.total <= 0)
            {
                Console.WriteLine("no solution found");
                return;
            }

            this.max = 0;
            this.maxX = 0;
            this.maxY = 0;

            // this prints the number of times an animal appears on each tile
            for (int y = 0; y < 5; y++)
            {
                Console.Write("  ");

                for (int x = 0; x < 5; x++)
                {
                    if (board.board[y][x] != -2) { Console.Write("    "); continue; }

                    for (int i = 0; i < (this.counts[y][x] == 0 ? 3 : 3 - Math.Floor(Math.Log10(this.counts[y][x]))); i++)
                        Console.Write(" ");

                    Console.Write(this.counts[y][x]);

                    if (this.counts[y][x] > max)
                    {
                        this.max = this.counts[y][x];
                        this.maxX = x;
                        this.maxY = y;
                    }
                }

                Console.WriteLine();    
            }
            Console.WriteLine();

            String shading = " ░▒▓█";

            // this prints a heatmap of the problem
            for (int y = 0; y < 5; y++)
            {
                Console.Write("     ");

                for (int x = 0; x < 5; x++)
                {
                    Console.Write(shading[(int)Math.Ceiling(this.counts[y][x] / (double)this.total * (shading.Length - 1))]);
                    Console.Write(shading[(int)Math.Ceiling(this.counts[y][x] / (double)this.total * (shading.Length - 1))]);
                }

                Console.WriteLine();
            }
            Console.WriteLine();

            // this prints the current state of the board
            for (int y = 0; y < 5; y++)
            {
                Console.Write("     ");

                for (int x = 0; x < 5; x++)
                {
                    if (x == this.maxX && y == maxY) { Console.Write("x "); continue; }
                    if (board.board[y][x] == -2) { Console.Write("_ "); continue; }

                    Console.Write("  ");
                }

                Console.WriteLine();
            }
            Console.WriteLine();

            // this prints the best position and other information about the board
            Console.WriteLine("  best position: " + (this.maxX + 1) + ", " + (this.maxY + 1));
            Console.WriteLine("  animal occurences: " + this.max + " / " + this.total +
                              ", " + ((int)(this.max / (double)this.total * 1000) / 10.0) + "%");
        }

        // this prompts the user to input information gathered about the position recommended to play at
        public void promptUpdate(Board board)
        {
            Console.Write("please enter the discovered tile (nothing = x): ");
            String input = Console.ReadLine();

            if (input.Length == 1) // x means that nothing was at that location
            {
                if (input[0] == 'x') board.updateTile(this.maxX, this.maxY, -1);
                else board.updateTile(this.maxX, this.maxY, (int)input[0] - 48); // accepts input in the form of the index of an animal
                return;
            }
            else
            {
                if (input == "done" || input == "reset") // done or reset exits the current board
                {
                    this.end = input;
                    return;
                }

                if (input == "nothing") // another way of inputting nothing
                {
                    board.updateTile(this.maxX, this.maxY, -1);
                    return;
                }

                for (int i = 0; i < this.animals.Count; i++) // you can also type the name of the animal if it was found at that location
                {
                    if (input == this.animals[i].name) {
                        board.updateTile(this.maxX, this.maxY, this.animals[i].num);
                        break;
                    }
                }
                return;
            }
        
        }

        // initializes the array of counts for each tile
        public void initCounts()
        {
            this.counts = new List<List<int>>();
            for (int i = 0; i < 5; i++)
            {
                this.counts.Add(new List<int>());
                for (int j = 0; j < 5; j++)
                {
                    this.counts[i].Add(0);
                }
            }
        }
    }
}
