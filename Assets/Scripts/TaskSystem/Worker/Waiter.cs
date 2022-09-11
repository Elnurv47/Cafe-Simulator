using TaskSystem;

public class Waiter : Worker
{
    private Order _order;
    public Order Order { get => _order; set => _order = value; }
}
