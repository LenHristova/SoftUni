using System;

public abstract class Entity : IEntity
{
    protected const int INITIAL_DURABILITY = 1000;
    private const int BROKE_DECREMENT = 100;

    private double _durability;

    protected Entity(int id)
    {
        Id = id;
        Durability = INITIAL_DURABILITY;
    }

    public int Id { get; }

    public virtual double Durability
    {
        get => _durability;
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException(string.Format(OutputMessages.BROKEN_ENTITY, GetType().Name, Id));
            }

            _durability = value;
        }
    }

    public abstract double Produce();

    public void Broke()
    {
        Durability -= BROKE_DECREMENT;
    }

    public override string ToString()
    {
        return string.Format(OutputMessages.ENTITY_TO_STRING, GetType().Name, Durability);
    }
}