using System;

public static class AnimalFactory
{
    public static Animal CreateAnimal(string[] animalInfo)
    {
        var type = animalInfo[0];
        var name = animalInfo[1];
        var weight = double.Parse(animalInfo[2]);
        switch (type)
        {
            case nameof(Hen):
                var wingSize = double.Parse(animalInfo[3]);
                return new Hen(name, weight, wingSize);

            case nameof(Owl):
                wingSize = double.Parse(animalInfo[3]);
                return new Owl(name, weight, wingSize);

            case nameof(Mouse):
                var livingRegion = animalInfo[3];
                    return new Mouse(name, weight, livingRegion);

            case nameof(Dog):
                livingRegion = animalInfo[3];
                return new Dog(name, weight, livingRegion);

            case nameof(Cat):
                livingRegion = animalInfo[3];
                var breed = animalInfo[4];
                return new Cat(name, weight, livingRegion, breed);

            case nameof(Tiger):
                livingRegion = animalInfo[3];
                breed = animalInfo[4];
                return new Tiger(name, weight, livingRegion, breed);
        }

        throw new ArgumentException("Invalid animal type");
    }
}