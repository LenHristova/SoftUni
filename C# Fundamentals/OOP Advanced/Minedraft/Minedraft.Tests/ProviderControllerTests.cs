using System.Collections.Generic;

using NUnit.Framework;

public class ProviderControllerTests
{

    private ProviderController providerController;

    [SetUp]
    public void SetUp()
    {
        providerController = new ProviderController(new EnergyRepository());
    }

    [Test]
    public void TestDefault()
    {
        Assert.That(providerController.TotalEnergyProduced, Is.EqualTo(0));
        Assert.That(providerController.Entities.Count, Is.EqualTo(0));
    }

    [Test]
    public void Register()
    {
        var args = new List<string>() { "Solar", "1", "10" };
        providerController.Register(args);

        Assert.That(providerController.Entities.Count, Is.EqualTo(1));
    }

    [Test]
    public void ProduceTotalEnergy()
    {
        var args = new List<string>() { "Pressure", "1", "10" };
        providerController.Register(args);

        providerController.Produce();

        Assert.That(providerController.TotalEnergyProduced, Is.EqualTo(20));
    }

    [Test]
    public void ProduceBroke()
    {
        var args = new List<string>() { "Pressure", "1", "10" };
        providerController.Register(args);

        for (int i = 0; i < 8; i++)
        {
            providerController.Produce();
        }

        Assert.That(providerController.Entities.Count, Is.EqualTo(0));
    }
}