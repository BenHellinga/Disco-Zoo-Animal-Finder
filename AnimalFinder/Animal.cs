using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnimalFinder
{
    /*
     * This just contains all the information about an animal, including the size of the pattern, the pattern itself
     * and how many tiles are in the pattern, as well as the name of the animal
     * 
     */


    internal class Animal
    {

        public List<List<int>> pattern; // might be useless idk
        public int num;
        public int width;
        public int height;
        public String name;
        public String layout;

        // constructor

        public Animal(String Name, int Num)
        {
            this.name = Name;

            // this is just a big switch case statement for the patterns of the animals
            // I used to have it in a file but in a more inefficient way, and then moved it too here
            // and if I ever continue with this project i'll move these back to a file
            //
            // the largest a pattern can be so far in the game is a 4x4, so thats why there are 16 numbers

            switch (this.name)
            {
                // all
                case "bux":             this.layout = "1000 0000 0000 0000"; break;

                // farm
                case "sheep":           this.layout = "1111 0000 0000 0000"; break;
                case "pig":             this.layout = "1100 1100 0000 0000"; break;
                case "rabbit":          this.layout = "1000 1000 1000 1000"; break;
                case "horse":           this.layout = "1000 1000 1000 0000"; break;
                case "cow":             this.layout = "1110 0000 0000 0000"; break;
                case "unicorn":         this.layout = "1000 0110 0000 0000"; break;
                case "chicken":         this.layout = "1000 0100 0010 0000"; break;

                // outback
                case "kangaroo":        this.layout = "1000 0100 0010 0001"; break;
                case "platypus":        this.layout = "1100 0110 0000 0000"; break;
                case "crocodile":       this.layout = "1111 0000 0000 0000"; break;
                case "koala":           this.layout = "1100 0100 0000 0000"; break;
                case "cockatoo":        this.layout = "1000 0100 0100 0000"; break;
                case "tiddalik":        this.layout = "0100 1010 0000 0000"; break;
                case "echidna":         this.layout = "0010 1100 0000 0000"; break;

                // savanna
                case "zebra":           this.layout = "0100 1010 0100 0000"; break;
                case "hippo":           this.layout = "1010 0000 1010 0000"; break;
                case "giraffe":         this.layout = "1000 1000 1000 1000"; break;
                case "lion":            this.layout = "1110 0000 0000 0000"; break;
                case "elephant":        this.layout = "1100 1000 0000 0000"; break;
                case "gryphon":         this.layout = "1010 0100 0000 0000"; break;
                case "rhinoceros":      this.layout = "0100 1000 0100 0000"; break;

                // nothern
                case "bear":            this.layout = "1100 0100 0100 0000"; break;
                case "skunk":           this.layout = "0110 1100 0000 0000"; break;
                case "beaver":          this.layout = "0010 1100 0010 0000"; break;
                case "moose":           this.layout = "1010 0100 0000 0000"; break;
                case "fox":             this.layout = "1100 0010 0000 0000"; break;
                case "sasquatch":       this.layout = "1000 1000 0000 0000"; break;
                case "otter":           this.layout = "1000 1100 0000 0000"; break;

                // polar
                case "penguin":         this.layout = "0100 0100 1010 0000"; break;
                case "seal":            this.layout = "1000 0101 0010 0000"; break;
                case "muskox":          this.layout = "1100 1010 0000 0000"; break;
                case "polar bear":      this.layout = "1010 0010 0000 0000"; break;
                case "walrus":          this.layout = "1000 0110 0000 0000"; break;
                case "yeti":            this.layout = "1000 0000 1000 0000"; break;
                case "snowy owl":       this.layout = "1100 0100 0000 0000"; break;

                // jungle
                case "monkey":          this.layout = "1010 0101 0000 0000"; break;
                case "toucan":          this.layout = "0100 1000 0100 0100"; break;
                case "gorilla":         this.layout = "1010 1010 0000 0000"; break;
                case "panda":           this.layout = "0010 1000 0010 0000"; break;
                case "tiger":           this.layout = "1011 0000 0000 0000"; break;
                case "phoenix":         this.layout = "1000 0000 0010 0000"; break;
                case "lemur":           this.layout = "1000 0100 1000 0000"; break;

                // jurassic
                case "diplodocus":      this.layout = "1000 0110 0100 0000"; break;
                case "stegosaurus":     this.layout = "0110 1001 0000 0000"; break;
                case "raptor":          this.layout = "1100 0100 0010 0000"; break;
                case "t rex":           this.layout = "1000 0000 1100 0000"; break;
                case "triceratops":     this.layout = "1000 0010 1000 0000"; break;
                case "dragon":          this.layout = "1000 0010 0000 0000"; break;
                case "ankylosaurus":    this.layout = "0010 1010 0000 0000"; break;

                // ice age
                case "wooly rhino":     this.layout = "0010 1001 0100 0000"; break;
                case "giant sloth":     this.layout = "1000 0010 1010 0000"; break;
                case "dire wolf":       this.layout = "0100 1001 0100 0000"; break;
                case "saber tooth":     this.layout = "1000 0010 0100 0000"; break;
                case "mammoth":         this.layout = "0100 1000 0010 0000"; break;
                case "akhlut":          this.layout = "0010 1000 0010 0000"; break;
                case "yukon camel":     this.layout = "0010 1000 0001 0000"; break;

                // city
                case "raccoon":         this.layout = "1010 1001 0000 0000"; break;
                case "pigeon":          this.layout = "1000 0100 0110 0000"; break;
                case "rat":             this.layout = "1100 0101 0000 0000"; break;
                case "squirrel":        this.layout = "0010 1000 0100 0000"; break;
                case "opossum":         this.layout = "1000 1010 0000 0000"; break;
                case "sewer turtle":    this.layout = "1100 0000 0000 0000"; break;
                case "chipmunk":        this.layout = "0100 1001 0000 0000"; break;

                // mountain
                case "goat":            this.layout = "1000 1110 0000 0000"; break;
                case "cougar":          this.layout = "1000 0100 1010 0000"; break;
                case "elk":             this.layout = "1010 0110 0000 0000"; break;
                case "eagle":           this.layout = "1000 1000 0100 0000"; break;
                case "coyote":          this.layout = "1100 0010 0000 0000"; break;
                case "aatxe":           this.layout = "0010 1000 0000 0000"; break;
                case "pika":            this.layout = "1010 0010 0000 0000"; break;

                // moon
                case "moonkey":         this.layout = "1000 1010 0010 0000"; break;
                case "lunar tick":      this.layout = "0100 0000 0100 1010"; break;
                case "tribble":         this.layout = "0100 1110 0000 0000"; break;
                case "moonicorn":       this.layout = "1000 1100 0000 0000"; break;
                case "luna moth":       this.layout = "1010 0000 0100 0000"; break;
                case "jade rabbit":     this.layout = "1000 0000 0100 0000"; break;
                case "babmoon":         this.layout = "0100 0010 1000 0000"; break;

                // mars
                case "marsmot":         this.layout = "0100 0100 1100 0000"; break;
                case "marsmoset":       this.layout = "1010 0010 0100 0000"; break;
                case "rock":            this.layout = "1100 1100 0000 0000"; break;
                case "rover":           this.layout = "0100 1010 0000 0000"; break;
                case "martian":         this.layout = "1010 0100 0000 0000"; break;
                case "marsmallow":      this.layout = "1000 0000 1000 0000"; break;
                case "marsten":         this.layout = "1010 0001 0000 0000"; break;
                
                default: this.layout = ""; break;
            }

            init();
            this.num = Num;
        }

        // public methods

        // this method reads the layout of the animal and fills in the information of the width, height, and tiles of the pattern
        public void init()
        {
            if (this.layout == "")
                return;

            this.width = 1;
            this.height = 1;

            for (int y = 0; y < 4; y++) // double loop to find the max width/height of the tiles to find width/height of the pattern
            {
                for (int x = 0; x < 4; x++)
                {
                    if (this.layout[y * 5 + x] - 48 == 1)
                    {
                        this.width = Math.Max(this.width, x + 1);
                        this.height = Math.Max(this.height, y + 1);
                    }
                }
            }

            this.pattern = new List<List<int>>(); // fills out the pattern double list

            for (int y = 0; y < this.height; y++)
            {
                this.pattern.Add(new List<int>());

                for (int x = 0; x < this.width; x++)
                {
                    this.pattern[y].Add(this.layout[y * 5 + x] - 48);
                }
            }
        }
    }
}
