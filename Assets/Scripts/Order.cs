using TaskSystem;

public class Order
{
    private IFood _orderedFood;
    public IFood OrderedFood { get => _orderedFood; private set => _orderedFood = value; }

    public Order(IFood orderedFood)
    {
        OrderedFood = orderedFood;
    }
}
