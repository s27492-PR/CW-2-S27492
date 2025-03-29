using System;

public class Ship
{
    public List<Container> Containers;
    public double MaxSpeed;
    public int MaxContainerNumber;
    public double MaxWeight;


    public Ship(List<Container> containers, double maxSpeed, int maxContainerNumber, double maxWeight)
    {
        Containers = containers;
        MaxSpeed = maxSpeed;
        MaxContainerNumber = maxContainerNumber;
        MaxWeight = maxWeight;
    }

    public void AddContainer(Container container)
    {
        double totalWeight = this.Containers.Sum(x => x.LoadWeight + x.Weight);

        if (Containers.Count >= this.MaxContainerNumber)
        {
            throw new InvalidOperationException("Ship is full - can't add more containers");
        }

        if (totalWeight + container.LoadWeight + container.Weight > this.MaxWeight * 1000)
        {
            throw new InvalidOperationException("Weight limit exceeded - can't add container");
        }

        Containers.Add(container);
    }
    public void RemoveContainer(string serialNumber)
    {
        Container conToBeRemoved = this.Containers.FirstOrDefault(x => x.SerialNum == serialNumber);
        if (conToBeRemoved == null)
        {
            throw new InvalidOperationException("Container not found");
        }
        Containers.Remove(conToBeRemoved);
    }

    public void ReplaceContainer(Container container, string serialNumber)
    {
        Container conToBeReplaced = this.Containers.FirstOrDefault(x => x.SerialNum == serialNumber);
        if (conToBeReplaced == null)
        {
            throw new InvalidOperationException("Container not found");
        }
        double totalWeight = Containers.Sum(x => x.LoadWeight + x.Weight) - (conToBeReplaced.LoadWeight + conToBeReplaced.Weight) + (container.LoadWeight + container.Weight);

        if (totalWeight > MaxWeight * 1000)
        {
            throw new InvalidOperationException("New container exceeds weight limit.");
        }
        Containers.Remove(conToBeReplaced);
        Containers.Add(container);

    }
    public void TransferContainer(Ship targetShip, string serialNum)
    {
        Container container = Containers.FirstOrDefault(x => x.SerialNum == serialNum);
        if (container == null)
        {
            throw new InvalidOperationException("Container not found.");
        }

        RemoveContainer(serialNum);
        targetShip.AddContainer(container);
    }
    public void PrintShipInfo()
    {
        Console.WriteLine($"Ship Info: Speed: {MaxSpeed} knots, Max Containers: {MaxContainerNumber}, Max Weight: {MaxWeight} tons");
        Console.WriteLine($"Current containers: {Containers.Count}");

        foreach (var container in Containers)
        {
            Console.WriteLine($"- {container.SerialNum} ({container.GetType().Name}) | Load: {container.LoadWeight} kg");
        }
    }
}
