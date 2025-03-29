using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    static List<Ship> ships = new List<Ship>();
    static List<Container> containers = new List<Container>();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            DisplayMainMenu();
            string choice = Console.ReadLine();
            HandleUserChoice(choice);
        }
    }

    static void DisplayMainMenu()
    {
        Console.WriteLine("List of Ships:");
        if (ships.Count == 0) Console.WriteLine("None");
        else foreach (var ship in ships) Console.WriteLine($"- {ship.MaxSpeed} knots, {ship.MaxContainerNumber} containers, {ship.MaxWeight} tons");

        Console.WriteLine("\nList of Containers:");
        if (containers.Count == 0) Console.WriteLine("None");
        else foreach (var container in containers) Console.WriteLine($"- {container.SerialNum} ({container.GetType().Name})");

        Console.WriteLine("\nAvailable actions:");
        Console.WriteLine("1. Add Ship");
        Console.WriteLine("2. Remove Ship");
        Console.WriteLine("3. Add Container");
        Console.WriteLine("4. Place Container on Ship (LOAD)");
        Console.WriteLine("5. Remove Container from Ship (UNLOAD)");
        Console.WriteLine("6. Print Ship Info");
        Console.WriteLine("7. Print Container Info");
        Console.WriteLine("8. Transfer Container between Ships");
        Console.WriteLine("9. Exit");
        Console.Write("Choose an option: ");
    }

    static void HandleUserChoice(string choice)
    {
        switch (choice)
        {
            case "1": AddShip(); break;
            case "2": RemoveShip(); break;
            case "3": AddContainer(); break;
            case "4": PlaceContainerOnShip(); break;
            case "5": RemoveContainerFromShip(); break;
            case "6": PrintShipInfo(); break;
            case "7": PrintContainerInfo(); break;
            case "8": TransferContainerBetweenShips(); break;
            case "9": Environment.Exit(0); break;
            default: Console.WriteLine("Invalid choice, press Enter to continue..."); Console.ReadLine(); break;
        }
    }

    static void AddShip()
    {
        Console.Write("Enter ship max speed (knots): ");
        double speed = double.Parse(Console.ReadLine());
        Console.Write("Enter max number of containers: ");
        int maxContainers = int.Parse(Console.ReadLine());
        Console.Write("Enter max weight (tons): ");
        double maxWeight = double.Parse(Console.ReadLine());

        ships.Add(new Ship(new List<Container>(), speed, maxContainers, maxWeight));
        Console.WriteLine("Ship added! Press Enter...");
        Console.ReadLine();
    }

    static void RemoveShip()
    {
        if (ships.Count == 0)
        {
            Console.WriteLine("No ships to remove. Press Enter...");
            Console.ReadLine();
            return;
        }
        Console.Write("Enter the index of the ship to remove: ");
        int index = int.Parse(Console.ReadLine());
        if (index < 0 || index >= ships.Count)
        {
            Console.WriteLine("Invalid index. Press Enter...");
            Console.ReadLine();
            return;
        }
        ships.RemoveAt(index);
        Console.WriteLine("Ship removed! Press Enter...");
        Console.ReadLine();
    }

    static void AddContainer()
    {
        Console.Write("Enter container type (L - Liquid, G - Gas, C - Refrigerated): ");
        string type = Console.ReadLine().ToUpper();

        Console.Write("Enter load weight (kg): ");
        double loadWeight = double.Parse(Console.ReadLine());
        Console.Write("Enter height (m): ");
        double height = double.Parse(Console.ReadLine());
        Console.Write("Enter container weight (kg): ");
        double weight = double.Parse(Console.ReadLine());
        Console.Write("Enter depth (m): ");
        double depth = double.Parse(Console.ReadLine());
        Console.Write("Enter max load (kg): ");
        double maxLoad = double.Parse(Console.ReadLine());

        Container container = null;

        if (type == "L") container = new LiquidContainer(loadWeight, height, weight, depth, maxLoad, false);
        else if (type == "G") container = new GasContainer(loadWeight, height, weight, depth, maxLoad, 1.0);
        else if (type == "C")
        {
            Console.Write("Enter stored product type: ");
            string productType = Console.ReadLine();
            Console.Write("Enter storage temperature (°C): ");
            double temperature = double.Parse(Console.ReadLine());
            container = new RefrigeratedContainer(loadWeight, height, weight, depth, maxLoad, productType, temperature);
        }

        if (container != null)
        {
            containers.Add(container);
            Console.WriteLine("Container added! Press Enter...");
        }
        else
        {
            Console.WriteLine("Invalid container type. Press Enter...");
        }
        Console.ReadLine();
    }


    static void PlaceContainerOnShip()
    {
        Console.Write("Enter the index of the ship: ");
        int shipIndex = int.Parse(Console.ReadLine());
        Console.Write("Enter the serial number of the container: ");
        string serialNum = Console.ReadLine();

        Container container = containers.FirstOrDefault(c => c.SerialNum == serialNum);
        if (container == null || shipIndex < 0 || shipIndex >= ships.Count)
        {
            Console.WriteLine("Invalid ship index or container not found. Press Enter...");
            Console.ReadLine();
            return;
        }

        ships[shipIndex].AddContainer(container);
        containers.Remove(container);
        Console.WriteLine("Container placed on ship! Press Enter...");
        Console.ReadLine();
    }

    static void RemoveContainerFromShip()
    {
        Console.Write("Enter the index of the ship: ");
        int shipIndex = int.Parse(Console.ReadLine());
        Console.Write("Enter the serial number of the container to remove: ");
        string serialNum = Console.ReadLine();

        if (shipIndex < 0 || shipIndex >= ships.Count)
        {
            Console.WriteLine("Invalid ship index. Press Enter...");
            Console.ReadLine();
            return;
        }

        ships[shipIndex].RemoveContainer(serialNum);
        Console.WriteLine("Container removed from ship! Press Enter...");
        Console.ReadLine();
    }

    static void PrintShipInfo()
    {
        Console.Write("Enter the index of the ship: ");
        int index = int.Parse(Console.ReadLine());
        if (index < 0 || index >= ships.Count)
        {
            Console.WriteLine("Invalid index. Press Enter...");
            Console.ReadLine();
            return;
        }
        ships[index].PrintShipInfo();
        Console.WriteLine("Press Enter...");
        Console.ReadLine();
    }

    static void PrintContainerInfo()
    {
        Console.Write("Enter container serial number: ");
        string serialNum = Console.ReadLine();
        Container container = containers.FirstOrDefault(c => c.SerialNum == serialNum);
        if (container == null)
        {
            Console.WriteLine("Container not found. Press Enter...");
            Console.ReadLine();
            return;
        }
        container.PrintInfo();
        Console.WriteLine("Press Enter...");
        Console.ReadLine();
    }
    static void TransferContainerBetweenShips()
    {
        Console.Write("Enter the index of the source ship: ");
        int sourceIndex = int.Parse(Console.ReadLine());
        Console.Write("Enter the index of the target ship: ");
        int targetIndex = int.Parse(Console.ReadLine());
        Console.Write("Enter the serial number of the container to transfer: ");
        string serialNum = Console.ReadLine();

        if (sourceIndex < 0 || sourceIndex >= ships.Count || targetIndex < 0 || targetIndex >= ships.Count)
        {
            Console.WriteLine("Invalid ship index. Press Enter...");
            Console.ReadLine();
            return;
        }

        Ship sourceShip = ships[sourceIndex];
        Ship targetShip = ships[targetIndex];

        try
        {
            sourceShip.TransferContainer(targetShip, serialNum);
            Console.WriteLine("Container transferred successfully! Press Enter...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}. Press Enter...");
        }

        Console.ReadLine();
    }

}
