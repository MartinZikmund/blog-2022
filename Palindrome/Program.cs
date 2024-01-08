using System;
using System.Collections.Generic;
using System.Linq;

namespace ChristmasTree
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the width of the tree from the first command line argument
            int width = int.Parse(args[0]);

            // Calculate the number of rows needed for the top of the tree
            int numRows = (width + 1) / 2;

            // Create a random number generator
            Random rand = new Random();

            // Draw the top of the tree
            for (int i = 0; i < numRows; i++)
            {
                // Calculate the number of asterisks for this row
                int numAsterisks = (i * 2) + 1;

                // Calculate the number of spaces on either side of the asterisks
                int numSpaces = (width - numAsterisks) / 2;

                // Choose a random number of ornaments for this row
                int numOrnaments = rand.Next(numAsterisks + 1);

                // Create a list of ornaments for this row
                List<char> ornaments = Enumerable.Repeat('o', numOrnaments).Concat(Enumerable.Repeat('*', numAsterisks - numOrnaments)).ToList();

                // Shuffle the ornaments
                ornaments = ornaments.OrderBy(x => rand.Next()).ToList();

                // Draw the row
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(new string(' ', numSpaces));
                for (int j = 0; j < ornaments.Count; j++)
                {
                    if (ornaments[j] == 'o')
                    {
                        // Choose a random color for the ornament
                        ConsoleColor color = (ConsoleColor)rand.Next(1, 16);
                        Console.ForegroundColor = color;
                    }
                    Console.Write(ornaments[j]);
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine(new string(' ', numSpaces));
            }

            // Draw the trunk of the tree
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(new string(' ', width / 2) + "|" + new string(' ', width / 2));
            Console.WriteLine(new string(' ', width / 2) + "|" + new string(' ', width / 2));

            // Reset the console color
            Console.ResetColor();

            // Print a message
            Console.WriteLine("Merry Christmas!");
        }
    }
}
