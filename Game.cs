using System;

namespace WorldOfZuul
{
    public class Game
    {
        private Room? currentRoom;
        private Room? previousRoom;
        private Inventory_functionality inventory;

        public Game()
        {
            CreateRooms();
            inventory = new Inventory_functionality();

            inventory.AddItem(new Item("house key", "just a key to Ur house"));
        }

        private void CreateRooms()
        {
            Room? outside = new("Outside", "You are standing outside the main entrance of the university.");
            Room? theatre = new("Theatre", "You find yourself inside a large lecture theatre. You can See a KEVIN LE MINION screaming for help!", "Emerald");
            Room? pub = new("Pub", "You've entered the campus pub.");
            Room? lab = new("Lab", "You're in a computing lab." , "", "Emerald" );
            Room? office = new("Office", "You've entered an administration office.");

            outside.SetExits(null, theatre, lab, pub);
            theatre.SetExit("west", outside);
            pub.SetExit("east", outside);
            lab.SetExits(outside, office, null, null);
            office.SetExit("west", lab);

            currentRoom = outside;
        }

        public void Play()
        {
            Parser parser = new();
            PrintWelcome();
            bool continuePlaying = true;

            while (continuePlaying)
            {
                Console.WriteLine(currentRoom?.ShortDescription);
                Console.Write("> ");

                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                Command? command = parser.GetCommand(input);

                if (command == null)
                {
                    Console.WriteLine("I don't know that command.");
                    continue;
                }

                switch (command.Name)
                {
                    case "look":
                        Console.WriteLine(currentRoom?.LongDescription);
                        break;

                    case "back":
                        if (previousRoom == null)
                            Console.WriteLine("You can't go back from here!");
                        else
                            currentRoom = previousRoom;
                        break;
                    
                    case "talk":
                        switch (currentRoom.ShortDescription)
                        {
                            case ("Outsie"):
                                Console. WriteLine("You talk to urself madman?!");
                                break;
                                 case("Theatre"):
                                Console.WriteLine(
                                    "Kevin wants a banana but in exchange for it he has to give the villagers some clear water. How do we get it?\n" +
                                    "1. From the tap that u have in ur house\n" +
                                    "2. Get water from the river and filter it\n" +
                                    "3. Give them a salty one, no one will realize anyway");
                                string chooise = Console.ReadLine();
                                switch (chooise)
                                {
                                    case("1"):
                                        Console.WriteLine("You helped! Kevin got Banna and gives u an emerald!");
                                        Item emerald = new Item("Emerald", "You are standing emerald.");
                                        inventory.AddItem(emerald);
                                        currentRoom = previousRoom;

                                        break;
                                    case("2"):
                                        Console.WriteLine("Vills are half happy cuz they could've done it themselves!U recive some red powder");
                                        Item powder = new Item("Red Powder", "You are standing red powder.");
                                        inventory.AddItem(powder);
                                        currentRoom = previousRoom;

                                        break;
                                    case("3"):
                                        Console.WriteLine("No way u tought that's gonna work out! Get the hell out of here before the Golem kills U");
                                        currentRoom = previousRoom;
                                        break;
                        }

                    break;
                                 case("lab"):
                                     
                                     
                                     break;
                         }

                        break;

                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        Move(command.Name);
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    case "show":
                        inventory.ShowItems();
                        break;

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing World of Zuul!");
        }

        private void Move(string direction)
        {
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                if (currentRoom.Exits[direction].Check(inventory))
                {
                    previousRoom = currentRoom;
                    currentRoom = currentRoom?.Exits[direction];
                }
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }

        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to the World of Zuul!");
            Console.WriteLine("World of Zuul is a new, incredibly boring adventure game.");
            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("You are lost. You are alone. You wander around the university.");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
            Console.WriteLine("Type 'show' to display your inventory.");
        }

       

            }
        }
    
