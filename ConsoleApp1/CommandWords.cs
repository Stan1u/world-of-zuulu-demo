using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class CommandWords
    {
        public List<string> ValidCommands { get; } = new List<string> { "north", "east", "south", "west", "explore", "back", "map", "quit", "help", "show", "talk", "1", "2", "3", "4" };

        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}