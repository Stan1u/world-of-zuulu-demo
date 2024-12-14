using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WorldOfZuul;
public class Item
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Item(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public override string ToString()
    {
        return $"{Name}: {Description}";
    }
}

public class Inventory_functionality
{
    public List<Item> items;
    public List<Item> items_holder;

    public Inventory_functionality()
    {
        items = new List<Item>();
        items_holder = new List<Item>();
    }
    

    public void AddItem(Item item)
    {
        items_holder.Add(item);
        items.Add(item);
        Console.WriteLine($"Added item: {item.Name}");
    }

    public void GiveItem(Item item)
    {
        items.Remove(item);
            Console.WriteLine($"Item {item.Name} has ben given ");
    }
    public void ShowItems()
    {
        Console.WriteLine("Inventory Items:");
        if (items.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
        }
        else
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}

public class InventoryTest
{
    public static void TestInventory(string[] args)
    {
        // Tworzenie ekwipunku
        Inventory_functionality inventory = new Inventory_functionality();

        // Przedmioty zdobyte po zakończeniu misji
        Item key1 = new Item("House key", "just a key to Ur house");

        // Dodawanie przedmiotów do ekwipunku
        inventory.AddItem(key1);

        // Wyświetlanie przedmiotów w ekwipunku
        Console.WriteLine("\nInventory after mission:");
        inventory.ShowItems();
    }
    
}