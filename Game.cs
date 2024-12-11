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
            Room? village = new("Village", "You are standing in the main area of AquaVale village.");
            Room? well = new("Well", "You enter the village well. The air is heavy with the smell of stagnant water, and you notice algae growing around the edges. A concerned villager named Amara approaches you. She is a local water keeper, deeply invested in the well’s condition. Write 'talk' to talk to Amara", "Emerald");
            Room? sanitation_area = new("Sanitation Area", "The sanitation area is an open plot of land that villagers currently use for waste disposal. The ground is littered with refuse, and a foul smell permeates the air. A young carpenter named Malik approaches you. He’s eager to help but unsure how to proceed. Write 'talk' to talk to Malik");
            Room? lab = new("Lab", "You're in a computing lab." , "", "Emerald" ); //room to be edited
            Room? office = new("Office", "You've entered an administration office."); //room to be edited

            village.SetExits(null, well, lab, sanitation_area);
            well.SetExit("west", village);
            sanitation_area.SetExit("east", village);
            lab.SetExits(village, office, null, null);
            office.SetExit("west", lab);

            currentRoom = village;
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
                    case "explore":
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
        case "Village":
            Console.WriteLine("You talk to yourself, madman?!"); // Placeholder dialog
            break;

        case "Well":
            Console.WriteLine(
                "Amara: Leader, our well is in terrible condition. People are falling sick because of the polluted water. We need a solution, but I can’t decide what’s best. What should we do?\n" +
                "1. Install a high-quality water filtration system, ensuring clean water for the village.\n" +
                "2. Clean the well thoroughly and educate villagers on protecting it from contamination.\n" +
                "3. Ignore the problem for now and hope the water improves naturally."
            );
            string wellChoice = Console.ReadLine();
            switch (wellChoice)
            {
                case "1":
                    Console.WriteLine("You chose the most effective option. You receive an emerald from Amara.");
                    inventory.AddItem(new Item("Emerald", "A shiny emerald given by Amara."));
                    break;
                case "2":
                    Console.WriteLine("You chose a moderately good option, some maintenance will be required. You receive red powder from Amara.");
                    inventory.AddItem(new Item("Red Powder", "A mysterious red powder given by Amara."));
                    break;
                case "3":
                    Console.WriteLine("Your option is ineffective and leads to further sickness in the village. Amara is disappointed.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose 1, 2, or 3.");
                    break;
            }
            break;

        case "Sanitation Area":
            Console.WriteLine(
                "Malik: Leader, this area desperately needs proper sanitation facilities. The villagers are falling ill due to poor hygiene. What should we do?\n" +
                "1. Build durable toilets and washing stations using sustainable materials.\n" +
                "2. Build temporary latrines while planning permanent facilities.\n" +
                "3. Do nothing and tell villagers to manage as they are."
            );
            string sanitationChoice = Console.ReadLine();
            switch (sanitationChoice)
            {
                case "1":
                    Console.WriteLine("You chose the most effective option. Malik rewards you with a sapphire.");
                    inventory.AddItem(new Item("Sapphire", "A radiant sapphire given by Malik."));
                    break;
                case "2":
                    Console.WriteLine("You chose a moderately good option. Malik provides you with wooden planks for future use.");
                    inventory.AddItem(new Item("Wooden Planks", "Sturdy planks provided by Malik."));
                    break;
                case "3":
                    Console.WriteLine("Your decision disappoints Malik and leaves the sanitation area in poor condition.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose 1, 2, or 3.");
                    break;
            }
            break;

        default:
            Console.WriteLine("There's no one here to talk to.");
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

            Console.WriteLine("Thank you for playing World of Zuul!"); //needs to be changed
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
            Console.WriteLine("The village of AquaVale has been your home for as long as you can remember.\nNestled between rolling hills and a winding river, it was once a place of abundance and harmony.\nBut over the years, things have taken a turn for the worse.\nThe river, once a lifeline for the community, now carries pollutants from upstream.\nThe well, a vital source of water, has fallen into neglect, and sanitation facilities are practically nonexistent.\nSickness is rampant, and the villagers are losing hope.");
            Console.WriteLine("\nAs the newly chosen leader of AquaVale, you carry the weight of responsibility.\nThe people have placed their trust in you to bring back the village's former glory.\nYour goal is clear: ensure clean water and proper sanitation for everyone.\nBut resources are scarce, and time is limited.\nWith only ten turns to make a difference, every decision counts.");
            Console.WriteLine("\nWill you lead AquaVale to a brighter future, or will the village continue to suffer under the weight of its challenges?");
            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("You are lost. You are alone. You wander around the university.");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'explore' to explore around the area you are in.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
            Console.WriteLine("Type 'show' to display your inventory.");
        }

       }
       }
    
