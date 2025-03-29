using System;

public class LiquidContainer : Container, IHazardNotifier
{
	public bool IsHazardous { get; set; }

	public LiquidContainer(double loadWeight, double height, double weight, double depth, double maxLoad, bool isHazardous) :
		base(loadWeight, height, weight, depth, maxLoad)
	{
		this.IsHazardous = isHazardous;
	}

	public void NotifyHazard(string message)
	{
		Console.WriteLine($"HAZARD! Serial Number  {message} ");
	}

	public override void Load(double weight)
	{
		double capacityLimit = IsHazardous ? 0.5 * MaxLoad : 0.9 * MaxLoad;

		if (LoadWeight + weight > capacityLimit)
		{
			NotifyHazard("Attempt to overload liquid container");
			throw new OverfillException("Load exceeds safe capacity for liquid container.");
		}
		LoadWeight += weight;
	}
    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"Hazardous: {(IsHazardous ? "YES" : "NO")}");
    }


}
