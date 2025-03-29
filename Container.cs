using System;

public class Container
{

private static int _counterForSerialNum = 1; // Counter for unique serial numbers
public double LoadWeight { get; set; } // kg
public double Height { get; set; }     // cm
public double Weight { get; set; }     // kg
public double Depth { get; set; }      // cm
public double MaxLoad { get; set; }    // kg
public string SerialNum { get; private set; } // generated later

    public Container(double loadWeight, double height, double weight, double depth, double maxLoad)
    {
    LoadWeight = loadWeight;
    Height = height;
    Weight = weight;
    Depth = depth;
    MaxLoad = maxLoad;
    SerialNum = GenerateSerialNumber();
    
    }

    public virtual void Load(double weight) 
    {
        if (this.LoadWeight + weight > this.MaxLoad) 
        {
            throw new OverfillException("Load exceeds container capacity.");
        }
        this.LoadWeight += weight;
    }
    public virtual void Unload()
    {
        LoadWeight = 0;
    }

    // Method for generating custom/unique container serial number
    private string GenerateSerialNumber()
    {
        string containerType = this switch
        {
            LiquidContainer => "L",
            GasContainer => "G",
            RefrigeratedContainer => "C",
            _ => "Default"
        };

        return $"KON-{containerType}-{_counterForSerialNum++}";
    }
    public virtual void PrintInfo()
    {
        Console.WriteLine($"Serial Number: {SerialNum}");
        Console.WriteLine($"Type: {GetType().Name}");
        Console.WriteLine($"Load Weight: {LoadWeight} kg");
        Console.WriteLine($"Container Weight: {Weight} kg");
        Console.WriteLine($"Max Load: {MaxLoad} kg");
        Console.WriteLine($"Dimensions: {Height}cm x {Depth}cm");
    }


}
