namespace WorldOfZuul
{
    public class Room
    {
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set;}
        public string Directions { get; private set; }
        public Dictionary<string, Room> Exits { get; private set; } = new();
        public string reqired_item { get; private set; }
        public string dispise_item { get; private set; }
         public bool IsLocked { get; private set; } // New property to indicate if the room is locked
        public Room( string shortDesc, string Directions , string longDesc, string dispise_item = null, 
            string reqired_item = null)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
            this.Directions = Directions;
            this.reqired_item = reqired_item;
            this.dispise_item = dispise_item;
        }

        public void SetDirections(string directions)
        {
            this.Directions = directions;
        }
        public void SetDescription(string longDesc)
        {
           LongDescription = longDesc;
        }
        public void SetExits(Room? north, Room? east, Room? south, Room? west)
        {
            SetExit("north", north);
            SetExit("east", east);
            SetExit("south", south);
            SetExit("west", west);
        }

        public void SetExit(string direction, Room? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
        }

         public void LockRoom()
        {
            IsLocked = true; // Lock the room
        }

        public void UnlockRoom()
        {
            IsLocked = false; // Unlock the room
        }

        public bool Check(Inventory_functionality inventory)
        {
            if (IsLocked)
            {
                Console.WriteLine($"The {ShortDescription} is locked. You cannot enter.");
                return false;
            }

            if (string.IsNullOrEmpty(reqired_item) && string.IsNullOrEmpty(dispise_item))
            {
                return true;
            }

            else if (!string.IsNullOrEmpty(dispise_item))
            {
                foreach (Item item in inventory.items_holder)
                {
                    if (item.Name == dispise_item)
                    {
                        return true;
                    }
                }
                return true;
            }

            switch (reqired_item) 
            {
                case "Office key":
                    Console.WriteLine("\nDoor is locked\n"
                                      + "1. Unlock\n"
                                      + "2. Go Back");
                    break;
                
                case "Emerald":
                    Console.WriteLine("\nA Villager stoped You! He wants an Emerald if you want him to let you pass\n"
                                      + "1. Give him emerald\n"
                                      + "2. Go Back\n");
                    break;
            }
            

            while (true)
            {
                string wellChoice = Console.ReadLine();
                if (wellChoice == "1")
                {
                    foreach (Item item in inventory.items)
                    {
                        if (item.Name == reqired_item)
                        {
                            Console.WriteLine("\nYou entered");
                            inventory.GiveItem(item);
                            reqired_item = null;
                            return true;
                        }
                    }

                    Console.WriteLine("You can't enter because you do not have the required item.");
                    return false;


                }
                else if (wellChoice == "2")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Choose between 1 and 2");
                }
            }
        }

    }
    
}
