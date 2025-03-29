using System;

public class GasContainer : Container, IHazardNotifier
{
	public double Pressure { get; set; }

	public GasContainer(double loadWeight, double height, double weight, double depth, double maxLoad, double pressure):
		base(loadWeight, height, weight, depth, maxLoad)
	{
		this.Pressure = pressure;
	}

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"HAZARD! Serial Number  {message} ");
    }

	public override void Load(double weight) 
	{
		if(LoadWeight + weight > MaxLoad)
		{
			NotifyHazard("Attempt to overload gas container");
            throw new OverfillException("Load exceeds gas container capacity.");
        }
		LoadWeight += weight;
	}
	public override void Unload()
	{
		LoadWeight *= 0.05; // leave 5%
	}
    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"Pressure: {Pressure}");
    }

}
