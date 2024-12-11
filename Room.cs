namespace WorldOfZuul
{
    public class Room
    {
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set;}
        public Dictionary<string, Room> Exits { get; private set; } = new();
        public string reqired_item { get; private set; }
        public string dispise_item { get; private set; }
        public Room( string shortDesc, string longDesc, string dispise_item = null, string reqired_item = null)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
            this.reqired_item = reqired_item;
            this.dispise_item = dispise_item;
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

        
        public bool Check(Inventory_functionality inventory)
        {
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
                        Console.WriteLine("You can't enter back");
                        return false;
                    }
                }
                return true;
            }

            Console.WriteLine("A Villager stoped You! He wants an Emerald if you want him to let you pass\n"
                              + "1. Give him emerald\n"
                              + "2. Go Back\n");
            

            while (true)
            {
                string wellChoice = Console.ReadLine();
                if (wellChoice == "1")
                {
                    foreach (Item item in inventory.items)
                    {
                        if (item.Name == reqired_item)
                        {
                            Console.WriteLine("You entered");
                            inventory.GiveItem(item);
                            return true;
                        }
                    }

                    Console.WriteLine("You can't enter because you do not have the required item. Find the right item and come back then.");
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
