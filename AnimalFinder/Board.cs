using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnimalFinder
{
    /*
     * This is just the board that contains the animals and colision detection of animal patterns
     * 
     * Also includes code to print the board
     */

    internal class Board
    {

        public List<List<int>> board;
        public List<int> counts;
        public int count;
        public int width;
        public int height;

        // constructor

        public Board(int Count)
        {
            this.width = 5;
            this.height = 5;
            this.count = Count;

            makeBoard();
            makeCounts();
        }

        // public methods

        public void updateTile(int x, int y, int tile)
        {
            this.board[y][x] = tile;

            if (tile != -1)
                this.counts[tile]++;
        }

        // adds an animal to the board after it has been found to be a valid position
        public void addAnimal(int x, int y, Animal animal)
        {
            for (int i = 0; i < animal.height; i++)
                for (int j = 0; j < animal.width; j++)
                    if (animal.pattern[i][j] == 1)
                        this.board[y + i][x + j] = animal.num;
        }

        // collision detection to find if the animal fits at the desired x/y
        // just loops through every tile in the animal pattern and the corrosponding tile in the board and checks if
        // there is a collision or not
        public bool validSpot(int x, int y, Animal animal)
        {
            if (x + animal.width - 1 >= this.width) return false; // checks to see if animal is placed out of the board
            if (y + animal.height - 1 >= this.height) return false;

            int count = 0;
            for (int i = 0; i < animal.height; i++) // loop through every tile of the animal pattern
            {
                for (int j = 0; j < animal.width; j++)
                {
                    if (animal.pattern[i][j] == 0) continue; // if animal pattern at this x/y is empty it doesnt matter

                    if (this.board[y + i][x + j] == -2) continue; // -2 means that tile is unknown and could be anything, so the animal can be there
                    if (this.board[y + i][x + j] == -1) return false; // -1 means that tile is known to be empty, so the animal can't be there

                    // if it has been explored and is known to be an animal, check if the tile contains
                    // a part of this animal, and if it doesn't the animal can't be there
                    if (this.board[y + i][x + j] != animal.num) return false;
                    else { count++; continue; }
                }
            }

            // if the number of known tile locations of that animal doesn't match the number
            // of known locations that this animal exists on the board, then the animal can't exist here
            //
            // basically, if you know a tile of the animal and the current position you're attempting doesn't contain that known position
            // this attemted position is incorrect
            if (count != this.counts[animal.num]) return false;

            return true;
        }

        // copy the current board and return the copy
        public Board copy()
        {
            Board newBoard = new Board(this.count);
            
            for (int y = 0; y < this.height; y++)
                for (int x = 0; x < this.width; x++)
                    newBoard.board[y][x] = this.board[y][x];

            for (int i = 0; i < this.count; i++)
                newBoard.counts[i] = this.counts[i];

            return newBoard;

        }

        // print the board
        public void print()
        {
            Console.WriteLine("      1 2 3 4 5");
            Console.WriteLine("    +-----------+");

            for (int y = 0; y < this.height; y++)
            {
                Console.Write("  " + y + " | ");

                for (int x = 0; x < this.width; x++)
                {
                    switch (this.board[y][x])
                    {
                        case -2: Console.Write("  "); break; // -2 means unkown
                        case -1: Console.Write("x "); break; // -1 means known to be nothing
                        default: Console.Write(this.board[y][x] + " "); break; // anything else is the number of the animal you inputted
                    }
                }

                Console.WriteLine("|");
            }
            Console.WriteLine("    +-----------+");
        }

        // private methods

        // this just initializes the board
        private void makeBoard()
        {
            this.board = new List<List<int>>();

            for (int i = 0; i < this.height; i++)
            {
                this.board.Add(new List<int>());

                for (int j = 0; j < this.width; j++)
                    this.board[i].Add(-2);
            }
        }

        // this initializes the list of counts
        private void makeCounts()
        {
            this.counts = new List<int>();

            for (int i = 0; i < this.count; i++)
                this.counts.Add(0);
        }

    }
}
